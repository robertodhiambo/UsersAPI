# UsersAPI
This is a project to create, get, update and delete users implemented using
.Net Core 3.1 Web API and the Entity Framework for managing the connection to the database.

To run the project you'll need to have Visual Studio 2019 Installed and MS SQL 2017 running. 
Change the server name the database in the application.json file to match your local server name before running the project.
To get all users using Postman we use api/Users [HttpGet]
To get a single user using a specific id we use api/Users/5 [HttpGet({"id"})]
To add or post a new user we use api/Users [HttpPost]
To update or put a user information we use api/Users [HttpPut]
To a delete a user using a specific id we use api/Users/5 [HttpDelete]
