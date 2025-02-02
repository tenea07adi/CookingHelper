
namespace Core
{
    public static partial class Constants
    {
        public static class Exceptions
        {
            public static string InvalidCredentials = "Invalid credentials!";
            public static string InvalidCodeOrEmail = "Invalid code or email.";
            public static string InviteExpired = "Invite expired!";
            public static string NotSafePassword = "The password needs to contains more then 5 characters, a high case character, a lowercase character, a digit and a special character.";
            public static string InvalidEmail = "Invalid email!";
            public static string InviteAlreadyExists = "Invite already exist!";
            public static string InvalidRole = "Invalid role!";
            public static string UserNotFound = "User not found!";
            public static string IncorectOldPassword = "Incorect old password!";

            public static string InvalidAppConfig = "Invalid application configuration!";

            public static string InvalidEntityFilter = "Invalid entity filter!";
            public static string EntityNotFound = "Entity not found!";

            public static string SpecificEntityNotFound = "{0} not found!";
        }
    }
}
