using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Errors;

public static class UserErrors
{
    public static Error DuplicatedEmail => new(
        "User.DuplicatedEmail",
        "This Email is Already Exists.",
        StatusCodes.Status409Conflict
    );

    public static Error DuplicatedSSN => new(
        "User.DuplicatedSSN",
        "This SSN is Already Exists.",
        StatusCodes.Status409Conflict
    );

    public static Error NotFound => new(
        "User.NotFound",
        "This User not Exists.",
        StatusCodes.Status404NotFound
    );
}

