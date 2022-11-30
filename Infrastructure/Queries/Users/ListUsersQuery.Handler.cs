using Bk.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bk.Infrastructure.Queries.Users
{
    public class ListUsersQueryHandler : IRequestHandler<ListUsersQuery, ListUsersQueryResponse>
    {
        private readonly AppDbContext _context;
        public ListUsersQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ListUsersQueryResponse> Handle(ListUsersQuery request, CancellationToken cancellationToken)
        {
            return new ListUsersQueryResponse
            {
                Users = await _context.Users.Select(x => new UserResponse
                {
                    Id = x.Id,
                    UserName = x.UserName,
                }).ToListAsync(),
            };
        }
    }
}
