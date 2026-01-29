using App.Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace App.Core.Entities.Universities;
public class University
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Website { get; set; } = string.Empty;

    public int YearOfEstablishment { get; set; }
    public bool IsDeleted { get; set; }
    public ICollection<Faculty> Faculties { get; set; } = new HashSet<Faculty>();
    public ICollection<ApplicationUser> Users { get; set; } = new HashSet<ApplicationUser>();
    public ICollection<ApplicationRole> Roles { get; set; } = new HashSet<ApplicationRole>();
}
