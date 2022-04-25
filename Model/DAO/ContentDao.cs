using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.DAO
{
    public class ContentDao
    {
        OnlineShopDbContext db = null;
        //public static string USER_SESSION = "USER_SESSION";
        public ContentDao() //tên của contractor
        {
            db = new OnlineShopDbContext();  
        }


            // trả về một cái content
        public Content GetByID(long id)
        {
            return db.Contents.Find(id);
        }

        public long Insert(Content content)
        {
            db.Contents.Add(content);
            db.SaveChanges();
            return content.ID;
        }

        //phương thức lấy ra tất cả các bản ghi
        public PagedList.IPagedList<Content> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Content> model = db.Contents;
            if (!string.IsNullOrEmpty(searchString)) //nếu chuỗi tìm kiếm khác rỗng
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

    }
}
