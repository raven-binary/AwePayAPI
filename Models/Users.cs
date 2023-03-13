using Redis.OM.Modeling;

namespace AwePayAPI.Models;

[Document]
public class Users
	{
       [RedisIdField] [Indexed] public int ID {get; set;}
        [Indexed] public string? Fullname {get; set;}
        [Searchable(Sortable = true)] public string? Email {get; set;}
        [Searchable(Sortable = true)] public int Phone {get; set;}
        [Indexed] public int Age {get; set;}
	}
