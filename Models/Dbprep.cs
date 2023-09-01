using Microsoft.EntityFrameworkCore;
using webapp.Data;

//Data seed, create database if it doesn't exist
namespace webapp.Models{
    public static class DbPrep{
        public static void Preparation(IApplicationBuilder builder){
            using (var serviceScope = builder.ApplicationServices.CreateScope()){
                Seed(serviceScope.ServiceProvider.GetService<DatabaseContext>());
            }
        }

        public static void Seed(DatabaseContext context){
            context.Database.Migrate();
        }
    }
}