using System.Net;
using XiaoFeng.Xml;

namespace XiaoFeng.Onvif
{
    /// <summary>
    /// 云台服务
    /// </summary>
    public class PTZService
    {
        public static readonly string URL = "onvif/media_service";
        /// <summary>
        /// 获取云台状态
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="onvifUTCDateTime"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<List<string>> GetStatus(string ip, string user, string pass, DateTime onvifUTCDateTime,string token)
        {
            string reqMessageStr = $@"
                                        <tptz:GetStatus xmlns:tptz=""http://www.onvif.org/ver20/ptz/wsdl"">
                                          <tptz:ProfileToken>{token}</tptz:ProfileToken>
                                        </tptz:GetStatus>";
            var result = await OnvifAuth.RemoteClient(ip, URL, reqMessageStr, user, pass, onvifUTCDateTime);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var xnode_list = result.Html.XmlToEntity<XmlValue>();
            }
            else
            {
                /*请求失败*/
            }
            return default;
        }
    }
}
