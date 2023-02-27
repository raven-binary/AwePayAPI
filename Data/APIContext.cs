using Microsoft.EntityFrameworkCore;
using AwePayAPI.Models;

namespace AwePayAPI.Data

{
	public class APIContext : DbContext
	{
		public DbSet<AwePayAPI> UsersDb {get; set;} //UsersDb is the name of the in-memory database

		public APIContext(DbContextOptions<APIContext> options)
			:base(options)
		{
			
		}
	}

}
