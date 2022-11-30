using MediatR;

namespace Bk.Infrastructure.Queries.Users
{
    public class LoginUser : IRequest<int?>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
