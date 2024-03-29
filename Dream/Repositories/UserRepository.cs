﻿using Dream.Data.Models;
using Dream.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace Dream.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private DreamContext context;
        public UserRepository(DreamContext context)
        { this.context = context; }
        public void Add(User user)
        {
            context.Users.Add(user);
        }

        public void Delete(User user)
        {
            context.Users.Remove(user);
            Save();
        }

        public void Update(User user)
        {
            context.Update(user);
            Save();
        }

        public User Get(int id)
        {
            return context.Users.FirstOrDefault(x => x.UserId == id);
        }
        public User GetByUsername(string username)
        {
            return context.Users.FirstOrDefault(x => x.Username == username);
        }

        public List<User> GetAll()
        {
            return context.Users.ToList();
        }
        public bool UserUsernameExists(string username)
        {
            return context.Users.Any(x => x.Username == username);
        }

        public bool UserEmailExists(string email)
        {
            return context.Users.Any(x => x.Email == email);
        }

        public bool Exists(int id)
        {
            return context.Users.Any(x => x.UserId == id);
        }

        public void Save()
        {
            bool saveFailed;
            do
            {
                saveFailed = false;

                try
                {
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    /* Update the values of the entity that failed to save from the store */
                    ex.Entries.Single().Reload();
                }

            } while (saveFailed);
        }
    }
}
