namespace LibraryManagementSystem.DTOs
{
        public class UserRegisterDto
        {
            public string Username { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;

            // This defaults to Member so normal users don't have to pass a role during signup
            public string Role { get; set; } = "Member";
        }

        public class UserLoginDto
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public class AuthResponseDto
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; } = string.Empty;
            public string Token { get; set; } = string.Empty;
        }
    }


