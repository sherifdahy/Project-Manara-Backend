using App.Application.Contracts.Responses.Days;
using App.Application.Queries.Days;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Application.Handlers.Queries.Days;

public class GetAllDaysQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetAllDaysQuery, Result<List<DayResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<List<DayResponse>>> Handle(GetAllDaysQuery request, CancellationToken cancellationToken)
    {
        var days = await _unitOfWork.Days.GetAllAsync(cancellationToken);

        return Result.Success(days.Adapt<List<DayResponse>>());
    }
}
