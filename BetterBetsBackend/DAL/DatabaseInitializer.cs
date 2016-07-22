using BetterBetsBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BetterBetsBackend.DAL
{
    public class DatabaseInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<DatabaseContext>
    {
        protected override void Seed(DatabaseContext context)
        {
            var users = new List<User>
            {
                new User { Name = "Lars", Email = "lars.olsson@blackbaud.com", Password = "password", Image =  "http://s3.amazonaws.com/37assets/svn/765-default-avatar.png"}
            };
            users.ForEach(u => context.Users.Add(u));
            context.SaveChanges();
            var organizations = new List<Organization>
            {
                new Organization {Name = "Best Charity", Merchant = "11111111-1111-1111-1111-111111111111" }
            };
            organizations.ForEach(o => context.Organizations.Add(o));
            context.SaveChanges();
        }
    }
}