using Model.DAO;
using Model.EF;
using OnlineShop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ContentDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        //Truyền view bag để hiển thị category drop down list
        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        //Get ra form edit
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var dao = new ContentDao();
            var content = dao.GetByID(id); //id truyền vào

            SetViewBag(content.CategoryID); //content truyền vào selected value của dropdown list
            //sau đó content truyền vào selected value
            return View(content);
        }

        [HttpPost] //sau khi post lên nó lại được gán viewbag nó lại trả về cái trang này
        public ActionResult Create(Content model)
        {
            if (ModelState.IsValid)
            {
                var currentCulture = Session[CommonConstants.CurrentCulture];
                model.Language = currentCulture.ToString();
                var id = new ContentDao().Insert(model);
                if (id > 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", StaticResources.Resources.InsertCategoryFailed);
                }
            }
            SetViewBag(); //để chưa kết nối được thì view bag vẫn còn
            return View(model);
        }

        [HttpPost] 
        public ActionResult Edit (Content model)
        {
            if (ModelState.IsValid)
            {

            }
            SetViewBag(model.CategoryID); 
            return View();
        }
        //hàm Set view bag để lấy dữ liệu phần danh mục để hiển thị trong drop downlist
        public void SetViewBag(long? selectedId = null)
        {
            var dao = new CategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", selectedId);
        }

    }
}