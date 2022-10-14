# XiaoFeng.Onvif
using XiaoFeng.Onvif;


var ip = "192.168.12.2";
var user = "onvif";
var pass = "123456";

var onvifUTCDateTime = await DeviceService.GetSystemDateAndTime(ip);
var info= await DeviceService.GetDeviceInformation(ip, user, pass, onvifUTCDateTime);
await DeviceService.GetCapabilities(ip);
var tokens = await MediaService.GetProfiles(ip, user, pass, onvifUTCDateTime);
var streamUri = await MediaService.GetStreamUri(ip, user, pass, onvifUTCDateTime, tokens[0]);
var img =  await MediaService.GetSnapshotUri(ip, user, pass, onvifUTCDateTime, tokens[0]);

Console.WriteLine("streamUri:        " + streamUri.Insert(7, $"{user}:{pass}@"));
Console.WriteLine("img:              "+img);
Console.WriteLine("info:              " + info);
await PTZService.GetStatus(ip, user, pass, onvifUTCDateTime, tokens[0]);
