using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenith_MAUI.Business.DTO;

namespace Zenith_MAUI.Common
{
    public static class SecureStorageExtensions
    {
        public static Business.DTO.User GetUser(this ISecureStorage storage)
        {
            var token = Task.Run(async () => await storage.GetAsync("token")).Result;

            if (token == null)
            {
                return null;
            }

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = jsonToken as JwtSecurityToken;

            var id = tokenS.Claims.First(claim => claim.Type == "Id").Value;
            var username = tokenS.Claims.First(claim => claim.Type == "Username").Value;
            var exp = tokenS.Claims.First(x => x.Type == "exp").Value;

            long expTimestamp = long.Parse(exp);

            DateTime d = UnixTimeStampToDateTime(expTimestamp);

            if (d < DateTime.Now)
            {
                SecureStorage.Default.Remove("token");
                return null;
            }

            return new Business.DTO.User { Id = int.Parse(id), Token = token, Username = username };
        }

        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTime;
        }

    }
}
