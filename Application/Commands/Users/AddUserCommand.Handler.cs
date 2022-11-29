using Domain.Entities;
using Infrastructure.Context;
using MediatR;
using Shared;

namespace Application.Commands.Users
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, bool>
    {
        private readonly AppDbContext _context;
        public AddUserCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.UserId);
            user = ObjectToObjectMapper<AddUserCommand, User>.Map(request, user);
            await _context.AddAsync(user);
            if(await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            throw new Exception("Add user failed.");
        }
    }
}
