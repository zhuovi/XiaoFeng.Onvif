using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XiaoFeng.Onvif
{
    /// <summary>
    /// 结果
    /// </summary>
    public class OnvifResult<T>
    {

        /// <summary>
        /// 
        /// </summary>
        public bool Success { get; set; } = false;

        /// <summary>
        /// 结果
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrMsg { get; set; }

    }
}
