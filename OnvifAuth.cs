using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using XiaoFeng.Http;

namespace XiaoFeng.Onvif
{
    public class OnvifAuth
    {
        /// <summary>
        /// 远程调用
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="url"></param>
        /// <param name="reqMessageStr"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="onvifUTCDateTime"></param>
        /// <returns></returns>
        public static async Task<HttpResponse> RemoteClient(IPEndPoint iPEndPoint, string url, string reqMessageStr,
            string user, string pass, DateTime onvifUTCDateTime)
        {

            var headToken = GetHeadToken(user, pass, onvifUTCDateTime);
            return await HttpHelper.GetHtmlAsync(new HttpRequest
            {
                HttpCore = HttpCore.HttpClient,
                Method = Http.HttpMethod.Post,
                KeepAlive = false,
                ContentType = "application/xml",
                Address = $"http://{iPEndPoint}/{url}",
                BodyData = $"{EnvelopeHeader()}{headToken}{EnvelopeBody(reqMessageStr)}{EnvelopeFooter()}"
            });
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
            var data = new List<byte[]> {
                    nonce.FromBase64StringToBytes(),
                    createdString.GetBytes()
                  };
            var passByte = password.GetBytes();
            if (passByte != null) data.Add(passByte);

            return new XiaoFeng.Cryptography.SHAEncryption()
                .Encrypt(data.SelectMany(a => a).ToArray(), XiaoFeng.Cryptography.SHAType.SHA1).ToBase64String();
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
            return nonce.ToBase64String();
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
            string created = onvifUTCDateTime.ToString("yyyy-MM-ddThh:mm:ssZ");
            //设置发送消息体
            return $@"
                              <s:Header xmlns:s=""http://www.w3.org/2003/05/soap-envelope"">
                                 <wsse:Security xmlns:wsse=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd"" 
                                                xmlns:wsu=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd""> 
                                           <wsse:UsernameToken> 
                                               <wsse:Username>{username}</wsse:Username>
                                               <wsse:Password Type=""http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-username-token-profile-1.0#PasswordDigest"">{GetPasswordDigest(nonce, created, password)}</wsse:Password>
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
            return $@"  
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
        /// <summary>
        /// 探查消息
        /// </summary>
        /// <returns></returns>
        public static byte[] UdpProbeMessage()
        {
            return $@"<?xml version=""1.0"" encoding=""UTF-8""?>
                <e:Envelope xmlns:e=""http://www.w3.org/2003/05/soap-envelope""
                xmlns:w=""http://schemas.xmlsoap.org/ws/2004/08/addressing""
                xmlns:d=""http://schemas.xmlsoap.org/ws/2005/04/discovery""
                xmlns:dn=""http://www.onvif.org/ver10/network/wsdl"">
                <e:Header>
                      <w:MessageID>uuid:{Guid.NewGuid().ToString("N")}</w:MessageID>
                      <w:To e:mustUnderstand=""true"">urn:schemas-xmlsoap-org:ws:2005:04:discovery</w:To>
                      <w:Action a:mustUnderstand=""true"">http://schemas.xmlsoap.org/ws/2005/04/discovery/Probe</w:Action>
                </e:Header>
                <e:Body>
                <d:Probe>
                         <d:Types>dn:NetworkVideoTransmitter</d:Types>
                </d:Probe>
                </e:Body>
                </e:Envelope>".GetBytes();
        }

        /// <summary>
        /// 统一错误响应
        /// </summary>
        /// <param name="xnode"></param>
        /// <returns></returns>
        public static object ErrorResponse(string xnode)
        {
            return XElement.Parse(xnode).Descendants("Reason").Select(x => x.Element("Text").Value).Cast<string>().FirstOrDefault();
        }
    }

}
