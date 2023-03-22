using System;
using System.Collections.Generic;
using System.Text;

namespace XiaoFeng.Onvif
{
    /// <summary>
    /// 发现设备信息
    /// </summary>
    public class DiscoveryOnvifInfo
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// UUID
        /// </summary>
        public string UUID { get; set; }

        /// <summary>
        /// onvif地址
        /// </summary>
        public string ServiceAddress { get; set; }

        /// <summary>
        /// Hardware
        /// </summary>
        public string Hardware { get; set; }

        /// <summary>
        /// 设备IP地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// onvif端口
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Types { get; set; }

    }
}
