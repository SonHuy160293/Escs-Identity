namespace Identity.Domain.Dtos
{
    public class UserTokenDTO
    {

    }

    public class UserTokenGetDto
    {

        public long Id { get; set; }
        public string Key { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        public long ServiceId { get; set; }

        public ServiceGetDto Service { get; set; }

        public long UserId { get; set; }

        public UserGetDto User { get; set; }
    }
}
