using Microsoft.EntityFrameworkCore;
using AwePayAPI.Models;

namespace AwePayAPI.Data

{
	public class APIContext : DbContext
	{
		public DbSet<AwePayAPI> Users {get; set;}		

		public APIContext(DbContextOptions<APIContext> options)
			:base(options)
		{
			
		}
	}

}
