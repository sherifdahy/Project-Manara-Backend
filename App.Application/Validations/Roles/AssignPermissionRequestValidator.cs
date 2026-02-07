using App.Application.Contracts.Requests.Roles;
using App.Infrastructure.Localization.Localizers;

namespace App.Application.Validations.Roles;

public class AssignPermissionRequestValidator : AbstractValidator<AssignPermissionRequest>
{
    public AssignPermissionRequestValidator(JsonStringLocalizer localizer)
    {

        
    }
}
