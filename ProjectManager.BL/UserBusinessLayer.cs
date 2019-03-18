
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager.Entities;
using ProjectManager.DL;
using System.Data.Entity;
using System.Web.Http.Cors;

namespace ProjectManager.BL
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserBusinessLayer
    {
        private DataContext db = new DataContext();

        public DbSet<UserTable> GetAllUsers()
        {
            return db.Users;
        }

        public List<UserTable> GetUsersummary()
        {
            return db.Users.ToList();
            //IQueryable<User> query = (from users in db.Users
            //                                 group users by new
            //                                 {
            //                                     users.FirstName,
            //                                     users.LastName,
            //                                     users.EmployeeId,
            //                                     users.Deleted
            //                                 }
            //                                   into lists
            //                                 select new User
            //                                 {
            //                                     FirstName = lists.Key.FirstName,
            //                                     LastName = lists.Key.LastName,
            //                                     EmployeeId = lists.Key.EmployeeId,
            //                                     Deleted= lists.Key.Deleted
            //                                 });
            //return query.ToList();
        }

        public bool Add(UserTable users)
        {
            try
            {
                db.Users.Add(users);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;

        }
        public bool Delete(int userId)
        {
            try
            {
                var query = (from update in db.Users.Where(x => x.UserId == userId)
                             select update).SingleOrDefault();
                if (query != null)
                {
                    query.UserId = userId;
                    query.Deleted = true;

                    db.Entry(query).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Update(int userId, UserTable users)
        {
            try
            {
                var query = (from update in db.Users.Where(x => x.UserId == userId)
                             select update).SingleOrDefault();
                if (query != null)
                {
                    query.UserId = userId;
                    query.FirstName = users.FirstName;
                    query.LastName = users.LastName;
                    query.EmployeeId = users.EmployeeId;

                    db.Entry(query).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        
    }

    public class User
    {
        public string FirstName;
        public string LastName;
        public int EmployeeId;
        public bool Deleted;
    }
}

