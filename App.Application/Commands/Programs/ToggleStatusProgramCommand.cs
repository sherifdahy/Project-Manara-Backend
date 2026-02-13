using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Commands.Programs;

public record ToggleStatusProgramCommand(int Id) : IRequest<Result>;