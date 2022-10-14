using System.Net;
using XiaoFeng.Xml;

namespace XiaoFeng.Onvif
{
    /// <summary>
    /// 设备服务
    /// </summary>
    public class DeviceService
    {
        /// <summary>
        /// 获取服务器时间
        /// </summary>
        public static async Task<DateTime> GetSystemDateAndTime(string ip)
        {
            var onvifUTCDateTime = DateTime.Now;
            string reqMessageStr = "<tds:GetSystemDateAndTime/>";
            var result = await Http.HttpHelper.GetHtmlAsync(new Http.HttpRequest
            {
                Method = HttpMethod.Post.ToString(),
                ContentType = "application/xml",
                Address = $"http://{ip}/onvif/device_service",
                BodyData = $"{OnvifAuth.EnvelopeHeader()}{OnvifAuth.EnvelopeBody(reqMessageStr)}{OnvifAuth.EnvelopeFooter()}"
            });
            if (result.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    var xnode_list = result.Html.XmlToEntity<XmlValue>();
                    //不同设备 获取时间的节点也不一样
                    if (   xnode_list.ChildNodes != null
                        && xnode_list.ChildNodes[0].ChildNodes != null
                        && xnode_list.ChildNodes[0].ChildNodes[0].ChildNodes != null
                        && xnode_list.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes != null)
                    {
                        //UNIVIEW Camera
                        var utcDateTime = xnode_list.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[3];
                        var time_node = utcDateTime.ChildNodes[0];
                        var date_node = utcDateTime.ChildNodes[1];

                        var _hour = time_node[0].ToCast<int>();
                        var _min = time_node[1].ToCast<int>();
                        var _sec = time_node[2].ToCast<int>();

                        var _year = date_node[0].ToCast<int>();
                        var _month = date_node[1].ToCast<int>();
                        var _day = date_node[2].ToCast<int>();

                        onvifUTCDateTime = new DateTime(_year, _month, _day, _hour, _min, _sec);
                    }
                    if (xnode_list.ChildNodes != null
                        && xnode_list.ChildNodes[1].ChildNodes != null
                        && xnode_list.ChildNodes[1].ChildNodes[0].ChildNodes != null
                        && xnode_list.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes != null)
                    {
                        //Dahua Camera
                        var utcDateTime = xnode_list.ChildNodes[1].ChildNodes[0].ChildNodes[0].ChildNodes[3];
                        var time_node = utcDateTime.ChildNodes[0];
                        var date_node = utcDateTime.ChildNodes[1];

                        var _hour = time_node[0].ToCast<int>();
                        var _min = time_node[1].ToCast<int>();
                        var _sec = time_node[2].ToCast<int>();

                        var _year = date_node[0].ToCast<int>();
                        var _month = date_node[1].ToCast<int>();
                        var _day = date_node[2].ToCast<int>();

                        onvifUTCDateTime = new DateTime(_year, _month, _day, _hour, _min, _sec);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                /*请求失败*/
            }
            return onvifUTCDateTime;
        }
        /// <summary>
        /// 初始化摄像头服务配置
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetCapabilities(string ip)
        {
            string reqMessageStr = @" 
                                      <tds:GetCapabilities> 
                                           <tds:Category>All</tds:Category> 
                                      </tds:GetCapabilities> ";
            var result = await Http.HttpHelper.GetHtmlAsync(new Http.HttpRequest
            {
                Method = HttpMethod.Post.ToString(),
                ContentType = "application/xml",
                Address = $"http://{ip}/onvif/device_service",
                BodyData = $"{OnvifAuth.EnvelopeHeader()}{OnvifAuth.EnvelopeBody(reqMessageStr)}{OnvifAuth.EnvelopeFooter()}"
            });
            if (result.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    var xnode_list = result.Html.XmlToEntity<XmlValue>();

                    foreach (var item in xnode_list.Attributes)
                    {
                        var ll = item.Value;
                    }
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
            {
                /*请求失败*/
            }
            return "";
        }
    }

}
