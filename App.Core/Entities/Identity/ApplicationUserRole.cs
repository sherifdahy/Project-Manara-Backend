using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities.Identity;

public class ApplicationUserRole : IdentityUserRole<int>
{
    public ApplicationUser User { get; set; } = default!;
    public ApplicationRole Role { get; set; } = default!;
}
