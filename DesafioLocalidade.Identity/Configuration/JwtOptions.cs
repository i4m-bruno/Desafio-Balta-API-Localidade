using Microsoft.IdentityModel.Tokens;

namespace DesafioLocalidade.Identity.Configuration
{
    public class JwtOptions
   {
        public JwtOptions()
        {
            
        }
        public JwtOptions(string issuer, string audience, SigningCredentials signingCredentials, int expiration)
        {
            Issuer = issuer;
            Audience = audience;
            SigningCredentials = signingCredentials;
            Expiration = expiration;
        }

        public string Issuer { get; set; }
       public string Audience { get; set; }
       public SigningCredentials SigningCredentials { get; set; }
       public int Expiration { get; set; }
    }
}
