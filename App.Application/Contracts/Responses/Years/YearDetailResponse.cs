using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Contracts.Responses.Years;

public record YearDetailResponse
(
    string Name,
    DateTime StartDate,
    DateTime EndDate,
    int ActiveTermId
);


