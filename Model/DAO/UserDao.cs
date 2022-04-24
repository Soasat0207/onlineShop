using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF; //để có thể sử dụng cái DB ở È
using PagedList;
using Common;

namespace Model.DAO
{
    public class UserDao //project khác có thể truy cập
    {
        OnlineShopDbContext db = null; //khởi tạo biến

        public UserDao()
        {
            db = new OnlineShopDbContext(); //khởi tạo biến dao
        }
        //insert
        public long Insert (User entity) //phương thức insert trả về kiểu chính là cái ID cỉa nó
        {
            db.Users.Add(entity); //khởi tạo biến DB
            db.SaveChanges();
            return entity.ID; //tự tăng nên mình gen ra cái ID tự sinh
        }
        public long InsertForFacebook(User entity)
        {
            var user = db.Users.SingleOrDefault(x => x.UserName == entity.UserName);
            if (user == null)
            {
                db.Users.Add(entity);
                db.SaveChanges();
                return entity.ID;
            }
            else
            {
                return user.ID;
            }

        }
        //phương thức lấy ra tất cả các bản ghi
        public PagedList.IPagedList<User> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable <User> model = db.Users;
            if (!string.IsNullOrEmpty(searchString)) //nếu chuỗi tìm kiếm khác rỗng
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        
        // update
        public bool Update (User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                if(!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Address = entity.Address;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }
        }
        //change status
        public bool ChangeStatus(long id)
        {
            var user = db.Users.Find(id); //find user
            user.Status = !user.Status; //gán T-->F F-->T
            db.SaveChanges();
            return user.Status;
        }

        //delete
        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        //Get detail
        public User GetById (string userName) //phương thức lấy ra User truyền vào ID của nó
        {
          
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }

        //View detail - phương thức tìm kiếm dựa trên khóa chính, nếu khóa chính là int truyền thằng cái ID vào, cách 2 dùng dạng singerordefault
        public User ViewDetail (int id)
        {
            return db.Users.Find(id);
        }
        public List<string> GetListCredential(string userName)
        {
            var user = db.Users.Single(x => x.UserName == userName);
            var data = (from a in db.Credentials
                        join b in db.UserGroups on a.UserGroupID equals b.ID
                        join c in db.Roles on a.RoleID equals c.ID
                        where b.ID == user.GroupID
                        select new
                        {
                            RoleID = a.RoleID,
                            UserGroupID = a.UserGroupID
                        }).AsEnumerable().Select(x => new Credential()
                        {
                            RoleID = x.RoleID,
                            UserGroupID = x.UserGroupID
                        });
            return data.Select(x => x.RoleID).ToList();

        }
        //login
        public int Login(string userName, string passWord, bool isLoginAdmin = false)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
            {
                return 0;
            }
            else
            {
                if (isLoginAdmin == true)
                {
                    if (result.GroupID == CommonConstants.ADMIN_GROUP || result.GroupID == CommonConstants.MOD_GROUP)
                    {
                        if (result.Status == false)
                        {
                            return -1;
                        }
                        else
                        {
                            if (result.Password == passWord)
                                return 1;
                            else
                                return -2;
                        }
                    }
                    else
                    {
                        return -3;
                    }
                }
                else
                {
                    if (result.Status == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (result.Password == passWord)
                            return 1;
                        else
                            return -2;
                    }
                }
            }
        }
        public bool CheckUserName(string userName)
        {
            return db.Users.Count(x => x.UserName == userName) > 0; //check User trùng
        }
        public bool CheckEmail(string email)
        {
            return db.Users.Count(x => x.Email == email) > 0; //check trùng
        }
    }
}
