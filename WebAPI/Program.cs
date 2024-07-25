using System.Text;
using Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

var appConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql(appConnectionString));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	   .AddJwtBearer
	   (
		   options =>
		   {
			   options.TokenValidationParameters = new TokenValidationParameters
			   {
				   ValidIssuer = builder.Configuration["Jwt:Issuer"],
				   ValidAudience = builder.Configuration["Jwt:Audience"],
				   ValidateIssuer = false,
				   ValidateAudience = false,
				   ValidateLifetime = false,
				   ValidateIssuerSigningKey = false,
				   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
			   };
		   }
	   );


builder.Services.AddAuthorization();

builder.Services.AddControllers()
	   .AddJsonOptions
	   (
		   options =>
		   {
			   options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
			   options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
		   }
	   );

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	IServiceProvider serviceProvider = scope.ServiceProvider;
	DatabaseContext context = serviceProvider.GetRequiredService<DatabaseContext>();

	await DatabaseInitializer.Rebuild(context);
}

if (!app.Environment.IsDevelopment())
{
	app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
