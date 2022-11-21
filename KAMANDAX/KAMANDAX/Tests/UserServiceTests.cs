namespace Tests
{
    public class UserServiceTests
    {
        [Theory]
        [InlineData("email1@email.com", "Test1")]
        [InlineData("email2@email.com", "Test2")]
        [InlineData("email3@email.com", "Test3")]
        public async void GetUserByEmail_UsersExists_ReturnedCorrectValue(string email, string expectedName)
        {
            var options = new DbContextOptionsBuilder<WebDbContext>()
                .UseInMemoryDatabase(databaseName: "kamandax")
                .Options;

            using(var context = new WebDbContext(options))
            {
                context.Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    FullName = "Test1",
                    Email = "email1@email.com",
                    Password = "Password",
                    Address = "Address",
                    Country = "LT",
                    City = "City",
                    PostalCode = "123456"
                });
                context.Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    FullName = "Test2",
                    Email = "email2@email.com",
                    Password = "Password",
                    Address = "Address",
                    Country = "LT",
                    City = "City",
                    PostalCode = "123456"
                });
                context.Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    FullName = "Test3",
                    Email = "email3@email.com",
                    Password = "Password",
                    Address = "Address",
                    Country = "LT",
                    City = "City",
                    PostalCode = "123456"
                });
                context.SaveChanges();
            }
            using(var context = new WebDbContext(options))
            {
            var service = new UserService(context);
            var userNew = await service.GetUserByEmail(email);

            Assert.Equal(expectedName, userNew.FullName);
            }
        }
        [Fact]
        public async void GetUserById_UsersExists_ReturnedCorrectValues()
        {
            var id = Guid.NewGuid();
            var options = new DbContextOptionsBuilder<WebDbContext>()
                .UseInMemoryDatabase(databaseName: "kamandax")
                .Options;

            using (var context = new WebDbContext(options))
            {
                context.Users.Add(new User
                {
                    Id = id,
                    FullName = "Test1",
                    Email = "email1@email.com",
                    Password = "Password",
                    Address = "Address",
                    Country = "LT",
                    City = "City",
                    PostalCode = "123456"
                });
                context.SaveChanges();
            }
            using (var context = new WebDbContext(options))
            {
                var service = new UserService(context);
                var userNew = await service.GetUserById(id);

                Assert.Equal("Test1", userNew.FullName);
            }
        }
    }
}