using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;
using Model.DAO;
using OnlineShop.Common;
using PagedList.Mvc;
using PagedList;

namespace OnlineShop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        [HasCredential(RoleID = "VIEW_USER")]
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new UserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString; //gán vào view bag để để tự lưu ở trên thanh search - Viewbag là 1 cái card data truyên f từ controller xuống, xuyên suốt từ controler --> view

            return View(model);
        }
        [HttpGet] //giành cho return 1 cái view, tải trang giao diện 
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpGet]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit (int id)
        {
            var user = new UserDao().ViewDetail(id);
            return View(user);
        }


        [HttpPost] //giành cho foward poosst lên
        [HasCredential(RoleID = "ADD_USER")]
        public ActionResult Create (User user) 
        { 
            DateTime date =  DateTime.Now;

            if (ModelState.IsValid) //kiểm tra xem nó có validate form không
            {
                var dao = new UserDao(); //khởi tạo biến dao
                var encryptedMd5Pas = Encryptor.MD5Hash(user.Password); //mã hóa mật khẩu
                user.Password = encryptedMd5Pas;
                user.CreatedDate = date;
                long id = dao.Insert(user); //insert biến Dao, gán biến id vào
                if (id > 0) //insert thành công
                {
                    SetAlert("Thêm user thành công", "success");
                    return RedirectToAction("Index", "User"); //chuyển vê trang quản lý
                }
                else
                {
                    ModelState.AddModelError("", "Thêm user không thành công");
                }
            }
            return View("Index");
        }
        //Phương thức post giành cho edit
        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public ActionResult Edit(User user)
        {
            //DateTime date = DateTime.Now;

            if (ModelState.IsValid) //kiểm tra xem nó có validate form không
            {
                var dao = new UserDao(); //khởi tạo biến dao

                if (!string.IsNullOrEmpty(user.Password))
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(user.Password); //mã hóa mật khẩu
                    user.Password = encryptedMd5Pas;
                }

                //user.CreatedDate = date;

                var result = dao.Update(user); //insert biến Dao, gán biến id vào
                if (result) //thành công
                {
                    SetAlert("Cập nhật user thành công", "success");
                    return RedirectToAction("Index", "User"); //chuyển vê trang quản lý
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật user không thành công");
                }
            }
            return View("Index");
        }

        [HttpDelete]
        [HasCredential(RoleID = "DELETE_USER")]
        public ActionResult Delete(int id)
        {
            new UserDao().Delete(id);
            return RedirectToAction("Index");
        }

        //change Status 
        [HttpPost]
        [HasCredential(RoleID = "EDIT_USER")]
        public JsonResult ChangeStatus(long id)
        {
            var result = new UserDao().ChangeStatus(id);
            return Json(new
            {
                status = result
            });
        }
    }
}