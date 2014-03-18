namespace Hisco.Security
{
    using System.Configuration;
    using System.Security.Cryptography;
    using System.Text;
    using WebGrease.Css.Extensions;

    public class BasicSecurity : ISecurity
    {
        private readonly string _hashKey = ConfigurationManager.AppSettings["hashKey"];

        public string GenerateHash(string[] keys)
        {
            var keyBuilder = new StringBuilder();
            keys.ForEach(x => keyBuilder.Append(x));
            keyBuilder.Append(_hashKey);

            var md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(keyBuilder.ToString()));

            var hashBuilder = new StringBuilder();
            foreach (byte t in data)
                hashBuilder.Append(t.ToString("x2"));

            return hashBuilder.ToString();
        }
    }
}