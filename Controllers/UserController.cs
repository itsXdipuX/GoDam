using GoDam.Data;
using GoDam.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoDam.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly ApplicationDbContext _context;


        public UserController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            _context = context;
        }

        public IActionResult Index()
        {
            List<UserViewModel> users = new List<UserViewModel>();

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"select u.Id,u.Email,r.Name Role,u.PhoneNumber from AspNetUsers u 
                                        left join AspNetUserRoles ur 
                                        on ur.UserId = u.Id 
                                        join AspNetRoles r
                                        on r.Id = ur.RoleId
                                        ;";

                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    UserViewModel data;
                    while (result.Read())
                    {
                        data = new UserViewModel();
                        data.Id = result.GetString(0);
                        data.Email = result.GetString(1);
                        data.Role = result.GetString(2);
                        data.Phone = result.GetString(3);
                        users.Add(data);
                    }
                }
            }
            return View(users);
        }

        public async Task<IActionResult> ChangePass(string Id)
        {
            List<UserViewModel> users = new List<UserViewModel>();

            if (Id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(Id);
            if (user == null)
            {
                return NotFound();
            }

            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = @"select u.Id,u.Email,r.Name Role,u.PhoneNumber from AspNetUsers u 
                                        join AspNetUserRoles ur 
                                        on ur.UserId = u.Id 
                                        join AspNetRoles r
                                        on r.Id = ur.RoleId
                                        where u.Id= '" + Id + "';";
                               

                _context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    UserViewModel data;
                    while (result.Read())
                    {
                        data = new UserViewModel();
                        data.Id = result.GetString(0);
                        data.Email = result.GetString(1);
                        data.Role = result.GetString(2);
                        data.Phone = result.GetString(3);
                        users.Add(data);
                    }
                }
            }
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePass(string Id, string NewPassword)
        {

            var user = await userManager.FindByIdAsync(Id);
            var removePassword = await userManager.RemovePasswordAsync(user);
            if (removePassword.Succeeded)
            {
                //Removed Password Success
                var AddPassword = await userManager.AddPasswordAsync(user, NewPassword);
                if (AddPassword.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }


        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            var rolesList = await userManager.GetRolesAsync(user).ConfigureAwait(false);
            var r = rolesList[0];
            //var userRoleId = await roleManager.GetRoleIdAsync(r);
            if (user == null)
            {
                return NotFound();
            }

            UserViewModel u = new UserViewModel();
            u.Id = user.Id;
            u.Email = user.UserName;
            u.Phone = user.PhoneNumber;
            //u.Email = user.Email;
            u.Role = r;
            //user role

            ViewData["RoleId"] = new SelectList(_context.Roles, "Name", "Name");

            return View(u);
        }

        [HttpPost]
        public async Task<IActionResult> EditAsync(string Id, [Bind("Id,Phone,Email,Role")] UserViewModel user)
        {
            if (Id != user.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    using (var command = _context.Database.GetDbConnection().CreateCommand())
                    {
                        command.CommandText = @"update AspNetUsers set PhoneNumber = '" + user.Phone + "' where Id = '" + user.Id + "';";
                        _context.Database.OpenConnection();
                        using (var result = command.ExecuteReader())
                        {
                            //var role= await _context.Roles.FindAsync(user.Role);
                            var currentUser = await userManager.FindByIdAsync(user.Id);

                            //var oldRole = await roleManager.FindByIdAsync(user.Id); 

                            var oldRole = await userManager.GetRolesAsync(currentUser);
                            foreach (string r in oldRole)
                            {
                                var restult = await userManager.RemoveFromRoleAsync(currentUser, r);
                            }
                            //await userManager.RemoveFromRoleAsync(currentUser, role.Name);

                            var roleResult = await userManager.AddToRoleAsync(currentUser, user.Role);
                        }
                    }
                }
                catch (Exception)
                {

                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

    }
}
