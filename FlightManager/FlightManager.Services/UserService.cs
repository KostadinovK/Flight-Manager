using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlightManager.Common;
using FlightManager.Data;
using FlightManager.Domain;
using FlightManager.Services.Models;
using Microsoft.AspNetCore.Identity;

namespace FlightManager.Services
{
    public class UserService : IUserService
    {
        private FlightManagerDbContext context;

        public UserService(FlightManagerDbContext context, UserManager<User> userManager)
        {
            this.context = context;
        }

        public int GetCount()
        {
            return context.Users.Count(u => u.UserName != "admin");
        }

        public List<User> GetAll(int page, int showByPage, string orderBy)
        {
            var role = context.Roles.SingleOrDefault(r => r.Name == "User");

            var users = context.Users.Where(u => u.UserName != "admin");

            if (orderBy == "emailAsc")
            {
                users = users.OrderBy(u => u.Email);
            }
            else if (orderBy == "emailDesc")
            {
                users = users.OrderByDescending(u => u.Email);
            }
            else if (orderBy == "usernameDesc")
            {
                users = users.OrderByDescending(u => u.UserName);
            }
            else if (orderBy == "fNameDesc")
            {
                users = users.OrderByDescending(u => u.FirstName);
            }
            else if (orderBy == "fNameAsc")
            {
                users = users.OrderBy(u => u.FirstName);
            }
            else if (orderBy == "lNameDesc")
            {
                users = users.OrderByDescending(u => u.LastName);
            }
            else if (orderBy == "lNameAsc")
            {
                users = users.OrderBy(u => u.LastName);
            }
            else
            {
                users = users.OrderBy(u => u.UserName);
            }

            var result = users
                .Take(page * showByPage)
                .Skip((page - 1) * showByPage)
                .ToList();

            return result;
        }

        public bool Contains(string id)
        {
            return context.Users.Any(u => u.Id == id);
        }

        public User GetById(string id)
        {
            if (!Contains(id))
            {
                throw new ArgumentException("Invalid user Id");
            }

            var user = context.Users.SingleOrDefault(u => u.Id == id);

            return user;
        }

        public void Edit(UserServiceModel user)
        {
            if (!Contains(user.Id))
            {
                throw new ArgumentException("Invalid user id!");
            }

            var userFromDb = context.Users.SingleOrDefault(u => u.Id == user.Id);

            userFromDb.FirstName = user.FirstName;
            userFromDb.LastName = user.LastName;
            userFromDb.Address = user.Address;
            userFromDb.EGN = user.EGN;

            context.Users.Update(userFromDb);
            context.SaveChanges();
        }

        public void Delete(string id)
        {
            var user = context.Users.SingleOrDefault(u => u.Id == id);

            context.Users.Remove(user);
            context.SaveChanges();
        }
    }
}
