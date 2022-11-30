using Bk.Infrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Bk.Infrastructure.Queries.Users
{
    public class LoginUserHandler : IRequestHandler<LoginUser, int?>
    {
        private readonly AppDbContext _context;
        public LoginUserHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int?> Handle(LoginUser request, CancellationToken cancellationToken)
        {
            return (await _context.Users.FirstOrDefaultAsync(x=>x.UserName == request.UserName && x.Password == request.Password))?.Id ?? null; 
        }
    }
}
