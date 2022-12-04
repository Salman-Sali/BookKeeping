namespace Bk.Infrastructure.Queries.Users
{
    public class ListUsersQueryResponse
    {
        public IEnumerable<UserResponse>? Users { get; set; }
        public int TotalPages { get; set; }
    }

    public class UserResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string? UpdatedBy { get; set; }
        public string? UpdatedOn { get; set; }
    }
}
