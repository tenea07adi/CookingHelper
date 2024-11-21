using API.Helpers;

namespace UnitTests.Tests.Helpers
{
    [TestClass]
    public class CryptographyHelperTests
    {
        [TestMethod]
        public void IsSafePassword_WeakPassword_ShouldReturnFalse()
        {
            // Arrange
            var password = "Password1";

            // Act
            var result = CryptographyHelper.IsSafePassword(password);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsSafePassword_StrongPassword_ShouldReturnTrue()
        {
            // Arrange
            var password = "Password.1";

            // Act
            var result = CryptographyHelper.IsSafePassword(password);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HashPassword_GenerateHash_ShouldReturnHashedPassword()
        {
            // Arrange
            var passwordSalt = CryptographyHelper.GeneratePasswordSalt();
            var password = "Password.1";

            // Act
            var result = CryptographyHelper.HashPassword(passwordSalt, password);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreNotEqual(password, result);
            Assert.AreNotEqual(passwordSalt, result);
            Assert.IsTrue(result.Length > 10);
        }

        [TestMethod]
        public void IsCorrectPassword_CorrectPassword_ShouldReturnTrue()
        {
            // Arrange
            var passwordSalt = CryptographyHelper.GeneratePasswordSalt();
            var password = "Password.1";

            var passwordHash = CryptographyHelper.HashPassword(password, passwordSalt);

            // Act
            var result = CryptographyHelper.IsCorrectPassword(password, passwordHash, passwordSalt);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsCorrectPassword_IncorrectPassword_ShouldReturnFalse()
        {
            // Arrange
            var passwordSalt = CryptographyHelper.GeneratePasswordSalt();
            var accountPassword = "Password.1";

            var logInPassword = "Password.2";

            var passwordHash = CryptographyHelper.HashPassword(accountPassword, passwordSalt);

            // Act
            var result = CryptographyHelper.IsCorrectPassword(logInPassword, passwordHash, passwordSalt);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
