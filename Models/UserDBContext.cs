using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersAPI.Models
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        internal User Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
