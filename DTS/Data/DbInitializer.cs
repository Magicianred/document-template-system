using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTS.Models;

namespace DTS.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AppContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            var templateStates = new TemplateState[]
            {
                new TemplateState{State = "Active"},
                new TemplateState{State = "Inactive"}
            };

            foreach (var templateState in templateStates)
            {
                context.TemplateStates.Add(templateState);
            }

            var types = new UserType[]
            {
                new UserType{Type = "Admin"},
                new UserType{Type = "Editor"},
                new UserType{Type = "User"}
            };

            foreach (var type in types)
            {
                context.Types.Add(type);
            }

            context.SaveChanges();

            var statuses = new UserStatus[]
            {
                new UserStatus{Status = "Active"},
                new UserStatus{Status = "Blocked"},
                new UserStatus{Status = "Suspended"}
            };

            foreach (var status in statuses)
            {
                context.Statuses.Add(status);
            }


            var users = new User[]
            {
                new User{Name ="Bartek",Surname ="Zadlo",Email ="bZadlo@DTS.com", Login="BZadlo", Password = "test"},
                new User{Name ="Magda",Surname ="Kiebala",Email ="mKiebala@DTS.com", Login="MKiebala", Password = "test"},
                new User{Name ="Piotrek",Surname ="Kaminski",Email ="pKaminski@DTS.com", Login="PKaminski", Password = "test"},

            };
            foreach (var user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }
}
