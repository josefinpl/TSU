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
                CategoryName = x.Category.Name,
                Numbers = db.Number.Where(y => y.Authority_Id == x.Id).Select(number => new NumberVM
                {
                    Id = number.Id,
                    Name = number.Name,
                    Number1 = number.Number1,
                    Authority_Id = number.Authority_Id

                }
                ).ToList(),
                Hours = db.Hour.Where(h => h.Authority_Id == x.Id).Select(hour => new HourVM
                {
                    Id = hour.Id,
                    Open = hour.Open,
                    Close = hour.Close,
                    Name = hour.Name,
                    Authority_Id = hour.Authority_Id
                }).ToList()
            }).Single();

            return authority;
        }

        public int SetAuthority(AuthorityVM model)
        {
            Authority a = new Authority
            {
                Name = model.Name,
                Description = model.Description,
                Category_Id = model.Category_Id,
                Address_Id = SetAddress(model)
            };

            db.Authority.Add(a);
            db.SaveChanges();

            return a.Id;
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

            if (authority.Number.Name !=null)
            {
                Number number = new Number
                {
                    Name = authority.Number.Name,
                    Number1 = authority.Number.Number1,
                    Authority_Id = authority.Id
                };
                db.Number.Add(number);                               
            }

            if (authority.Hour.Name != null)
            {
                Hour hour = new Hour
                {
                    Name = authority.Hour.Name,
                    Open = authority.Hour.Open,
                    Close = authority.Hour.Close,
                    Authority_Id = authority.Id
                };
                db.Hour.Add(hour);
            }

            if (image != null)
            {
                authority.Logo = new byte[image.ContentLength];
                image.InputStream.Read(authority.Logo, 0, image.ContentLength);
                a.Logo = authority.Logo;
            }

                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
        }
   
        public User LoginCheck(User user)
        {
            var usr = db.User.FirstOrDefault(u => u.Username == user.Username && u.Password == user.Password);

            return usr;
        }

        public void DeleteAuthority(int id)
        {
            var authority = db.Authority.Where(x => x.Id == id).Single();

            if (authority.Hour.Count != 0)
            {
                for (int i = 0; i < authority.Hour.Count; i++)
                {
                    db.Hour.Remove(authority.Hour.ElementAt(i));
                }

            }

            if (authority.Number.Count != 0)
            {
                for (int i = 0; i < authority.Number.Count; i++)
                {
                    db.Number.Remove(authority.Number.ElementAt(i));
                }
             
            }

            db.Authority.Remove(authority);

            db.SaveChanges();
        }

        public void SetLogo(AuthorityVM authority, HttpPostedFileBase image)
        {
            var a = db.Authority.Where(x => x.Id == authority.Id).FirstOrDefault();

            if (image != null)
            {
                authority.Logo = new byte[image.ContentLength];
                image.InputStream.Read(authority.Logo, 0, image.ContentLength);
                a.Logo = authority.Logo;
                db.Entry(a).State = EntityState.Modified;
                db.SaveChanges();
            }

          
        }

        public NumberVM GetNumber(int? id)
        {
            var number = db.Number.Where(x => x.Id == id).Select(x => new NumberVM
            {
                Id = x.Id,
                Name = x.Name,
                Number1 = x.Number1,
                Authority_Id = x.Authority_Id

            }).FirstOrDefault();

            return number;
        }

        public void SetNumber(NumberVM model)
        {
            Number number = new Number
            {
                Name = model.Name,
                Number1 = model.Number1,
                Authority_Id = model.Authority_Id
            };

            db.Number.Add(number);
            db.SaveChanges();

        }

        public void DeleteNumber(int id)
        {
            var number = db.Number.Where(x => x.Id == id).Single();
            db.Number.Remove(number);

            db.SaveChanges();
        }
        public void EditNumber(NumberVM number)
        {
            Number n = new Number
            {
                Id = number.Id,
                Name = number.Name,
                Number1 = number.Number1,
                Authority_Id = number.Authority_Id
            };

            db.Entry(n).State = EntityState.Modified;

            db.SaveChanges();
        }

        public HourVM GetHour(int? id)
        {
            var hour = db.Hour.Where(x => x.Id == id).Select(x => new HourVM
            {
                Id = x.Id,
                Name = x.Name,
                Open = x.Open,
                Close = x.Close,
                Authority_Id = x.Authority_Id

            }).FirstOrDefault();

            return hour;
        }

        public void SetHour(HourVM model)
        {
            Hour h = new Hour
            {
                Name = model.Name,
                Open = model.Open,
                Close = model.Close,
                Authority_Id = model.Authority_Id
            };

            db.Hour.Add(h);
            db.SaveChanges();
        }

        public void EditHour(Hour hour)
        {
            db.Entry(hour).State = EntityState.Modified;

            db.SaveChanges();
        }

        public void DeleteHour(int id)
        {
            var hour = db.Hour.Where(x => x.Id == id).Single();
            db.Hour.Remove(hour);

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