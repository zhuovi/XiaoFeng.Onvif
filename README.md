# 欢迎使用 XiaoFeng.Onvif 工具库

**在你的项目中添加nuget包引用，搜索 XiaoFeng.Onvif**

#### 基本用法
```
using XiaoFeng.Onvif;

var ip = "192.168.12.2";
var port=8088;
var user = "onvif";
var pass = "123456";

var iPEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

var resu = await DeviceService.DiscoveryOnvif(3);

var onvifUTCDateTime = await DeviceService.GetSystemDateAndTime(iPEndPoint);
var info = await DeviceService.GetDeviceInformation(iPEndPoint, user, pass, onvifUTCDateTime);
var abilities= await DeviceService.GetCapabilities(iPEndPoint);
var tokens = await MediaService.GetProfiles(iPEndPoint, user, pass, onvifUTCDateTime);
var streamUri = await MediaService.GetStreamUri(iPEndPoint, user, pass, onvifUTCDateTime, tokens[0]);
var img = await MediaService.GetSnapshotUri(iPEndPoint, user, pass, onvifUTCDateTime, tokens[0]);

await PTZService.GetStatus(iPEndPoint, user, pass, onvifUTCDateTime, tokens[0]);
await PTZService.SetHomePosition(iPEndPoint, user, pass, onvifUTCDateTime, tokens[0]);
await PTZService.AbsoluteMove(iPEndPoint, user, pass, onvifUTCDateTime, tokens[0], 0, 0);
await PTZService.ContinuousMove(iPEndPoint, user, pass, onvifUTCDateTime, tokens[0], 0.6, 0.2, 1);
await PTZService.RelativeMove(iPEndPoint, user, pass, onvifUTCDateTime, tokens[0], 0.8, 0.5, 0.5);
await PTZService.GotoHomePosition(iPEndPoint, user, pass, onvifUTCDateTime, tokens[0], 0.3, 1, 1);

```

#### 开源初衷
```
在调研Onvif封装工具的时候，发现.NET 社区很少有开源的onvif项目，
有的项目还是付费的,人家直接开价，源码收费xxxx元。

经过内心激烈的挣扎，于是自己研发了一版.NET Core开源的项目。
本项目长期开源，基础功能都做好了，当然我自己的精力是有限的，
众人拾柴火焰高，希望对音视频领域感兴趣的小伙伴一起加入吧！

技术这东西，生不带来，死不带去，
技术来自于社区，我们回馈于社区。
互联网是有记忆的，等到我们七老八十了，
还能登录网站回味自己曾经的辉煌！
```
```
初版是先实现相关基础功能，后期可以做成服务或者系统，希望同道朋友一起来完善.NET社区的音视频领域的技术
```
#### 本项目开源100%免费
#### 本项目开源100%免费
#### 本项目开源100%免费
```
开源不易，路过的小伙伴可以给小编在github点个个小星星，以此激励一下小编！
```
#### 联系我
因为github访问网络确实慢，除了更新代码，本人访问github频次也低，
如果有疑问的小伙伴可以关注公众号，在线联系


![畅聊了个科技](https://user-images.githubusercontent.com/40175292/195968118-430de82a-864e-48f4-9d82-e01a33b06b0a.jpg)




