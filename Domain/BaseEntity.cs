using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace Domain
{
    public class BaseEntity<TId>
    {
        private readonly TId _id;


        protected BaseEntity()
        {

        }
        public BaseEntity(int createdBy)
        {
            if (createdBy < 0)
                throw new ArgumentException($"{nameof(createdBy)} must be greater than zero.");

            CreatedBy = createdBy;
            CreatedOn = DateTime.Now;
            UpdatedBy = createdBy;
            UpdatedOn = DateTime.Now;
        }


        public BaseEntity(int createdBy, TId id)
        {
            if (createdBy < 0)
                throw new ArgumentException($"{nameof(createdBy)} must be greater than zero.");

            _id = id;
            CreatedBy = createdBy;
            CreatedOn = DateTime.Now;
            UpdatedBy = createdBy;
            UpdatedOn = DateTime.Now;
        }

        public TId Id => _id;
        public int CreatedBy { get; private set; }
        public int UpdatedBy { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime UpdatedOn { get; private set; }

        public void SetUpdatedBy(int updatedBy)
        {
            if (updatedBy < 0)
                throw new ArgumentException($"{nameof(updatedBy)} must be greater than zero.");
            UpdatedBy = updatedBy;
            UpdatedOn = DateTime.Now;
        }
    }
}
