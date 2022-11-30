namespace Bk.Infrastructure.Queries.Users
{
    public class ListUsersQueryResponse
    {
        public IEnumerable<UserResponse>? Users { get; set; }
    }

    public class UserResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}
