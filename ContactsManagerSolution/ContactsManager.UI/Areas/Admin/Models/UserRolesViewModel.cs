using ContactsManager.Core.Domain.IdentityEntities;

namespace ContactsManager.UI.Areas.Admin.Models
{
    public class UserRolesViewModel
    {
        public ApplicationUser? CurrentUser { get; set; }

        public IEnumerable<string>? Roles { get; set; }
    }
}
