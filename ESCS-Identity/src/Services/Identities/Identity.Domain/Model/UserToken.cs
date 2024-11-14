using Core.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Domain.Model
{
    public class UserToken : BaseEntity
    {

        public string Key { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public long ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }

        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }


    }
}
