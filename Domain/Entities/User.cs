namespace Bk.Domain.Entities
{
    public class User : BaseEntity<int>
    {
        private User()
        {

        }

        public User(int createdBy) : base(createdBy)
        {

        }

        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
