using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Data;

public class AuthOptions
{
	public const string ISSUER = "MyAuthServer";                
	public const string AUDIENCE = "MyAuthClient";               
	public const string KEY = "mysupersecret_secretsecretsecretkey!123";
	
	public static SymmetricSecurityKey GetSymmetricSecurityKey() => new(Encoding.UTF8.GetBytes(KEY));
}
