using KAMANDAX.DB;
using KAMANDAX.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAMANDAX.Services
{
    public class CommentService
    {
        private readonly WebDbContext _db;

        public CommentService(WebDbContext db)
        {
            _db = db;
        }

        public async Task<Comment> Create(Comment comment)
        {
            _db.Add(comment);
            await _db.SaveChangesAsync();

            return comment;
        }

        public async Task<List<Comment>> GetProductComments(Guid productId)
        {
            return await _db.Comments.Where(i => i.ProductId == productId).ToListAsync();
        }
    }
}
