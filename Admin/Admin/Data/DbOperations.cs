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

        public AuthorityVM GetAuthority(int? id)
        {
            var authority = db.Authority.Where(x => x.Id == id).Select(x => new AuthorityVM
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                StreetAddress = x.Address.Address1,
                Zipcode = x.Address.Zipcode,
                City = x.Address.City,
                CategoryName = x.Category.Name
            }).Single();

            return authority;
        }

        public void DeleteAuthority(int id)
        {
            var authority = db.Authority.Where(x => x.Id == id).Single();
            db.Authority.Remove(authority);

            db.SaveChanges();
        }

        public IEnumerable<AuthorityVM> ListAuthorities()
        {
            var authorities = db.Authority.Include(u => u.Address).Include(u => u.Category).Select(x => new AuthorityVM
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                CategoryName = x.Category.Name,
                StreetAddress = x.Address.Address1,
                Zipcode = x.Address.Zipcode,
                City = x.Address.City
            }).ToList();

            return authorities;
        }

        public IEnumerable<Address> GetAllAddresses()
        {
            var allAddresses = db.Address.ToList();
            return allAddresses;
        }

        public int SetAddress(AuthorityVM model)
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