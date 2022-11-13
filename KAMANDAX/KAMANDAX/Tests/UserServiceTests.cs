namespace Tests
{
    public class UserServiceTests
    {
        [Fact]
        public async void GetUserByEmail_UsersExists_ReturnedCorrectValue()
        {
            var options = new DbContextOptionsBuilder<WebDbContext>()
                .UseInMemoryDatabase(databaseName: "kamandax")
                .Options;

            using(var context = new WebDbContext(options))
            {
                context.Users.Add(new User
                {
                    Id = Guid.NewGuid(),
                    FullName = "Test",
                    Email = "email@email.com",
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
            var userNew = await service.GetUserByEmail("email@email.com");

            Assert.Equal("email@email.com", userNew.Email);
            }
        }
    }
}