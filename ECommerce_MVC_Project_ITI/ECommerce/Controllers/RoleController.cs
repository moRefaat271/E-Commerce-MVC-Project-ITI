using Identity.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Identity.Controllers
{
    public class RoleController : Controller
    {
        public readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> _roleManager)
        {
            roleManager= _roleManager;
        }

       //[Route("")]
        public IActionResult AddRole()
        {
            ViewBag.lst = new List<SelectListItem> {
                new SelectListItem { Value = "Admin", Text = "Admin" },
                new SelectListItem { Value = "Seller", Text = "Seller" },
                new SelectListItem { Value = "User", Text = "User" }
            };
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> AddRole(IFormCollection roleInput)
        {
            ViewBag.lst = new List<SelectListItem> {
                new SelectListItem { Value = "Admin", Text = "Admin" },
                new SelectListItem { Value = "Seller", Text = "Seller" },
                new SelectListItem { Value = "User", Text = "User" }
            };
            if (ModelState.IsValid)
            {
                 IdentityRole Role = new IdentityRole() { Name = roleInput["RoleName"] };
                 IdentityResult result=await  roleManager.CreateAsync(Role);
                if(result.Succeeded)
                {
                    return View();
                }
                else
                {
                    foreach (var i in result.Errors)
                    {
                        ModelState.AddModelError("", i.Description);
                    }
                }
            }

            return View(roleInput);
        }
    }
}
