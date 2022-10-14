using System.Net;
using XiaoFeng.Xml;

namespace XiaoFeng.Onvif
{
    /// <summary>
    /// 媒体服务
    /// </summary>
    public class MediaService
    {
        /// <summary>
        /// 获取设备配置信息
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static async Task<string> GetProfiles(string ip, string user, string pass, DateTime onvifUTCDateTime)
        {
            var headToken = OnvifAuth.GetHeadToken(user, pass, onvifUTCDateTime);
            string reqMessageStr = @"
                                     <GetProfiles xmlns=""http://www.onvif.org/ver20/media/wsdl"">
                                         <Type>All</Type>
                                     </GetProfiles>";
            var result = await Http.HttpHelper.GetHtmlAsync(new Http.HttpRequest
            {
                Method = HttpMethod.Post.ToString(),
                ContentType = "application/xml",
                Address = $"http://{ip}/onvif/media_service",
                BodyData = $"{OnvifAuth.EnvelopeHeader()}{headToken}{OnvifAuth.EnvelopeBody(reqMessageStr)}{OnvifAuth.EnvelopeFooter()}"
            });
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var value = result.Html;
            }
            else
            {
                /*请求失败*/
            }
            return "";
        }
        /// <summary>
        /// 获取视频流地址
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>视频流地址</returns>
        public static async Task<string> GetStreamUri(string ip, string user, string pass, DateTime onvifUTCDateTime, string token)
        {
            var headToken = OnvifAuth.GetHeadToken(user, pass, onvifUTCDateTime);
            //设置发送消息体
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
            var result = await Http.HttpHelper.GetHtmlAsync(new Http.HttpRequest
            {
                Method = HttpMethod.Post.ToString(),
                ContentType = "application/xml",
                Address = $"http://{ip}/onvif/media_service",
                BodyData = $"{OnvifAuth.EnvelopeHeader()}{headToken}{OnvifAuth.EnvelopeBody(reqMessageStr)}{OnvifAuth.EnvelopeFooter()}"
            });
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var value = result.Html;
            }
            else
            {
                /*请求失败*/
            }
            return "";
        }
        /// <summary>
        /// 获取视频快照地址   
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns>视频快照地址</returns>
        public static async Task<string> GetSnapshotUri(string ip, string user, string pass, DateTime onvifUTCDateTime, string token)
        {
            var headToken = OnvifAuth.GetHeadToken(user, pass, onvifUTCDateTime);
            //设置发送消息体
            string reqMessageStr = $@"
                                       <trt:GetSnapshotUri  xmlns:trt=""http://www.onvif.org/ver10/media/wsdl"">
                                              <trt:ProfileToken>{token}</trt:ProfileToken>
                                       </trt:GetSnapshotUri>";
            var result = await Http.HttpHelper.GetHtmlAsync(new Http.HttpRequest
            {
                Method = HttpMethod.Post.ToString(),
                ContentType = "application/xml",
                Address = $"http://{ip}/onvif/media_service",
                BodyData = $"{OnvifAuth.EnvelopeHeader()}{headToken}{OnvifAuth.EnvelopeBody(reqMessageStr)}{OnvifAuth.EnvelopeFooter()}"
            });
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var xnode_list = result.Html.XmlToEntity<XmlValue>();
                if (xnode_list.ChildNodes != null
                       && xnode_list.ChildNodes[1].ChildNodes != null
                       && xnode_list.ChildNodes[1].ChildNodes[0].ChildNodes != null
                       && xnode_list.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes != null)
                {
                    return xnode_list.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[0].Value.ToCast<string>();
                }
            }
            else
            {
                var xnode_list = result.Html.XmlToEntity<XmlValue>();
                if (xnode_list.ChildNodes != null
                       && xnode_list.ChildNodes[1].ChildNodes != null
                       && xnode_list.ChildNodes[1].ChildNodes[0].ChildNodes != null
                       && xnode_list.ChildNodes[1].ChildNodes[0].ChildNodes[1].ChildNodes != null)
                {
                    return xnode_list.ChildNodes[1].ChildNodes[0].ChildNodes[1].ChildNodes[0].Value.ToCast<string>();
                }
            }
            return "";
        }
    }
}
