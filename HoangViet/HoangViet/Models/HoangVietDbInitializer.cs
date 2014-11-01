using HoangViet.Models.Accounts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Data.Entity;
using System.Web;

namespace HoangViet.Models
{
    // This is useful if you do not want to tear down the database each time you run the application.
    // public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    // This example shows you how to create a new database if the Model changes
    public class HoangVietDbInitializer : DropCreateDatabaseIfModelChanges<HoangVietDbContext>
    {
        protected override void Seed(HoangVietDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        //Create User=sangnguyen110@yahoo.com.vn with password=Mhbbnsbt@123 in the Admin role        
        public static void InitializeIdentityForEF(HoangVietDbContext db)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<HoangVietUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<HoangVietRoleManager>();
            const string name = "sangnguyen110@yahoo.com.vn";
            const string password = "Mhbbnsbt@123";
            const string roleName = "Admin";

            //Create Role Admin if it does not exist
            var role = roleManager.FindByName(roleName);
            if (role == null)
            {
                role = new IdentityRole(roleName);
                var roleresult = roleManager.Create(role);
            }

            var user = userManager.FindByName(name);
            if (user == null)
            {
                user = new HoangVietUser { UserName = name, Email = name, Name = "Nguyễn Thanh Sang", Sex = Gender.Male, PhoneNumber = "01227970361" };
                var result = userManager.Create(user, password);
                result = userManager.SetLockoutEnabled(user.Id, false);
            }

            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(role.Name))
            {
                var result = userManager.AddToRole(user.Id, role.Name);
            }

            for (int i = 0; i < 4; i++)
            {
                var tempUser = userManager.FindByName(string.Format("User{0}@yahoo.com", i.ToString()));
                if (tempUser == null)
                {
                    tempUser = new HoangVietUser()
                    {
                        UserName = string.Format("User{0}@yahoo.com", i.ToString()),
                        Name = string.Format("Name{0}", i.ToString()),
                        Email = string.Format("User{0}@yahoo.com", i.ToString()),
                        CompanyName = string.Format("Company {0}", i.ToString()),
                        DateOfBirth = new DateTime(1990 + i, 5 + i, 13 + i),
                        FaxNumber = "3722295" + i,
                        PhoneNumber = "01227970361" + i,
                        Sex = Gender.Female
                    };

                    userManager.Create(tempUser, string.Format("Password@{0}", i.ToString()));
                }
            }
        }
    }
}