using Bk.Domain.Enums;
using MediatR;

namespace Bk.Infrastructure.Queries.Books
{
    public class ListBookTypesQueryHandler : IRequestHandler<ListBookTypesQuery, ListBookTypesQueryResponse>
    {
        public Task<ListBookTypesQueryResponse> Handle(ListBookTypesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ListBookTypesQueryResponse
            {
                BookTypes = Enum.GetNames(typeof(BookType)).ToList()
            });
        }
    }
}
