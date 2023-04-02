using Microsoft.Build.Framework;

namespace Identity.ViewModel
{
    public class RoleViewModel
    {

        public string? RoleID { get; set; }
        [Required]
        public string? RoleName { get; set; }
    }
}
