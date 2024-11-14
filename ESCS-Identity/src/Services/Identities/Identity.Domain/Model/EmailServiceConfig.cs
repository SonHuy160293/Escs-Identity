using Core.Domain.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Identity.Domain.Model
{
    public class EmailServiceConfig : BaseEntity
    {

        public string SmtpEmail { get; set; } = default!;
        public string SmtpPassword { get; set; } = default!;
        public int SmtpPort { get; set; }


        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public long ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public virtual Service Service { get; set; }
    }
}
