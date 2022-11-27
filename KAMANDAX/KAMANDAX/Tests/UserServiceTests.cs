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
        [Fact]
        public async void CreateUser_Success()
        {
            var options = new DbContextOptionsBuilder<WebDbContext>()
                .UseInMemoryDatabase(databaseName: "kamandax")
                .Options;
            using var context = new WebDbContext(options);
            var newUser = new User
            {
                FullName = "KristupasLoz",
                Password = "Passowrd",
                Address = "Address",
                PostalCode = "1230",
                Country = "LT",
                City = "City"
            };
            var service = new UserService(context);
            var createdUser = service.Create(newUser);

            var userFromDb = await context.Users.FirstOrDefaultAsync(x => x.FullName == "KristupasLoz");

            Assert.Equal(newUser.FullName, userFromDb.FullName);
            Assert.Equal(newUser.Password, userFromDb.Password);
            Assert.Equal(newUser.Address, userFromDb.Address);
            Assert.Equal(newUser.City, userFromDb.City);
            Assert.Equal(newUser.Country, userFromDb.Country);
            Assert.Equal(newUser.PostalCode, userFromDb.PostalCode);

        }

        [Fact]
        public async void GetUsers_ReturnsUsersList()
        {
            var options = new DbContextOptionsBuilder<WebDbContext>()
                .UseInMemoryDatabase(databaseName: "kamandax")
                .Options;
            using var context = new WebDbContext(options);
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
            context.SaveChanges();
            var userService = new UserService(context);
            var users = await userService.GetUsers();
            Assert.Equal(users.Count, 2);
            Assert.Equal(users[0].FullName, "Test1");
            Assert.Equal(users[1].FullName, "Test2");
        }
        [Fact]
        public async void DeleteUser_DeletedSuccessfully()
        {
            var options = new DbContextOptionsBuilder<WebDbContext>()
                .UseInMemoryDatabase(databaseName: "kamandax")
                .Options;
            using var context = new WebDbContext(options);
            context.Users.Add(new User
            {
                Id = Guid.Parse("140222bd-bc2e-4e82-89a5-6d7b0f6423b3"),
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
                Id = Guid.Parse("325f1241-240b-4c4b-bc4e-80e7598a1765"),
                FullName = "Test2",
                Email = "email2@email.com",
                Password = "Password",
                Address = "Address",
                Country = "LT",
                City = "City",
                PostalCode = "123456"
            });
            context.SaveChanges();
            var userService = new UserService(context);
            await userService.Delete(Guid.Parse("325f1241-240b-4c4b-bc4e-80e7598a1765"));
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == Guid.Parse("325f1241-240b-4c4b-bc4e-80e7598a1765"));
            Assert.Null(user);
        }
        [Fact]
        public async void UpdatePassword_PasswordUpdated()
        {
            var options = new DbContextOptionsBuilder<WebDbContext>()
                .UseInMemoryDatabase(databaseName: "kamandax")
                .Options;
            using var context = new WebDbContext(options);
            context.Users.Add(new User
            {
                Id = Guid.Parse("140222bd-bc2e-4e82-89a5-6d7b0f6423b3"),
                FullName = "Test1",
                Email = "email1@email.com",
                Password = "Password",
                Address = "Address",
                Country = "LT",
                City = "City",
                PostalCode = "123456"
            });
            context.SaveChanges();
            var userService = new UserService(context);
            var user = await context.Users.FirstOrDefaultAsync(x => x.Email == "email1@email.com");
            Assert.Equal("Password", user.Password);
            await userService.UpdatePassword("email1@email.com", "NewPassword");
            var updatedUser = await context.Users.FirstOrDefaultAsync(x => x.Email == "email1@email.com");
            Assert.Equal("NewPassword", updatedUser.Password);
        }
    }
}