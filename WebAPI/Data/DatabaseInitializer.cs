using Application.Models;
using Database;
using Database.Data;

namespace WebAPI.Data;

public class DatabaseInitializer
{
	public static async Task Rebuild(DatabaseContext context)
	{
		await context.Database.EnsureDeletedAsync();
		await context.Database.EnsureCreatedAsync();

		await context.Notes.AddRangeAsync(DefaultValues.Notes());

		Role adminRole = DefaultValues.AdminRole;
		Role userRole = DefaultValues.UserRole;
		
		await context.Roles.AddAsync(adminRole);
		await context.Roles.AddAsync(userRole);
		
		await context.SaveChangesAsync();

		User admin = DefaultValues.Admin;
		admin.RoleId = adminRole.Id;
		
		User user = DefaultValues.User;
		user.RoleId = userRole.Id;

		await context.Users.AddRangeAsync(admin);
		await context.Users.AddRangeAsync(user);

		await context.SaveChangesAsync();
	}
}
