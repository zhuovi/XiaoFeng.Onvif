using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace XiaoFeng.Onvif
{
    public class Utility
    {
        /// <summary>
        /// 检查IP是否ping通
        /// </summary>
        /// <param name="_ip"></param>
        /// <returns></returns>
        public static bool CheckPingEnable(string _ip)
        {
            bool _isEnable = false;
            try
            {
                Ping pingSender = new Ping();
                PingReply reply = pingSender.Send(_ip, 120);//第一个参数为ip地址，第二个参数为ping的时间 
                if (reply.Status == IPStatus.Success)
                {
                    _isEnable = true;
                }
                else
                {
                    _isEnable = false;
                }
            }
            catch (Exception)
            {
                _isEnable = false;
            }
            return _isEnable;
        }
        /// <summary>
        /// 检查IP和端口是否通
        /// </summary>
        /// <param name="_ip"></param>
        /// <param name="_port"></param>
        /// <returns></returns>
        public static bool CheckIpPortEnable(string _ip, int _port)
        {
            //将IP和端口替换成为你要检测的
            string ipAddress = _ip;
            int portNum = _port;
            IPAddress ip = IPAddress.Parse(ipAddress);
            IPEndPoint point = new IPEndPoint(ip, portNum);

            bool _portEnable = false;
            try
            {
                using (Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    sock.Connect(point);
                    sock.Close();
                    _portEnable = true;
                }
            }
            catch (SocketException)
            {
                _portEnable = false;
            }
            return _portEnable;
        }


    }
}
