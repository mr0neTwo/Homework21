namespace Application.Models;

public sealed class Role
{
	public int Id { get; set; }
	public string Name { get; set; }
	public Permission[] Permissions { get; set; }
}
