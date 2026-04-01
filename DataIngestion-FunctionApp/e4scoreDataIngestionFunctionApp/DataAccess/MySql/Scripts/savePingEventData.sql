CREATE DEFINER=`e4score_app`@`%` PROCEDURE `savePingEventData`(
in battery double,
in temperature double,
in longitude double,
in latitude double ,
in assetid varchar(255),
in event_type varchar(255),
in direction varchar(255),
in company_id int,
in fuel int,
in imei varchar(255),
in source_timestamp datetime,
in speed int,
in tracker_type varchar(255),
in distanceFromDomicile double,
in address varchar(255),
in city varchar(255),
in postal varchar(255),
in state varchar(255),
in sensor_uuid varchar(255),
in location_uuid varchar(255),
in distanceFromPreviousEvent double
)
BEGIN

insert into `ezcheckin`.`ping_sensor` (created,deleted,enabled,updated,uuid,version,battery,temperature)
values (utc_timestamp(),0,1,null,sensor_uuid,0,battery,temperature);

insert into `ezcheckin`.`ping_location` (created,deleted,enabled,updated,uuid,version,latitude,longitude)
values (utc_timestamp(),0,1,null,location_uuid,0,latitude,longitude);
 
set @location_id = (select ping_location.id from ping_location where ping_location.uuid = location_uuid); 
 SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = sensor_uuid;

set @sensor_id = (select ping_sensor.id from ping_sensor where ping_sensor.uuid = sensor_uuid);
set @Assetuuid = (select asset.uuid from asset where asset.id =  assetid  and company_id = asset.company_id);
set @AssetName = (select asset.asset_id from asset where asset.id = assetid and asset.company_id = company_id );
set @dateoflastmove = (select eztrack_event.date_of_last_move from eztrack_event where eztrack_event.imei = imei order by created desc limit 1);
set @AssetDomicile = (select loc.name from asset as ast inner join location as loc on ast.domicile_id = loc.id where ast.id = assetid and loc.company_id = company_id);
set @LocationName = (select loc.name from asset as ast inner join location as loc on ast.location_id = loc.id where ast.id = assetid and loc.company_id = company_id);
set @LastPingLocation_lat  = (select ploc.latitude from eztrack_event as event inner join ping_location as ploc on event.location_id = ploc.id where event.imei = imei order by event.id desc limit 1);
set @LastPingLocation_lng  = (select ploc.longitude from eztrack_event as event inner join ping_location as ploc on event.location_id = ploc.id where event.imei = imei order by event.id desc limit 1);

if(@LastPingLocation_lat != latitude and @LastPingLocation_lng != longitude)
	then 
insert into `ezcheckin`.`eztrack_event` ( 
 eztrack_event.created ,
eztrack_event.deleted ,
eztrack_event.enabled ,
eztrack_event.updated ,
eztrack_event.uuid ,
eztrack_event.version ,
eztrack_event.address ,
eztrack_event.asset_uuid ,
eztrack_event.city ,
eztrack_event.date_of_last_move ,
eztrack_event.direction ,
eztrack_event.distance_from_domicile_in_meters ,
eztrack_event.distance_from_previous_event_in_meters ,
eztrack_event.event_type ,
eztrack_event.first_move_of_day ,
eztrack_event.fuel ,
eztrack_event.geofence_address ,
eztrack_event.geofence_city ,
eztrack_event.geofence_postal ,
eztrack_event.geofence_state ,
eztrack_event.idle_time ,
eztrack_event.imei ,
eztrack_event.is_move ,
eztrack_event.located_with ,
eztrack_event.location_name ,
eztrack_event.mileage ,
eztrack_event.move_threshold_in_meters ,
eztrack_event.moves_in_last30days ,
eztrack_event.ping_type ,
eztrack_event.postal ,
eztrack_event.sequence ,
eztrack_event.source_created_at ,
eztrack_event.source_timestamp ,
eztrack_event.source_uuid ,
eztrack_event.speed ,
eztrack_event.state ,
eztrack_event.group_id ,
eztrack_event.location_id ,
eztrack_event.sensors_id ,
eztrack_event.zone ,
eztrack_event.ping_event_uuid ,
eztrack_event.asset_domicile_name ,
eztrack_event.asset_name ,
eztrack_event.tracker_type ,
eztrack_event.excrusion_time_start ,
eztrack_event.dwell_time_start ,
eztrack_event.excrusion_time ,
eztrack_event.dwell_time 
)
VALUES 
(
utc_timestamp(),
0,
1,
null,
uuid(),
0,
address,
@Assetuuid,
city,
source_timestamp,
direction,
distanceFromDomicile,
distanceFromPreviousEvent,
event_type,
0,
fuel,
null,
null,
null,
null,
0,
imei,
1,
0,
@LocationName ,
null,
804.67,
0,
null,
null,
null,
null,
source_timestamp,
null,
speed,
null,
null,
@location_id,
@sensor_id,
1,
null,
@AssetDomicile,
@AssetName,
tracker_type,
null,
null,
null,
null
);
 set @aid = (select id from eztrack_event order by created desc limit 1);
 #SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = @aid ;

call ezcheckin.updateDeviceData(battery,assetid,company_id,imei,tracker_type,distanceFromDomicile,@location_id ,@AssetDomicile,address,city,postal,state);

else

insert into `ezcheckin`.`eztrack_event` ( 
 eztrack_event.created ,
eztrack_event.deleted ,
eztrack_event.enabled ,
eztrack_event.updated ,
eztrack_event.uuid ,
eztrack_event.version ,
eztrack_event.address ,
eztrack_event.asset_uuid ,
eztrack_event.city ,
eztrack_event.date_of_last_move ,
eztrack_event.direction ,
eztrack_event.distance_from_domicile_in_meters ,
eztrack_event.distance_from_previous_event_in_meters ,
eztrack_event.event_type ,
eztrack_event.first_move_of_day ,
eztrack_event.fuel ,
eztrack_event.geofence_address ,
eztrack_event.geofence_city ,
eztrack_event.geofence_postal ,
eztrack_event.geofence_state ,
eztrack_event.idle_time ,
eztrack_event.imei ,
eztrack_event.is_move ,
eztrack_event.located_with ,
eztrack_event.location_name ,
eztrack_event.mileage ,
eztrack_event.move_threshold_in_meters ,
eztrack_event.moves_in_last30days ,
eztrack_event.ping_type ,
eztrack_event.postal ,
eztrack_event.sequence ,
eztrack_event.source_created_at ,
eztrack_event.source_timestamp ,
eztrack_event.source_uuid ,
eztrack_event.speed ,
eztrack_event.state ,
eztrack_event.group_id ,
eztrack_event.location_id ,
eztrack_event.sensors_id ,
eztrack_event.zone ,
eztrack_event.ping_event_uuid ,
eztrack_event.asset_domicile_name ,
eztrack_event.asset_name ,
eztrack_event.tracker_type ,
eztrack_event.excrusion_time_start ,
eztrack_event.dwell_time_start ,
eztrack_event.excrusion_time ,
eztrack_event.dwell_time 
)
VALUES 
(
utc_timestamp(),
0,
1,
null,
uuid(),
0,
address,
@Assetuuid,
city,
@dateoflastmove,
direction,
distanceFromDomicile,
0,
event_type,
0,
fuel,
null,
null,
null,
null,
0,
imei,
0,
0,
@LocationName ,
null,
804.67,
0,
null,
null,
null,
null,
source_timestamp,
null,
speed,
null,
null,
@location_id,
@sensor_id,
1,
null,
@AssetDomicile,
@AssetName,
tracker_type,
null,
null,
null,
null
);
 set @aid = (select id from eztrack_event order by created desc limit 1);
 #SIGNAL SQLSTATE '45000' SET MESSAGE_TEXT = @aid ;

call ezcheckin.updateDeviceData(battery,assetid,company_id,imei,tracker_type,distanceFromDomicile,@location_id ,@AssetDomicile,address,city,postal,state);
call ezcheckin.UpdateAssetData(battery,assetid,company_id,imei,tracker_type,distanceFromDomicile,@location_id ,@Assetuuid,address,city,postal,state);

 end if;
END