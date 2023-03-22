using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiaoFeng.Onvif
{
    /// <summary>
    /// 设备信息
    /// </summary>
    public class DeviceInformation
    {

        /// <summary>
        /// 厂商
        /// </summary>
        public string Manufacturer { get; set; }


        /// <summary>
        /// 模块
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string FirmwareVersion { get; set; }


        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 硬件ID
        /// </summary>
        public string HardwareId { get; set; }

    }
}
