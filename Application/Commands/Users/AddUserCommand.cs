using MediatR;

namespace Application.Commands.Users
{
    public class AddUserCommand : IRequest<bool>
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public int UserId { get; set; }
    }
}
