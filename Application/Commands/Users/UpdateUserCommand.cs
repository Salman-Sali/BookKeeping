using MediatR;

namespace BK.Application.Commands.Users
{
    public class UpdateUserCommand : IRequest<bool>
    {
        public int UpdaterUserId { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
