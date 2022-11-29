using MediatR;

namespace Application.Commands.Users
{
    public class DeleteUserCommand : IRequest<bool>
    {
        public int UserId { get; set; }
    }
}
