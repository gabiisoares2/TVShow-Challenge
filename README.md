# TVShow-Challenge
Api create to introduce my code for a challenge

In this application I use: 

- .Net 6
- EntityFrameworkCore - CodeFirst
- Identity
- Dependency Injection
- Swagger
- Authentication JWT
- WebAPI

I'm apply concepts by SOLID, and arquitecture DDD. To Database I was used SQLServer, when you execute VS the base will be create,
and to populate database I'm create a service to consuming the informations to https://www.episodate.com/api, 
so when you execute the system and doesn't have any datas, the program will populate.
Please create a new User, and do a login to get a Token, next put the authentication with "Bearer {token}", after that you are able to use the application,
if you want finish the session, use logout.
To add a new favourite, set a list of Ids(GUID) of FilmShow, to finished set a list of Ids too.

Thanks and regards Gabriela Soares.
