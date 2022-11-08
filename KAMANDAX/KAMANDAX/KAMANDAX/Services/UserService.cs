using KAMANDAX.DB;
using KAMANDAX.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KAMANDAX.Services
{
    public class UserService
    {
        private readonly WebDbContext _db;

        public UserService(WebDbContext db)
        {
            _db = db;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<User> Create(User user)
        {
            user.Id = new Guid();
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return user;
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public string UpdateUser(User updUser)
        {
            _db.Users.Update(updUser);
            _db.SaveChanges();
            return "Update Successfully";
        }

        public async Task Delete(Guid id)
        {
            User user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            _db.Remove(user);
            await _db.SaveChangesAsync();
           
        }

        public async Task EditUser(UserRequest user, Guid id)
        {
            User oldUser = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
            oldUser.Id = id;
            oldUser.FullName = user.FullName;
            oldUser.Email = user.Email;
            oldUser.Password = user.Password;
            oldUser.Address = user.Address;
            oldUser.Country = user.Country;
            oldUser.City = user.City;
            oldUser.PostalCode = user.PostalCode;

            await _db.SaveChangesAsync();
        }

        public async Task UpdatePassword(string email, string password)
        {
            User user = await GetUserByEmail(email);
            user.Password = password;
            await _db.SaveChangesAsync();
        }
    }
}
