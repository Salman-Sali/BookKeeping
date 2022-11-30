using MediatR;

namespace Bk.Infrastructure.Queries.Books
{
    public class ListBookTypesQuery : IRequest<ListBookTypesQueryResponse>
    {
    }
}
