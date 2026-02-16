using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.Permissions;

public record GetPermissionsInUserResponse
(
    IEnumerable<string> DefaultPermissionsInFaculty,
    IEnumerable<string> OverridePermissions
);
