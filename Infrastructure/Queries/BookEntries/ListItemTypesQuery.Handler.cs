using Bk.Domain.Enums;
using Bk.Infrastructure.Queries.Books;
using MediatR;

namespace Bk.Infrastructure.Queries.BookEntries
{
    public class ListItemTypesQueryHandler : IRequestHandler<ListItemTypesQuery, ListItemTypesQueryResponse>
    {
        public Task<ListItemTypesQueryResponse> Handle(ListItemTypesQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ListItemTypesQueryResponse
            {
                ItemTypes = Enum.GetNames(typeof(ItemType)).ToList()
            });
        }
    }
}
