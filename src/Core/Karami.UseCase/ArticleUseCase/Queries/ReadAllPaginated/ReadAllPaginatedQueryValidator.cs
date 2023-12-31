﻿using Karami.Core.UseCase.Contracts.Interfaces;
using Karami.Core.UseCase.Exceptions;

namespace Karami.UseCase.ArticleUseCase.Queries.ReadAllPaginated;

public class ReadAllPaginatedQueryValidator : IValidator<ReadAllPaginatedQuery>
{
    public async Task<object> ValidateAsync(ReadAllPaginatedQuery input, CancellationToken cancellationToken)
    {
        await Task.Run(() => {
            
            if (input.PageNumber == null)
                throw new UseCaseException("تنظیم مقدار ( شماره صفحه ) الزامی می باشد !");

            if (input.CountPerPage == null)
                throw new UseCaseException("تنظیم مقدار ( تعداد برای هر صفحه ) الزامی می باشد !");
            
        }, cancellationToken);

        return default;
    }
}