namespace Identity.Domain.Dtos
{
    internal class UserDTO
    {
    }

    public class UserGetDto
    {
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;

        public string? PhoneNumber { get; set; } = default!;

        public long RoleId { get; set; }

        public RoleGetDto Role { get; set; }
    }
}
