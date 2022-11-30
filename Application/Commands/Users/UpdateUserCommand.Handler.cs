using Bk.Domain.Entities;
using Bk.Infrastructure.Context;
using MediatR;
using Bk.Shared;

namespace BK.Application.Commands.Users
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        private readonly AppDbContext _context;
        public UpdateUserCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = _context.Users.FirstOrDefault(x => x.Id == request.UserId);
            if (user == null)
            {
                throw new Exception("User not found.");
            }
            user = ObjectToObjectMapper<UpdateUserCommand, User>.Map(request, user);
            user.SetUpdatedBy(request.UpdaterUserId);

            _context.Update(user);
            if(await _context.SaveChangesAsync() > 0) {
                return true;
            }
            throw new Exception("User update failed.");
        }
    }
}
