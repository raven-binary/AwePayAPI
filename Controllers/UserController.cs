using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AwePayAPI.Models;
using AwePayAPI.Data.APIContext;
using Redis.OM;
using Redis.OM.Searching;
using System.Numerics;
using Microsoft.EntityFrameworkCore;
using System;

namespace AwePayAPI.Controllers.User;

[Route("api/[controller]/[action]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly APIContext _context; // In-memory Database
    private readonly RedisCollection<Users> _people; // For Redisearch
    private readonly RedisConnectionProvider _provider; // Singleton Connection over here

    public UserController(APIContext context, RedisConnectionProvider provider) // Constructor
    {
        _context = context;
        _provider = provider;
        _people = (RedisCollection<Users>)provider.RedisCollection<Users>();
    }

    // Create Users
    [HttpPost]
    public JsonResult Create(Users usr)
    {
        if (usr.ID == 0)
        {
            _context.UsersDb.Add(usr); // In-Memory DB
            _ = _people.InsertAsync(usr); // Redisearch
            Console.WriteLine("User " + usr.Fullname + " was created with ID: " + usr.ID);
            _context.SaveChanges();
            return new JsonResult(Ok(usr));
        }

        else
        {
            Console.WriteLine("User cannot be assigned custom ID, as it is auto-generated.!");
            return new JsonResult(NotFound());
        }

    }

    // Edit
    [HttpPut]
    public JsonResult Edit(int ID, string fullname, string email, int phone, int age)
    {
        if (ID <= 0)
        {
            Console.WriteLine("User ID must be greater than 0");
            return new JsonResult(NotFound());
        }

        else if (ID > 0)
        {
            var tempstr = _context.UsersDb.Find(ID);
            var tempstrRedis = _people.Where(x => x.ID == ID);

            if (tempstr != null || tempstrRedis != null)
            {
                if (tempstr != null)
                {
                    tempstr.Fullname = fullname;
                    tempstr.Email = email;
                    tempstr.Phone = phone;
                    tempstr.Age = age;
                    _context.UsersDb.Update(tempstr);
                    _context.SaveChanges();
                    Console.WriteLine("User " + tempstr.ID + " was edited.!");
                    //return new JsonResult(Ok(tempstr));
                }

                if (tempstrRedis != null)
                {
                    foreach (var person in _people.Where(x => x.ID == ID))
                    {
                        person.Fullname = fullname;
                        person.Email = email;
                        person.Phone = phone;
                        person.Age = age;
                    }
                    _people.Save();
                }
            }
            return new JsonResult(Ok(tempstrRedis));
        }

        else { return null; }
    }

    // Delete
    [HttpDelete]
    public JsonResult Delete(int ID)
    {
        var result = _context.UsersDb.Find(ID);

        if (result == null)
            return new JsonResult(NotFound());

        _context.UsersDb.Remove(result); // In-Memory DB
        _provider.Connection.Unlink($"AwePayAPI.Models.Users:{ID.ToString()}"); // Redisearch
        Console.WriteLine("User with" + ID + " was deleted.!");
        _context.SaveChanges();

        return new JsonResult(NoContent());
    }

    // Get by Email
    [HttpGet("Email")]
    public JsonResult? Get(String Email)
    {

        if (Email != null)
        {
            var resultRedisEmail = _people.Where(x => x.Email == Email).OrderByDescending(x => x.Email).ToList(); // Redisearch

            return new JsonResult(Ok(resultRedisEmail));
        }

        else if (Email == null)
            return new JsonResult(NotFound());

        else { return null; }

    }

    // Get by Phone
    [HttpGet("Phone")]
    public JsonResult? Get(int Phone)
    {
        if (Phone != null)
        {
            var resultRedisPhone = _people.Where(x => x.Phone == Phone).OrderByDescending(x => x.Phone).ToList(); // Redisearch
            return new JsonResult(Ok(resultRedisPhone));
        }

        else if (Phone == null)
            return new JsonResult(NotFound());

        else { return null; }
    }

}