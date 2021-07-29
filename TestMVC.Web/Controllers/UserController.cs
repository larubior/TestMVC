using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestMVC.Business.Interfaces;
using TestMVC.Model.User;

namespace TestMVC.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserBusiness userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            this.userBusiness = userBusiness;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> GetUsers()
        {
            return Json(await userBusiness.GetAllAsync(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<ActionResult> GetUser(int id)
        {
            return Json(await userBusiness.GetAsync(id), JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> Details(int? id)
        {
            UserViewModel model = new UserViewModel();

            if(id != null)
            {
                model = await userBusiness.GetAsync(Convert.ToInt32(id));
            }

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> SaveUser(UserViewModel request)
        {
            StringBuilder errors = new StringBuilder();

            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        errors.Append(modelError.ErrorMessage + "\r\n");
                    }
                }

                return Json(new { operation = false, message = errors.ToString() });
            }

            return Json(await userBusiness.SaveAsync(request));
        }

        [HttpPost]
        public async Task<ActionResult> ActivateUser(int id)
        {
            return Json(await userBusiness.ChangeStatusAsync(id, true));
        }

        [HttpPost]
        public async Task<ActionResult> DesactivateUser(int id)
        {
            return Json(await userBusiness.ChangeStatusAsync(id, false));
        }

        [HttpPost]
        public async Task<ActionResult> DeleteUser(int id)
        {
            return Json(await userBusiness.DeleteAsync(id));

        }
    }
}