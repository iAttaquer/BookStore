﻿using DotNetBoilerplate.Application.Books.DTO;
using DotNetBoilerplate.Application.Books.Get;
using DotNetBoilerplate.Infrastructure.DAL.Contexts;
using DotNetBoilerplate.Shared.Abstractions.Queries;
using Microsoft.EntityFrameworkCore;

namespace DotNetBoilerplate.Infrastructure.DAL.Handlers.Books;

internal sealed class GetBookStoreByIdHandler(
    DotNetBoilerplateReadDbContext dbContext
) : IQueryHandler<GetBookByIdQuery, BookDto>
{
    public async Task<BookDto> HandleAsync(GetBookByIdQuery query)
    {
        return await dbContext.Books
            .AsNoTracking()
            .Where(x => x.Id == query.Id)
            .Select(x=>new BookDto(
                x.Id,
                x.Title,
                x.Writer,
                x.Genre,
                x.Year,
                x.Description
            ))
            .FirstOrDefaultAsync();
    }
}