using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.DAO
{
    public class AboutDao
    {
        OnlineShopDbContext db = null;
        public AboutDao()
        {
            db = new OnlineShopDbContext();
        }
        public About GetActiveContact()
        {
            return db.Abouts.Single(x => x.Status == true);
        }
    }

}
