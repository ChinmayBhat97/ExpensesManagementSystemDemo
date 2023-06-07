using Microsoft.AspNetCore.Mvc.Rendering;

namespace DRS.ExpenseManagementSystem.UI.Models
{
    public class UserEnumModel
    {
        public UserEnumModel()
        {
            RoleList = new List<SelectListItem>();
        }

        public int roleID { get; set; }

        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}
