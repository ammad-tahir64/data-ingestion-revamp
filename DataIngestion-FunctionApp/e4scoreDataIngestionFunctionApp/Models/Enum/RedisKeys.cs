using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace e4scoreDataIngestionFunctionApp.Models.Enum
{
  public static class RedisKeys
  {
    public const string DeviceKey = "device_";
    public const string MatrackKey = "matrack_";
    public const string ImeiKey = "imei_";
    public const string EventKey = "event_";
    public const string MoveInNOfDays = "lastMoveDays_";
     public const string UnidentifiedDeviceKey = "unidentifiedimei_";

  }
}
