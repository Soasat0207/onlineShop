using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class MenuDao
    {
        OnlineShopDbContext db = null; //khai báo biến
        public MenuDao() //đặt tên contractor
        {
            db = new OnlineShopDbContext();
        }

        public List<Menu> ListByGroupId(int groupId) //trả về list menu
        {
            return db.Menus.Where(x => x.TypeID == groupId && x.Status == true).OrderBy(x => x.DisplayOrder).ToList(); //lấy ra danh sách menu, mắ cđịnh là tăng dần
        }
    }
}
