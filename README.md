# 欢迎使用 XiaoFeng.Onvif 工具库

**在你的项目中添加nuget包引用，搜索 XiaoFeng.Onvif**

![fayelf](https://user-images.githubusercontent.com/16105174/197918392-29d40971-a8a2-4be4-ac17-323f1d0bed82.png)

![GitHub top language](https://img.shields.io/github/languages/top/zhuovi/xiaofeng.Onvif?logo=github)
![GitHub License](https://img.shields.io/github/license/zhuovi/xiaofeng.Onvif?logo=github)
![Nuget Downloads](https://img.shields.io/nuget/dt/xiaofeng.Onvif?logo=nuget)
![Nuget](https://img.shields.io/nuget/v/xiaofeng.Onvif?logo=nuget)
![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/xiaofeng.Onvif?label=dev%20nuget&logo=nuget)

Nuget：XiaoFeng.Onvif

| QQ群号 | QQ群 | 公众号 |
| :----:| :----: | :----: |
| 748408911  | ![QQ 群](https://user-images.githubusercontent.com/16105174/198058269-0ea5928c-a2fc-4049-86da-cca2249229ae.png) | ![畅聊了个科技](https://user-images.githubusercontent.com/16105174/198059698-adbf29c3-60c2-4c76-b894-21793b40cf34.jpg) |

源码： https://github.com/zhuovi/xiaofeng.Onvif

# XiaoFeng 类库包含库
| 命名空间 | 所属类库 | 开源状态 | 说明 | 包含功能 |
| :----| :---- | :---- | :----: | :---- |
| XiaoFeng.Prototype | XiaoFeng.Core | :white_check_mark: | 扩展库 | ToCase 类型转换<br/>ToTimestamp,ToTimestamps 时间转时间戳<br/>GetBasePath 获取文件绝对路径,支持Linux,Windows<br/>GetFileName 获取文件名称<br/>GetMatch,GetMatches,GetMatchs,IsMatch,ReplacePatten,RemovePattern 正则表达式操作<br/> |
| XiaoFeng.Net | XiaoFeng.Net | :white_check_mark: | 网络库 | XiaoFeng网络库，封装了Socket客户端，服务端（Socket,WebSocket），根据当前库可轻松实现订阅，发布等功能。|
| XiaoFeng.Http | XiaoFeng.Core | :white_check_mark: | 模拟请求库 | 模拟网络请求 |
| XiaoFeng.Data | XiaoFeng.Core | :white_check_mark: | 数据库操作库 | 支持SQLSERVER,MYSQL,ORACLE,达梦,SQLITE,ACCESS,OLEDB,ODBC等数十种数据库 |
| XiaoFeng.Cache | XiaoFeng.Core | :white_check_mark: | 缓存库 |  内存缓存,Redis,MemcachedCache,MemoryCache,FileCache缓存 |
| XiaoFeng.Config | XiaoFeng.Core | :white_check_mark: | 配置文件库 | 通过创建模型自动生成配置文件，可为xml,json,ini文件格式 |
| XiaoFeng.Cryptography | XiaoFeng.Core | :white_check_mark: | 加密算法库 | AES,DES,RSA,MD5,DES3,SHA,HMAC,RC4加密算法 |
| XiaoFeng.Excel | XiaoFeng.Excel | :white_check_mark: | Excel操作库 | Excel操作，创建excel,编辑excel,读取excel内容，边框，字体，样式等功能  |
| XiaoFeng.Ftp | XiaoFeng.Ftp | :white_check_mark: | FTP请求库 | FTP客户端 |
| XiaoFeng.IO | XiaoFeng.Core | :white_check_mark: | 文件操作库 | 文件读写操作 |
| XiaoFeng.Json | XiaoFeng.Core | :white_check_mark: | Json序列化，反序列化库 | Json序列化，反序列化库 |
| XiaoFeng.Xml | XiaoFeng.Core | :white_check_mark: | Xml序列化，反序列化库 | Xml序列化，反序列化库 |
| XiaoFeng.Log | XiaoFeng.Core | :white_check_mark: | 日志库 | 写日志文件,数据库 |
| XiaoFeng.Memcached | XiaoFeng.Memcached | :white_check_mark: | Memcached缓存库 | Memcached中间件,支持.NET框架、.NET内核和.NET标准库,一种非常方便操作的客户端工具。实现了Set,Add,Replace,PrePend,Append,Cas,Get,Gets,Gat,Gats,Delete,Touch,Stats,Stats Items,Stats Slabs,Stats Sizes,Flush_All,Increment,Decrement,线程池功能。|
| XiaoFeng.Redis | XiaoFeng.Redis | :white_check_mark: | Redis缓存库 | Redis中间件,支持.NET框架、.NET内核和.NET标准库,一种非常方便操作的客户端工具。实现了Hash,Key,String,ZSet,Stream,Log,List,订阅发布,线程池功能; |
| XiaoFeng.Threading | XiaoFeng.Core | :white_check_mark: | 线程库 | 线程任务,线程队列 |
| XiaoFeng.Mvc | XiaoFeng.Mvc | :x: | 低代码WEB开发框架 | .net core 基础类，快速开发CMS框架，真正的低代码平台，自带角色权限，WebAPI平台，后台管理，可托管到服务运行命令为:应用.exe install 服务名 服务说明,命令还有 delete 删除 start 启动  stop 停止。 |
| XiaoFeng.Proxy | XiaoFeng.Proxy | :white_check_mark: | 代理库 | 开发中 |
| XiaoFeng.TDengine | XiaoFeng.TDengine | :white_check_mark: | TDengine 客户端 | 开发中 |
| XiaoFeng.GB28181 | XiaoFeng.GB28181 | :white_check_mark: | 视频监控库，SIP类库，GB28181协议 | 开发中 |
| XiaoFeng.Onvif | XiaoFeng.Onvif | :white_check_mark: | 视频监控库Onvif协议 | XiaoFeng.Onvif 基于.NET平台使用C#封装Onvif常用接口、设备、媒体、云台等功能， 拒绝WCF服务引用动态代理生成wsdl类文件 ， 使用原生XML扩展标记语言封装参数，所有的数据流向都可控。 |

# XiaoFeng.Onvif

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




