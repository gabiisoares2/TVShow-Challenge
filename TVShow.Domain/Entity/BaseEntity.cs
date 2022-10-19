using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TVShow.Domain.Entity
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? DisableDate { get; set; }

        protected BaseEntity()
        {
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }
    }
}