using Core.Services;

namespace UnitTests.Tests.Core.Services
{
    internal class CryptographyServiceTests
    {
        [TestMethod]
        public void IsSafePassword_WeakPassword_ShouldReturnFalse()
        {
            // Arrange
            var cryptographyService = new CryptographyService();
            var password = "Password1";

            // Act
            var result = cryptographyService.IsSafePassword(password);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsSafePassword_StrongPassword_ShouldReturnTrue()
        {
            // Arrange
            var cryptographyService = new CryptographyService();
            var password = "Password.1";

            // Act
            var result = cryptographyService.IsSafePassword(password);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void HashPassword_GenerateHash_ShouldReturnHashedPassword()
        {
            // Arrange
            var cryptographyService = new CryptographyService();

            var passwordSalt = cryptographyService.GeneratePasswordSalt();
            var password = "Password.1";

            // Act
            var result = cryptographyService.HashPassword(passwordSalt, password);

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
            var cryptographyService = new CryptographyService();

            var passwordSalt = cryptographyService.GeneratePasswordSalt();
            var password = "Password.1";

            var passwordHash = cryptographyService.HashPassword(password, passwordSalt);

            // Act
            var result = cryptographyService.IsCorrectPassword(password, passwordHash, passwordSalt);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void IsCorrectPassword_IncorrectPassword_ShouldReturnFalse()
        {
            // Arrange
            var cryptographyService = new CryptographyService();

            var passwordSalt = cryptographyService.GeneratePasswordSalt();
            var accountPassword = "Password.1";

            var logInPassword = "Password.2";

            var passwordHash = cryptographyService.HashPassword(accountPassword, passwordSalt);

            // Act
            var result = cryptographyService.IsCorrectPassword(logInPassword, passwordHash, passwordSalt);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
