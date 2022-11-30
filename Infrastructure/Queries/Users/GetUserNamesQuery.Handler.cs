using Bk.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bk.Infrastructure.Queries.Users
{
    public class GetUserNamesQueryHandler : IRequestHandler<GetUserNamesQuery, GetUserNamesQueryResponse>
    {
        private readonly AppDbContext _context;
        public GetUserNamesQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<GetUserNamesQueryResponse> Handle(GetUserNamesQuery request, CancellationToken cancellationToken)
        {
            return new GetUserNamesQueryResponse
            {
                UserNames = await _context.Users.Select(x => x.UserName).ToListAsync()
            };
        }
    }
}
