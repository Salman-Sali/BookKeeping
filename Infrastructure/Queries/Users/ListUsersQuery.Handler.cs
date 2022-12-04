using Bk.Domain.Enums;
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
            int totalPages = 0;
            var count = await _context.Users.CountAsync();

            totalPages = count / request.Limit.Value;
            if (count % request.Limit != 0)
            {
                totalPages++;
            }
            return new ListUsersQueryResponse
            {
                Users = await _context.Users.Select(x => new UserResponse
                {
                    Id = x.Id,
                    UserName = x.UserName,
                    Password = x.Password,
                    CreatedBy = _context.Users.FirstOrDefault(a => a.Id == x.CreatedBy).UserName,
                    CreatedOn = x.CreatedOn.ToShortDateString() + " " + x.CreatedOn.ToShortTimeString(),
                    UpdatedBy = (x.CreatedOn.ToShortDateString() == x.UpdatedOn.ToShortDateString() && x.CreatedOn.ToShortTimeString() == x.UpdatedOn.ToShortTimeString()) ? null : _context.Users.FirstOrDefault(a => a.Id == x.UpdatedBy).UserName,
                    UpdatedOn = x.CreatedOn.ToShortDateString() == x.UpdatedOn.ToShortDateString() && x.CreatedOn.ToShortTimeString() == x.UpdatedOn.ToShortTimeString() ? null : x.UpdatedOn.ToShortDateString() + " " + x.UpdatedOn.ToShortTimeString(),
                })
                .OrderBy(x => x.Id)
                .Skip((request.Page.Value - 1) * request.Limit.Value)
                .Take(request.Limit.Value)
                .ToListAsync(),
                TotalPages = totalPages
            };
        }
    }
}
