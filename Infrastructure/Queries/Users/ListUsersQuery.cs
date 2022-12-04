using MediatR;

namespace Bk.Infrastructure.Queries.Users
{
    public class ListUsersQuery : IRequest<ListUsersQueryResponse>
    {
        public int? Page { get; set; } = 1;
        public int? Limit { get; set; } = 15;
    }
}
