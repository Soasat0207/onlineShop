using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

       
    }
}
