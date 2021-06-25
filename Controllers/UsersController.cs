using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UsersAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UsersAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
      
        readonly UserDBContext dBContext;
        readonly MD5 md5Encryptor = MD5.Create();
        //private object dataRepository;

        
        public UsersController(UserDBContext _dBContext)
        {
            dBContext = _dBContext;
        }
        // GET: api/Users
        [HttpGet]
        public List<User> Get()
        {
            List<User> users = new List<User>();
            foreach(User user in users)
            {
                user.Password = hashPassword(user.Password);  
            }
            
            return dBContext.Users.ToList();
        }

        // GET api/Users/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

           User user = dBContext.Users.FirstOrDefault(x => x.UserId == id);

            //List<String> errors = ValidateModel(user);
            //if (errors.Any())
            //{
            //    return BadRequest(errors);
            //}
          
            if(user == null)
            {
                return NotFound("User with the id  Not found");
            }
            user.Password = hashPassword(user.Password);
            return Ok(user);

        }

        // POST api/Users
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            List<string> errors = ValidateModel(user);
            if (errors.Any()) {
                return BadRequest(errors);
            }

            user.Password = hashPassword(user.Password);
            dBContext.Users.Add(user);
             dBContext.SaveChanges();

            return Ok("User added successfully");
        }

        List<string> ValidateModel(User model)
        {
            List<string> errors = new List<string>();
            if(string.IsNullOrEmpty(model.FirstName)) {
                errors.Add("First Name is required");
            }

            if (string.IsNullOrEmpty(model.LastName))
            {
                errors.Add("Last Name is required");
            }

            if (string.IsNullOrEmpty(model.Email))
            {
                errors.Add("Email is required");
            }

            if (string.IsNullOrEmpty(model.PhoneNumber))
            {
                errors.Add("PhoneNumber is required");
            }

            if (string.IsNullOrEmpty(model.Password))
            {
                errors.Add("Password is required");
            }
            return errors;
        }

        // PUT api/Users
        [HttpPut]
        public IActionResult Put([FromBody] User userInfo)

        {

            List<string> errors = ValidateModel(userInfo);
            if (errors.Any())
            {
                return BadRequest(errors);
            }

            User user = dBContext.Users.FirstOrDefault(u => u.UserId == userInfo.UserId);
            if(user == null)
            {
                return NotFound("User with id " + userInfo.UserId + " Not found");
            }
            user.FirstName = userInfo.FirstName;
            user.LastName = userInfo.LastName;
            user.Email = userInfo.Email;
            user.PhoneNumber = userInfo.PhoneNumber;

            user.Password = hashPassword(userInfo.Password);

            dBContext.Users.Update(user);
            dBContext.SaveChanges();

            return Ok("User updated successfully");
        }

        private string hashPassword(string password)
        {
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            byte [] passwordHashBytes = shaEncryptor.ComputeHash(passwordBytes);
            return Encoding.ASCII.GetString(passwordHashBytes);
        }

        // DELETE api/Users/5
        [HttpDelete("{id}")]
        public IActionResult Delete ([FromBody] User userInfo)
        {
            User user = dBContext.Users.FirstOrDefault(d => d.UserId == userInfo.UserId);
            if (user == null) 
            {
                return NotFound("User with id " + userInfo.UserId + "Not found");

            }

             dBContext.Remove(user);
            dBContext.SaveChanges();
            return Ok("Deleted successfully");
        }
    }
}
