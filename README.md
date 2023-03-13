# AwePayAPI

### While searching for possible languages use to code REST API w/ CRUD operations,
### C# was used due to its versatility and sheer complexity.

### The following are packages used
1. "Microsoft.EntityFrameworkCore.InMemory" Version="7.0.3"
2. "Redis.OM" Version="0.4.1"
3. "Swashbuckle.AspNetCore" Version="6.2.3"

### As for database, both In-Memory and Redisearch were used.

### Redisearch were used to make sure database is persisted and can be used throughout a vast majority of applications
### Redisearch also provides Machine Learning pipelines as it allows Full-Text search on hashes, and it is very fast
### Therefore, many companies out there if they are dealing with payment, have to consider:
#### 1. Anti Money Laundering
#### 2. KYC - Know Your Customer
### Hence, Redisearch helps a lot here.

### The timeline of this project roughly took 2 weeks ~ and I can assure that all logics were done very quickly (can be checked via Github commit history)
#### 1. This project has 2 live cloud nodes:
#### A. Redisearch hosted via Linus Virtual Machine
#### B. REST endpoints hosted via Azure App Service [https://newawepayapi20230313092205.azurewebsites.net/]

#### The configuration of the cloud premises took the most time of this project.
#### Overally, wanted to make sure the best reaches AwePay, as I believe just like Ferrari builds its seat around drivers, specifics really matter.
