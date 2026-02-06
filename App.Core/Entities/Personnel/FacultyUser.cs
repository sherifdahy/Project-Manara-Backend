using App.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Core.Entities.Personnel;

public class FacultyUser
{
    public int UserId { get; set; }
    public int FacultyId { get; set; }

    public Faculty Faculty { get; set; } = default!;
    public ApplicationUser User { get; set; } = default!;

}
