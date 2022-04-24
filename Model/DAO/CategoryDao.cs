using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class CategoryDao
    {
        OnlineShopDbContext db = null;
        public CategoryDao() //khởi tạo contractor khởi tạo ra lớp online
        {
            db = new OnlineShopDbContext();
        }
        public long Insert(Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return category.ID;
        }
        public List<Category> ListAll() 
        {
            return db.Categories.Where(x => x.Status == true).ToList();
        }
        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id); //Find trực tieps id đó là khóa chính
        }
    }
}
