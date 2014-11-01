using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace HoangViet.Models.Accounts
{
    // Configure the RoleManager used in the application. RoleManager is defined in the ASP.NET Identity core assembly
    public class HoangVietRoleManager : RoleManager<IdentityRole>
    {
        public HoangVietRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static HoangVietRoleManager Create(IdentityFactoryOptions<HoangVietRoleManager> options, IOwinContext context)
        {
            var manager = new HoangVietRoleManager(new RoleStore<IdentityRole>(context.Get<HoangVietDbContext>()));

            return manager;
        }
    }
}