using Microsoft.EntityFrameworkCore;
using AwePayAPI.Models;

namespace AwePayAPI.Data.APIContext;

public class APIContext : DbContext
{
	public DbSet<Users> UsersDb {get; set;} //UsersDb is the name of the in-memory database

	public APIContext(DbContextOptions<APIContext> options)
		:base(options)
	{
			
	}
}


