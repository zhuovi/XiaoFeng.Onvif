using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XiaoFeng.Onvif
{
    public class OnvifAuth
    {
        /// <summary>
        /// 获取加密的字节数组
        /// </summary>
        /// <param name="nonce"></param>
        /// <param name="createdString"></param>
        /// <param name="basedPassword"></param>
        /// <returns></returns>
        private static byte[] buildBytes(string nonce, string createdString, string basedPassword)
        {
            byte[] nonceBytes = System.Convert.FromBase64String(nonce);
            byte[] time = Encoding.UTF8.GetBytes(createdString);
            byte[] pwd = Encoding.UTF8.GetBytes(basedPassword);

            byte[] operand = new byte[nonceBytes.Length + time.Length + pwd.Length];
            Array.Copy(nonceBytes, operand, nonceBytes.Length);
            Array.Copy(time, 0, operand, nonceBytes.Length, time.Length);
            Array.Copy(pwd, 0, operand, nonceBytes.Length + time.Length, pwd.Length);

            return operand;
        }

        /// <summary>
        /// 计算指定字节数组的哈希值。
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] SHAOneHash(byte[] data)
        {
            return HashAlgorithm.Create("SHA1").ComputeHash(data);
        }

        /// <summary>
        /// 获取加密后的字符串
        /// </summary>
        /// <param name="nonce"></param>
        /// <param name="createdString"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GetPasswordDigest(string nonce, string createdString, string password)
        {
            byte[] combined = buildBytes(nonce, createdString, password);
            string output = System.Convert.ToBase64String(SHAOneHash(combined));
            return output;
        }

        /// <summary>
        /// 加密的时间戳
        /// </summary>
        /// <param name="onvifUTCDateTime">从onvif获取的世间</param>
        /// <returns></returns>
        public static string GetCreated(DateTime onvifUTCDateTime)
        {
            return onvifUTCDateTime.ToString("yyyy-MM-ddThh:mm:ssZ");
        }

        /// <summary>
        /// 将随机的16位字节数据加密成nonce
        /// </summary>
        /// <param name="nonce"></param>
        /// <returns></returns>
        public static string GetNonce()
        {
            byte[] nonce = new byte[16];
            new Random().NextBytes(nonce);
            return System.Convert.ToBase64String(nonce);
        }

        /// <summary>
        /// 获取加密后的头部
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="onvifUTCDateTime"></param>
        /// <returns></returns>
        public static string GetHeadToken(string username, string password, DateTime onvifUTCDateTime)
        {
            string nonce = GetNonce();
            string created = GetCreated(onvifUTCDateTime);
            string pass = GetPasswordDigest(nonce, created, password);
            //设置发送消息体
            return $@"
                              <s:Header xmlns:s=""http://www.w3.org/2003/05/soap-envelope"">
                                 <wsse:Security xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" 
                                                xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd""> 
                                           <wsse:UsernameToken> 
                                               <wsse:Username>{username}</wsse:Username>
                                               <wsse:Password Type=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordDigest"">{pass}</wsse:Password>
                                               <wsse:Nonce>{nonce}</wsse:Nonce> 
                                               <wsu:Created>{created}</wsu:Created>
                                           </wsse:UsernameToken>
                                 </wsse:Security>
                              </s:Header>";
        }
        /// <summary>
        /// 信封头部
        /// </summary>
        /// <returns></returns>
        public static string EnvelopeHeader()
        {
            return @"<s:Envelope xmlns:s=""http://www.w3.org/2003/05/soap-envelope"" xmlns:tds=""http://www.onvif.org/ver10/device/wsdl"" xmlns:tt=""http://www.onvif.org/ver10/schema"">";
        }
        /// <summary>
        /// 信封内容
        /// </summary>
        /// <param name="bodyContent"></param>
        /// <returns></returns>
        public static string EnvelopeBody(string bodyContent)
        {
            return @$"  
                              <s:Body>
                                 {bodyContent}  
                             </s:Body>";
        }
        /// <summary>
        /// 信封尾部
        /// </summary>
        /// <returns></returns>
        public static string EnvelopeFooter()
        {
            return @"
                     </s:Envelope> "; 
        }
    }

}
