using Admin.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Admin.Models.db;
using System.Web.ModelBinding;

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
                Logo = x.Logo,
                StreetAddress = x.Address.Address1,
                Zipcode = x.Address.Zipcode,
                City = x.Address.City,
                Category_Id = x.Category_Id,
                CategoryName = x.Category.Name
            }).Single();

            return authority;
        }

        public void EditAuthority(AuthorityVM authority, HttpPostedFileBase image)
        {
            Authority a = new Authority
            {
                Id = authority.Id,
                Name = authority.Name,
                Description = authority.Description.Trim(),
                Logo = authority.Logo,
                Category_Id = authority.Category_Id,
                Address_Id = SetAddress(authority)
            };

            if (image != null)
            {
                authority.Logo = new byte[image.ContentLength];
                image.InputStream.Read(authority.Logo, 0, image.ContentLength);
                a.Logo = authority.Logo;
            }

                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
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
                Logo = x.Logo,
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
            var addresses = GetAllAddresses();

            foreach (var item in addresses)
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