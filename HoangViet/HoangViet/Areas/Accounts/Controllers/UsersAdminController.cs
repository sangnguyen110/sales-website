using HoangViet.Areas.Admin.Controllers;
using HoangViet.Models.Accounts;
using HoangViet.ViewModels.Accounts;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HoangViet.Areas.Accounts.Controllers
{

    public class UsersAdminController : BaseAdminController
    {
        public UsersAdminController()
        {
        }

        public UsersAdminController(HoangVietUserManager userManager, HoangVietRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        private HoangVietUserManager _userManager;
        public HoangVietUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<HoangVietUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private HoangVietRoleManager _roleManager;
        public HoangVietRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<HoangVietRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        //
        // GET: /Users/
        public async Task<ActionResult> Index(int? page)
        {
            var users = await UserManager.Users.Include(u => u.Roles).ToListAsync();
            foreach (var u in users)
            {
                var rolesName = await u.GetRolesNameAsync(this.UserManager);
                u.RolesName = rolesName;
            }
            var pageNumber = page ?? 1;
            var onePageUsers = users.ToPagedList(pageNumber, 10);
            return View(onePageUsers);
        }

        //
        // GET: /Users/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);

            return View(user);
        }

        //
        // GET: /Users/Create
        public async Task<ActionResult> Create()
        {
            var roles = await RoleManager.Roles.ToListAsync();
            //Get the list of Roles
            //    ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            return View(new RegisterViewModel()
            {
                RolesList = roles.Select(x => new SelectListItem()
                {
                    Selected = false,
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        //
        // POST: /Users/Create
        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = new HoangVietUser
                {
                    UserName = userViewModel.Email,
                    Email = userViewModel.Email,
                    Name = userViewModel.Name,
                    Sex = userViewModel.Sex,
                    DateOfBirth = DateTime.ParseExact(userViewModel.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    CompanyName = userViewModel.CompanyName,
                    FaxNumber = userViewModel.FaxNumber,
                    TaxCode = userViewModel.TaxCode,
                    PhoneNumber = userViewModel.PhoneNumber,
                    Website = userViewModel.Website,
                    Disabled = userViewModel.Disabled
                };
                var adminresult = await UserManager.CreateAsync(user, userViewModel.Password);

                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await UserManager.AddUserToRolesAsync(user.Id, selectedRoles);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            userViewModel.RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                              {
                                  Selected = selectedRoles.Contains(x.Name),
                                  Text = x.Name,
                                  Value = x.Name
                              });
                            //   ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                            return View(userViewModel);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First());
                    if (selectedRoles == null) selectedRoles = new string[] { };
                    userViewModel.RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                    {
                        Selected = selectedRoles.Contains(x.Name),
                        Text = x.Name,
                        Value = x.Name
                    });
                    //   ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
                    return View(userViewModel);

                }
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
            return View();
        }

        //
        // GET: /Users/Edit/1
        public async Task<ActionResult> Edit(string id, int? orderId)
        {
            ViewBag.OrderId = orderId;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);
            var registerViewModel = new EditUserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Name = user.Name,
                    Sex = user.Sex,
                    DateOfBirth = user.DateOfBirth.ToString("dd/MM/yyyy"),
                    CompanyName = user.CompanyName,
                    FaxNumber = user.FaxNumber,
                    TaxCode = user.TaxCode,
                    PhoneNumber = user.PhoneNumber,
                    Website = user.Website,
                    Disabled = user.Disabled,
                    RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                    {
                        Selected = userRoles.Contains(x.Name),
                        Text = x.Name,
                        Value = x.Name
                    })
                };
            return View(registerViewModel);
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditUserViewModel editUserViewModel, int? orderId, params string[] selectedRole)
        {
            ViewBag.OrderId = orderId;
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUserViewModel.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.Email = editUserViewModel.Email;
                user.Name = editUserViewModel.Name;
                user.Sex = editUserViewModel.Sex;
                user.DateOfBirth = DateTime.ParseExact(editUserViewModel.DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                user.CompanyName = editUserViewModel.CompanyName;
                user.FaxNumber = editUserViewModel.FaxNumber;
                user.TaxCode = editUserViewModel.TaxCode;
                user.PhoneNumber = editUserViewModel.PhoneNumber;
                user.Website = editUserViewModel.Website;
                user.Disabled = editUserViewModel.Disabled;

                var userRoles = await UserManager.GetRolesAsync(user.Id);

                selectedRole = selectedRole ?? new string[] { };

                var result = await UserManager.AddUserToRolesAsync(user.Id, selectedRole.Except(userRoles).ToList<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await UserManager.RemoveUserFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToList<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        //
        // GET: /Users/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                var user = await UserManager.FindByIdAsync(id);
                if (user == null)
                {
                    return HttpNotFound();
                }
                var result = await UserManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
