using System.Security.Cryptography;
using System.Text;
using XiaoFeng.Xml;

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
        public static async Task<Http.HttpResponse> RemoteClient(string ip, string url, string reqMessageStr,
            string user, string pass, DateTime onvifUTCDateTime)
        {
            var headToken = GetHeadToken(user, pass, onvifUTCDateTime);
            return await Http.HttpHelper.GetHtmlAsync(new Http.HttpRequest
            {
                Method = HttpMethod.Post.ToString(),
                ContentType = "application/xml",
                Address = $"http://{ip}/{url}",
                BodyData = $"{EnvelopeHeader()}{headToken}{EnvelopeBody(reqMessageStr)}{EnvelopeFooter()}"
            });
        }

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
        /// 获取加密后的字符串
        /// </summary>
        /// <param name="nonce"></param>
        /// <param name="createdString"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GetPasswordDigest(string nonce, string createdString, string password)
        {
           // byte[] combined = buildBytes(nonce, createdString, password);
            //string output = Convert.ToBase64String(HashAlgorithm.Create("SHA1").ComputeHash(combined));
            return new XiaoFeng.Cryptography.SHAEncryption().Encrypt(new List<byte[]> { nonce.FromBase64StringToBytes(), createdString.GetBytes(), password.GetBytes() }.SelectMany(a => a).ToArray(), Cryptography.SHAType.SHA1).ToBase64String();
            //return output;
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
            string created = onvifUTCDateTime.ToString("yyyy-MM-ddThh:mm:ssZ");
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
