using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;

namespace XiaoFeng.Onvif
{
    /// <summary>
    /// 通用工具类
    /// </summary>
    public static class Utility
    {
        public static async Task<List<UdpReceiveResult>> UdpOnvifClient(int timeout, string netMask)
        {
            var sendBuffer = OnvifAuth.UdpProbeMessage();
            var ipa = new IPEndPoint(IPAddress.Parse(netMask), 3702);
            var udp = new UdpClient()
            {
                EnableBroadcast = true
            };
            await udp.SendAsync(sendBuffer, sendBuffer.Length, ipa);
            var list = new List<UdpReceiveResult>();
            while (true)
            {
                try
                {
                    var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeout));
                    if (cts.IsCancellationRequested) break;
                    var ata = await udp.ReceiveAsync().WithCancellation(cts.Token);
                    list.Add(ata);
                }
                catch (Exception ex)
                {
                    break;
                }
            }
            udp.Close();
            udp.Dispose();
            return list;
        }

        //获取本机的网段
        public static string GetLocalIPGateway()
        {
            string[] arrary = GetLocalIp().Split('.');
            return arrary[2];
        }
        //获取本机ip
        public static string GetLocalIp()
        {
            ///获取本地的IP地址
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {

                //Debug.Log(_IPAddress.AddressFamily.ToString());
                //Debug.Log(_IPAddress.ToString());
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                    return AddressIP;
                }
            }
            return AddressIP;
        }

        /// <summary>
        /// 用于为返回任务的任何异步Task<T>方法提供取消可能性
        /// </summary>
        public static async Task<T> WithCancellation<T>(this Task<T> task, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            using (cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs))
            {
                if (task != await Task.WhenAny(task, tcs.Task))
                {
                    throw new OperationCanceledException(cancellationToken);
                }
            }
            return await task;
        }
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
