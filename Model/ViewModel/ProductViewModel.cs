using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ViewModel
{
    //đối tượng customize sẽ tổng hợp các thuộc tính cần thiết ở trong view mà mình muốn truy cập
    public class ProductViewModel
    {
        public long ID { set; get; }
        public string Images { set; get; }
        public string Name { set; get; }
        public decimal? Price { set; get; }
        public string CateName { set; get; }
        public string CateMetaTitle { set; get; }
        public string MetaTitle { set; get; }
        public DateTime? CreatedDate { set; get; }
    }
}
