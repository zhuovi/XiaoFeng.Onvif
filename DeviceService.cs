using System;
using System.Net;
using XiaoFeng.Xml;
using static XiaoFeng.Data.DataConfig;

namespace XiaoFeng.Onvif
{
    /// <summary>
    /// 设备服务
    /// </summary>
    public class DeviceService
    {
        public static readonly string URL = "onvif/device_service";
        /// <summary>
        /// 获取服务器时间
        /// </summary>
        public static async Task<DateTime> GetSystemDateAndTime(string ip)
        {
            var onvifUTCDateTime = DateTime.Now;
            string reqMessageStr = "<tds:GetSystemDateAndTime/>";
            var result = await OnvifAuth.RemoteClient(ip, URL, reqMessageStr, "user", "pass", onvifUTCDateTime);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                var xnode_list = result.Html.XmlToEntity<XmlValue>();
                //不同设备 获取时间的节点也不一样
                #region 获取设备时间
                if (xnode_list.ChildNodes != null
                    && xnode_list.ChildNodes[0].ChildNodes != null
                    && xnode_list.ChildNodes[0].ChildNodes[0].ChildNodes != null
                    && xnode_list.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes != null)
                {
                    //UNIVIEW、HIKVISION Camera
                    var utcDateTime = xnode_list.ChildNodes[0].ChildNodes[0].ChildNodes[0].ChildNodes[3];
                    var time_node = utcDateTime.ChildNodes[0];
                    var date_node = utcDateTime.ChildNodes[1];

                    var _hour = time_node[0].ToCast<int>();
                    var _min = time_node[1].ToCast<int>();
                    var _sec = time_node[2].ToCast<int>();

                    var _year = date_node[0].ToCast<int>();
                    var _month = date_node[1].ToCast<int>();
                    var _day = date_node[2].ToCast<int>();

                    return new DateTime(_year, _month, _day, _hour, _min, _sec);
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

                    return new DateTime(_year, _month, _day, _hour, _min, _sec);
                }
                #endregion
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
            var result = await OnvifAuth.RemoteClient(ip, URL, reqMessageStr, "user", "pass", DateTime.Now);
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
        /// <summary>
        /// 获取设备信息
        /// </summary>
        /// <returns></returns>
        public static async Task<string> GetDeviceInformation(string ip, string user, string pass, DateTime onvifUTCDateTime)
        {
            string reqMessageStr = @" 
                                      <tds:GetDeviceInformation /> ";
            var result = await OnvifAuth.RemoteClient(ip, URL, reqMessageStr, user, pass, onvifUTCDateTime);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                try
                {
                    var xnode_list = result.Html.ReplacePattern(@"(<|/)[a-z\-]+:", "$1").XmlToEntity<XmlValue>();
                    if (xnode_list.ChildNodes.Count > 1)
                    {
                        return xnode_list.ChildNodes[1].ChildNodes[0].ChildNodes.ToDictionary(x => x.Name, x => x.Value).ToJson();
                    }
                    return xnode_list.ChildNodes[0].ChildNodes[0].ChildNodes.ToDictionary(x => x.Name, x => x.Value).ToJson();
                }
                catch (Exception ex)
                {
                    return ex.ToString();
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
