namespace Identity.Domain.Dtos
{
    internal class EmailServiceConfigDTO
    {
    }

    public class EmailServiceConfigGetDto
    {
        public long? Id { get; set; }
        public string SmtpEmail { get; set; } = default!;
        public string SmtpPassword { get; set; } = default!;
        public int SmtpPort { get; set; }


        public long UserId { get; set; }

        public UserGetDto User { get; set; }

        public long ServiceId { get; set; }

        public ServiceGetDto Service { get; set; }
    }
}
