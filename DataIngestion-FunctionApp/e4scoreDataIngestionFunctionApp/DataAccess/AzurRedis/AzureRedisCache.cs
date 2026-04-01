using e4scoreDataIngestionFunctionApp.Models;
using e4scoreDataIngestionFunctionApp.Models.DomainModels;
using e4scoreDataIngestionFunctionApp.Models.Enum;
using e4scoreDataIngestionFunctionApp.Models.GeoLocation;
using e4scoreDataIngestionFunctionApp.Models.Redis;
using e4scoreDataIngestionFunctionApp.Models.RequestModels;
using NetTopologySuite.Index.HPRtree;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.DataAccess
{
    public class AzureRedisCache : IAzureRedisCache
    {
        private Lazy<ConnectionMultiplexer> lazyRedisConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            string cacheConnection = Environment.GetEnvironmentVariable("AzureRedisConnection");
            var options = ConfigurationOptions.Parse(cacheConnection);
            options.SyncTimeout = 10000; // Set the timeout value to 10 seconds
            return ConnectionMultiplexer.Connect(options);
        });

        public ConnectionMultiplexer RedisConnection
        {
            get
            {
                return lazyRedisConnection.Value;
            }
        }

        private IDatabase Connect()
        {
            return RedisConnection.GetDatabase();
        }

        #region Device
        public void SetDeviceCache(Device device)
        {
            var cache = Connect();
            cache.StringSet(RedisKeys.DeviceKey + device.imei, JsonSerializer.Serialize(device));
        }

        public Device GetDeviceCache(string imei)
        {
            var cache = Connect();
            //cache.KeyDelete("RedisKeys.DeviceKey" + imei);
            string cacheValue = cache.StringGet(RedisKeys.DeviceKey + imei);
            if (cacheValue != null)
            {
                return JsonSerializer.Deserialize<Device>(cacheValue);
            }
            return null;
        }
        #endregion

        #region Unidentified Devices
        public void SetUnidentifiedCache(string imei)
        {
            if (!UnidentifiedCacheExists(imei)) 
            {
                var cache = Connect();
                cache.StringSet(RedisKeys.UnidentifiedDeviceKey + imei, imei);
            }

        }

        public bool UnidentifiedCacheExists(string imei)
        {
            var cache = Connect();
            //cache.KeyDelete("RedisKeys.DeviceKey" + imei);
            string cacheValue = cache.StringGet(RedisKeys.UnidentifiedDeviceKey + imei);
            if (string.IsNullOrEmpty(cacheValue))
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Matrack
        public void SetMatrackCache(MatrackRequest matrackRequest)
        {
            var cache = Connect();
            cache.StringSet(RedisKeys.MatrackKey + matrackRequest.imei, JsonSerializer.Serialize(matrackRequest));
        }

        public MatrackRequest GetMatrackCache(string imei)
        {
            imei = imei.Trim();
            var cache = Connect();

            string cacheValue = cache.StringGet(RedisKeys.MatrackKey + imei);
            if (cacheValue != null)
            {
                return JsonSerializer.Deserialize<MatrackRequest>(cacheValue);
            }
            return null;
        }
        #endregion

        #region DateOfLastMove
        public void SetDateOfLastMoveCache(DateTime dateOfLastMove, string imei)
        {
            var cache = Connect();
            cache.StringSet(RedisKeys.ImeiKey + imei, Convert.ToString(dateOfLastMove));
        }

        public string GetDateOfLastMoveCache(string imei)
        {
            var cache = Connect();

            string cacheValue = cache.StringGet(RedisKeys.ImeiKey + imei);
            if (cacheValue != null)
            {
                return cacheValue;
            }
            return null;
        }
        #endregion

        #region Event
        public void SetEventCache(Event events)
        {
            if (events is null) 
            {
                return;
            }
            var cache = Connect();

            cache.StringSet(RedisKeys.EventKey + events.imei, JsonSerializer.Serialize(events));
        }

        public Event GetEventCache(string imei)
        {
            var cache = Connect();
            //cache.KeyDelete("event_" + imei);

            string cacheValue = cache.StringGet(RedisKeys.EventKey + imei);
            if (cacheValue != null)
            {
                return JsonSerializer.Deserialize<Event>(cacheValue);
            }
            return null;
        }
        #endregion

        #region LastMoves
        public void SetMovesInLastNDaysCache(List<LastMoveDays> lastMoveDays, string imei)
        {
            if (lastMoveDays is null)
            {
                return;
            }
            var cache = Connect();
            int isStart = 1;
            int isEnd = 1;
            foreach (var item in lastMoveDays.OrderBy(a => a.serial_number))
            {
                lastMoveDays[0].StartKey = isStart;
                lastMoveDays.LastOrDefault().Endkey = isEnd;
                item.imei = imei;
                object value = JsonSerializer.Serialize(item);
                cache.HashSet($"{RedisKeys.MoveInNOfDays}{imei}", new HashEntry[]
                {
                  new HashEntry(item.serial_number.ToString(), JsonSerializer.Serialize(item))
                });
            }
        }

    public void SetMovesInLastNDaysCacheFromEvent(LastMovesInNDays lastMovesInNDays, string imei)
    {
      if (lastMovesInNDays is null)
      {
        return;
      }
      var cache = Connect(); 
      cache.StringSet(RedisKeys.MoveInNOfDays + imei, JsonSerializer.Serialize(lastMovesInNDays)); 
    }


    public void updateMovesInLastNDaysCache(List<LastMoveDays> lastMoveDays, MatrackRequest matrackRequest, ulong isMove = 1)
        {
            var cache = Connect();
            if (lastMoveDays.Count >= 90)
            { 
                int next = 0;
                for (int i = 0; i <= lastMoveDays.Count - 1; i++)
                {
                    if (lastMoveDays[i].StartKey == 1)
                    {
                        cache.HashDelete($"{RedisKeys.MoveInNOfDays}{matrackRequest.imei}", lastMoveDays[i].serial_number.ToString());
                        next = i + 1;
                        lastMoveDays[next].StartKey = 1;
                        break;
                    }
                }
            }
            var lastEntry = lastMoveDays.Where(a => a.Endkey == 1).FirstOrDefault();
            var lastEntryKey  = lastEntry.serial_number + 1;
            lastEntry.Endkey = 0;


            cache.HashSet("lastMoveDays_" + matrackRequest.imei, new HashEntry[] { new HashEntry($"{lastEntry.serial_number}", JsonSerializer.Serialize(lastEntry)) });
            cache.HashSet("lastMoveDays_" + matrackRequest.imei, new HashEntry[] { new HashEntry($"{lastEntryKey}", JsonSerializer.Serialize(lastEntry)) });

            LastMoveDays lastMove = new LastMoveDays
            {
                imei = matrackRequest.imei,
                source_timestamp = matrackRequest.timestamp,
                is_move = isMove,
                StartKey = 0,
                Endkey = 1,
                serial_number = lastEntryKey,
            };
            cache.HashSet($"{RedisKeys.MoveInNOfDays}{matrackRequest.imei}", new HashEntry[]
            {
                new HashEntry(lastMove.serial_number, JsonSerializer.Serialize(lastMove))
            });
        }

        public List<LastMoveDays> GetMovesInLastNDaysCache(string imei)
        {
            var cache = Connect();
            List<LastMoveDays> lastMoveDays = new List<LastMoveDays>();
            var cacheValue = cache.HashKeys(RedisKeys.MoveInNOfDays + imei);

            //foreach (var entry in cacheValue)
            //{
            //    cache.HashDelete(RedisKeys.MoveInNOfDays + imei, entry);
            //    Console.WriteLine($"hash key deleted : {entry}");
            //}

            var latMoveDays = cache.HashGetAll(RedisKeys.MoveInNOfDays + imei).Select(lastMoveCache => Newtonsoft.Json.JsonConvert.DeserializeObject<LastMoveDays>(lastMoveCache.Value)).OrderBy(x => x.source_timestamp).ToList();

            if (!latMoveDays.Any()) 
            {
                return null;
            }

            return  latMoveDays;
        }


    public LastMovesInNDays GetMovesInLastNDaysCacheNew(string imei)
    {
      var cache = Connect();
      //cache.KeyDelete("event_" + imei);

      string latMoveDays = cache.StringGet(RedisKeys.MoveInNOfDays + imei);
      if (latMoveDays != null)
      {
        return JsonSerializer.Deserialize<LastMovesInNDays>(latMoveDays);
      }
      return null;

     }


    #endregion

    #region Dwell time

    public HashEntry[] GetAllLocationsByComapnyIdCache(string companyId)
        {
            var cache = Connect();

            var locations = cache.HashGetAll($"locations_{companyId}");
                       
            return locations;
        }
        public void SetDwellTimeCache(DwellTime dwellTime)
        {
            var cache = Connect();

            cache.StringSet("dwellTime_" + dwellTime.Imei, System.Text.Json.JsonSerializer.Serialize(dwellTime));
        }

        public DwellTime GetDwellTimeCache(string imei)
        {
            var cache = Connect();

            string cacheValue = cache.StringGet("dwellTime_" + imei);
            if (cacheValue != null)
            {
                return System.Text.Json.JsonSerializer.Deserialize<DwellTime>(cacheValue);
            }
            return null;
        }
        #endregion

        #region Excursion time

        public void SetExcursionTimeCache(ExcursionTime dwellTime)
        {
            var cache = Connect();
            cache.StringSet("excursionTime_" + dwellTime.Imei, JsonSerializer.Serialize(dwellTime));
        }
        public ExcursionTime GetExcursionTimeCache(string imei)
        {
            var cache = Connect();
            string cacheValue = cache.StringGet("excursionTime_" + imei);
            if (cacheValue != null)
            {
                return JsonSerializer.Deserialize<ExcursionTime>(cacheValue);
            }
            return null;
        }
        #endregion

        #region Asset Profile

        public void SetAssetProfileCache(string imei, DeviceDwellTime deviceDwellTime, DeviceExcursionTime deviceExcursionTime , long idleTime)
        {
            AssetProfile assetProfile = new AssetProfile
            {
                DeviceDwellTime = deviceDwellTime,
                DeviceExcursionTime = deviceExcursionTime,
                IdleTime= idleTime

            };
            var cache = Connect();
            cache.StringSet("assetProfile_" + imei, JsonSerializer.Serialize(assetProfile));
        }
        public AssetProfile GetAssetProfileCache(string imei)
        {
            var cache = Connect();
            string cacheValue = cache.StringGet("assetProfile_" + imei);
            if (cacheValue != null)
            {
                return JsonSerializer.Deserialize<AssetProfile>(cacheValue);
            }
            return null;
        }
        #endregion

    }
}
