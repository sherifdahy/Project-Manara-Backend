using App.Application.Contracts.Responses.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Queries.Account;

public class GetStudentProfileQuery : IRequest<Result<StudentProfileResponse>>
{
}
