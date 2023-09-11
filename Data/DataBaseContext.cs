using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using webapp.Models;

namespace webapp.Data{
    
    public class DatabaseContext : DbContext{
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base (options)
        {

        }

        public DbSet<PolicyModel> Policies {get; set;}
    }
}

