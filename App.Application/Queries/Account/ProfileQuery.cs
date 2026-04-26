using App.Application.Contracts.Responses.Account;
using App.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Account;

public record GetProfileQuery : IRequest<Result<ProfileResponse>>
{
}
