using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class CommentServiceTests
    {
        [Theory]
        [MemberData(nameof(GetData))]
        public async void GetProductComments_ReturnsCorrectValues(Guid productId, int expectedListLength)
        {
            var options = new DbContextOptionsBuilder<WebDbContext>()
                .UseInMemoryDatabase(databaseName: "kamandax")
                .Options;

            using (var context = new WebDbContext(options))
            {
                context.Database.EnsureDeleted();
                context.Comments.Add(new Comment
                {
                    Id = new Guid("c7b6582c-92d2-4793-be70-64444ba04d1d"),
                    Name = "TestName",
                    Data = "Comment",
                    ProductId = new Guid("140222bd-bc2e-4e82-89a5-6d7b0f6423b3")
                });
                context.Comments.Add(new Comment
                {
                    Id = new Guid("6d7e9137-7d1a-43b8-9e9e-9d5d4535e19a"),
                    Name = "TestName",
                    Data = "Comment",
                    ProductId = new Guid("140222bd-bc2e-4e82-89a5-6d7b0f6423b3")
                });
                context.Comments.Add(new Comment
                {
                    Id = new Guid("9588a76c-bf41-4f80-979c-c1915c02eb6e"),
                    Name = "TestName",
                    Data = "Comment",
                    ProductId = new Guid("140222bd-bc2e-4e82-89a5-6d7b0f6423b3")
                });
                context.Comments.Add(new Comment
                {
                    Id = new Guid("1187f25f-dd65-4aa7-92a3-eb0e53f62a81"),
                    Name = "TestName",
                    Data = "Comment",
                    ProductId = new Guid("325f1241-240b-4c4b-bc4e-80e7598a1765")
                });
                context.SaveChanges();
            }
            using (var context = new WebDbContext(options))
            {
                var service = new CommentService(context);
                var comments = await service.GetProductComments(productId);

                Assert.Equal(expectedListLength, comments.Count);
            }
        }

        [Fact]
        public async void CreateComment_Success()
        {
            var newComment = new Comment
            {
                Id = new Guid("c7b6582c-92d2-4793-be70-64444ba04d1d"),
                Name = "TestName",
                Data = "Comment",
                ProductId = new Guid("140222bd-bc2e-4e82-89a5-6d7b0f6423b3")
            };
            var options = new DbContextOptionsBuilder<WebDbContext>()
               .UseInMemoryDatabase(databaseName: "kamandax")
               .Options;
            using var context = new WebDbContext(options);
            var service = new CommentService(context);
            var createdComment = await service.Create(newComment);

            var commentFromDb = await context.Comments.FirstOrDefaultAsync(x => x.Id == newComment.Id);
            Assert.Equal(newComment, commentFromDb);
        }

        public static IEnumerable<object[]> GetData()
        {
            var allData = new List<object[]>
        {
            new object[] { Guid.Parse("140222bd-bc2e-4e82-89a5-6d7b0f6423b3"), 3 },
            new object[] { Guid.Parse("325f1241-240b-4c4b-bc4e-80e7598a1765"), 1 },
        };
            return allData;
        }
    }
}
