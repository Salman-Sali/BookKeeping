using MediatR;

namespace BK.Application.Commands.Users
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public int UserId { get; set; }
    }
}
