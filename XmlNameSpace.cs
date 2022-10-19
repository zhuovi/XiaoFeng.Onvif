using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoFeng.Onvif
{
    /// <summary>
    /// 命名空间
    /// </summary>
    public class XmlNameSpace
    {
        public static string DeviceService = @" xmlns:tds=""http://www.onvif.org/ver10/device/wsdl""
                                                xmlns:tt=""http://www.onvif.org/ver10/schema"" ";
        public static string EventService = @"xmlns:wsa=""http://www.w3.org/2005/08/addressing"" 
                                              xmlns:tev=""http://www.onvif.org/ver10/events/wsdl""";
        public static string MediaService = @"xmlns:trt=""http://www.onvif.org/ver10/media/wsdl""
                                              xmlns:tt=""http://www.onvif.org/ver10/schema""";
        public static string PTZService = @"xmlns:ter=""http://www.onvif.org/ver10/error""
                                            xmlns:xs=""http://www.w3.org/2001/XMLSchema""
                                            xmlns:tt=""http://www.onvif.org/ver10/schema""
                                            xmlns:tptz=""http://www.onvif.org/ver20/ptz/wsdl""";
    }
}
