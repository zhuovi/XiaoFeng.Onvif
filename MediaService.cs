using System.Net;
using System.Xml.Linq;
using XiaoFeng.Xml;

namespace XiaoFeng.Onvif
{
    /// <summary>
    /// 媒体服务
    /// </summary>
    public class MediaService
    {
        public static readonly string URL = "onvif/media_service";
        /// <summary>
        /// 获取token信息
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static async Task<List<string>> GetProfiles(string ip, string user, string pass, DateTime onvifUTCDateTime)
        {
            string reqMessageStr = @"
                                     <GetProfiles xmlns=""http://www.onvif.org/ver20/media/wsdl"">
                                         <Type>All</Type>
                                     </GetProfiles>";
            var result = await OnvifAuth.RemoteClient(ip, URL, reqMessageStr, user, pass, onvifUTCDateTime);
            var xnode = result.Html.ReplacePattern(@"(<|/)[a-z0-9\-]+:", "$1");
            if (result.StatusCode == HttpStatusCode.OK)
            {
               return XElement.Parse(xnode).Descendants("Profiles").Attributes("token").Select(x=>x.Value).ToList();
            }
            else
            {
                return OnvifAuth.ErrorResponse(xnode).ToCast<List<string>>();
            }
        }
        /// <summary>
        /// 获取视频流地址
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>视频流地址</returns>
        public static async Task<string> GetStreamUri(string ip, string user, string pass, DateTime onvifUTCDateTime, string token)
        {
            string reqMessageStr = $@"
                                        <GetStreamUri xmlns=""http://www.onvif.org/ver10/media/wsdl"">
                                          <StreamSetup>
                                            <Stream xmlns=""http://www.onvif.org/ver10/schema"">RTP-Unicast</Stream>
                                            <Transport xmlns=""http://www.onvif.org/ver10/schema"">
                                              <Protocol>UDP</Protocol>
                                            </Transport>
                                          </StreamSetup>
                                          <ProfileToken>{token}</ProfileToken>
                                       </GetStreamUri>";
            var result = await OnvifAuth.RemoteClient(ip, URL, reqMessageStr, user, pass, onvifUTCDateTime);
            var xnode = result.Html.ReplacePattern(@"(<|/)[a-z0-9\-]+:", "$1");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return XElement.Parse(xnode).Descendants("Uri").Select(x => x.Value).FirstOrDefault();
            }
            else
            {
                return OnvifAuth.ErrorResponse(xnode).ToCast<string>();
            }
        }
        /// <summary>
        /// 获取视频快照地址   
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>视频快照地址</returns>
        public static async Task<string> GetSnapshotUri(string ip, string user, string pass, DateTime onvifUTCDateTime, string token)
        {
            string reqMessageStr = $@"
                                       <trt:GetSnapshotUri  xmlns:trt=""http://www.onvif.org/ver10/media/wsdl"">
                                              <trt:ProfileToken>{token}</trt:ProfileToken>
                                       </trt:GetSnapshotUri>";
            var result = await OnvifAuth.RemoteClient(ip, URL, reqMessageStr, user, pass, onvifUTCDateTime);
            var xnode = result.Html.ReplacePattern(@"(<|/)[a-z0-9\-]+:", "$1");
            if (result.StatusCode == HttpStatusCode.OK)
            {
                return XElement.Parse(xnode).Descendants("Uri").Select(x => x.Value).FirstOrDefault();
            }
            else
            {
                return OnvifAuth.ErrorResponse(xnode).ToCast<string>();
            }
        }
    }
}
