CREATE DEFINER=`e4score_app`@`%` PROCEDURE `getEventDeviceByIMEI`(
in imei varchar(255)
#in timestamp datetime,
#in DeviceProvider varchar(255),
#in tz varchar(255),
#in event_type varchar(255),
#in imei varchar(255)
)
BEGIN
select * from eztrack_device where eztrack_device.imei = imei ;

select loc.latitude,loc.longitude,ploc.longitude as last_longitude,ploc.latitude as last_latitude,ast.* from eztrack_event as ast 
inner join location as loc on ast.asset_domicile_name = loc.name 
inner join ping_location as ploc on ast.location_id = ploc.id
where ast.imei = imei order by ast.id desc limit 1;
END