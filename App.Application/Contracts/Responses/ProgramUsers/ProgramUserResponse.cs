using App.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.ProgramUsers;

public record ProgramUserResponse
{
    public int Id { get; set; }
    public int ProgramId { get; set; }
    public int DepartmentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string NationalId { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsDisabled { get; set; }
    public bool IsDeleted { get; set; }
    public List<string> Roles { get; set; } = [];
    public DateOnly BirthDate { get; set; }
    public Gender Gender { get; set; }
    public Religion Religion { get; set; }
    public string PhoneNumber { get; set; } = string.Empty;
}
