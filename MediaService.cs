using System.Net;
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
        /// 获取设备配置信息
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
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var xnode_list = result.Html.XmlToEntity<XmlValue>();
                if (xnode_list.ChildNodes != null
                       && xnode_list.ChildNodes[1].ChildNodes != null
                       && xnode_list.ChildNodes[1].ChildNodes[0].ChildNodes != null)
                {
                    List<string> tokens = new List<string>();
                    foreach (var item in xnode_list.ChildNodes[1].ChildNodes[0].ChildNodes)
                    {
                        var token = item.Attributes[1].Value;
                        var profileName = item.ChildNodes[0].Value;

                        tokens.Add(token.ToCast<string>());
                        foreach (var configurations in item.ChildNodes[1].ChildNodes)
                        {

                        }
                    }
                    return tokens;
                }
            }
            else
            {
                /*请求失败*/
            }
            return default;
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
            string reqMessageStr = $@"
                                       <trt:GetSnapshotUri  xmlns:trt=""http://www.onvif.org/ver10/media/wsdl"">
                                              <trt:ProfileToken>{token}</trt:ProfileToken>
                                       </trt:GetSnapshotUri>";
            var result = await OnvifAuth.RemoteClient(ip, URL, reqMessageStr, user, pass, onvifUTCDateTime);
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
