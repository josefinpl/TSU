using Admin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Admin.Models.db;

namespace Admin.Data
{
    public class DbOperations
    {
        private tusjoseEntities db = new tusjoseEntities();

        public UserVM GetUser(int? id)
        {
            var user = db.User.Where(x => x.Id == id).Select(x => new UserVM
            {
                Id = x.Id,
                Username = x.Username,
                Password = x.Password,
                Firstname = x.Firstname,
                Lastname = x.Lastname,
                Email = x.Email,
                Number = x.Number,
                Access_Id = x.Access_Id,
                AccessName = x.Access.Name,
                StreetAddress = x.Address.Address1,
                Zipcode = x.Address.Zipcode,
                City = x.Address.City,
            }).Single();

            return user;
        }

        public void DeleteUser(int id)
        {
            var user = db.User.Where(x => x.Id == id).Single();
            db.User.Remove(user);

            db.SaveChanges();
        }

        public IEnumerable<UserVM> ListUsers()
        {
            var users = db.User.Include(u => u.Address).Select(x => new UserVM
            {
                Id = x.Id,
                Username = x.Username,
                Password = x.Password,
                Firstname = x.Firstname,
                Lastname = x.Lastname,
                Email = x.Email,
                StreetAddress = x.Address.Address1,
                Zipcode = x.Address.Zipcode,
                City = x.Address.City
            }).ToList();

            return users;
        }

        public IEnumerable<Address> GetAllAddresses()
        {
            var allAddresses = db.Address.ToList();
            return allAddresses;
        }

        public int SetAddress(UserVM model)
        {
            Address a = new Address
            {
                Address1 = model.StreetAddress,
                Zipcode = model.Zipcode,
                City = model.City
            };

            bool exists = false;
            foreach (var item in GetAllAddresses())
            {
                if (a.Address1.Trim() == item.Address1.Trim()
                    && a.Zipcode == item.Zipcode)
                {
                    exists = true;
                    a = item;
                    break;
                }
            }

            if (!exists)
            {
                db.Address.Add(a);
                db.SaveChanges();
            }
            return a.Id;
        }
    }
}