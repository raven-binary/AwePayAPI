using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AwePayAPI.Models;
using AwePayAPI.Data;

namespace AwePayAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly APIContext _context;
		
		public UserController(APIContext context)
		{
			_context = context;
		}
		
		// Create / Edit
		[HttpPost]
		public JsonResult CreateEdit(Users user)
		{
			if (user.ID == 0)
			{
				_context.Users.Add(user);
			}
			else
			{
				var tempUserDb = _context.UsersDb.Find(user.ID);
				if (tempUserDb == null)
				{return new JsonResult(NotFound())};
				tempUserDb = user;
			}
			_context.SaveChanges();

			return new JsonResult(Ok(user));
		}
		
		// Get
		[HttpGet]
		public JsonResult Get(int ID)
		{
			var result = _context.UsersDb.Find(ID);
			
			if (result == null)
				{return new JsonResult(NotFound())};
			
			return new JsonResult(Ok(result));
		}

		// Delete
		[HttpDelete]
		public JsonResult Delete(int ID)
		{
			var result = _context.UsersDb.Find(ID);
			
			if (result == null)
				{return new JsonResult(NotFound())};
			else
			{
				_context.UsersDb.Remove(result);
				_context.SaveChanges();
			}
			return new JsonResult(NoContent());
		}
		
		// Get all
		[HttpGet()]
		public JsonResult GetAll()
		{
			var result = _context.UsersDb.ToList();
			
			return new JsonResult(Ok(result));
		}
		
	} // class enclosure

} // namespace enclosure
