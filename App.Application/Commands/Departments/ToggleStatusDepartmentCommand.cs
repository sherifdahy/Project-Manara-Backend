using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.Departments;

public record ToggleStatusDepartmentCommand(int Id) : IRequest<Result>;
