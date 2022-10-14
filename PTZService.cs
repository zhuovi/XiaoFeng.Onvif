using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using XiaoFeng.Xml;

namespace XiaoFeng.Onvif
{
    /// <summary>
    /// 云台服务
    /// </summary>
    public class PTZService
    {
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
            var headToken = OnvifAuth.GetHeadToken(user, pass, onvifUTCDateTime);
            string reqMessageStr = $@"
                                        <tptz:GetStatus xmlns:tptz=""http://www.onvif.org/ver20/ptz/wsdl"">
                                          <tptz:ProfileToken>{token}</tptz:ProfileToken>
                                        </tptz:GetStatus>";
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
                //if (xnode_list.ChildNodes != null
                //       && xnode_list.ChildNodes[1].ChildNodes != null
                //       && xnode_list.ChildNodes[1].ChildNodes[0].ChildNodes != null)
                //{
                //    List<string> tokens = new List<string>();
                //    foreach (var item in xnode_list.ChildNodes[1].ChildNodes[0].ChildNodes)
                //    {
                //        var token = item.Attributes[1].Value;
                //        var profileName = item.ChildNodes[0].Value;

                //        tokens.Add(token.ToCast<string>());
                //        foreach (var configurations in item.ChildNodes[1].ChildNodes)
                //        {

                //        }
                //    }
                //    return tokens;
                //}
            }
            else
            {
                /*请求失败*/
            }
            return default;
        }
    }
}
