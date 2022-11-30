using MediatR;

namespace Bk.Infrastructure.Queries.Users
{
    public class GetUserNamesQuery : IRequest<GetUserNamesQueryResponse>
    {
    }
}
