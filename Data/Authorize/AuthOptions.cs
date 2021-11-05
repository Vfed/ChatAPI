using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace ChatAPI.Data.Authorize
{
    public class AuthOptions
    {
        public const string ISSUER = "ChatApiServer"; 
        public const string AUDIENCE = "ChatApiClient";
        const string KEY = "123mysupersecret_secretkey!55321";
        public const int LIFETIME = 5; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
