using BuzzLogic.Transaction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;


namespace MVCNetCore.Controllers
{
    public class RegisterController : Controller
    {
        private readonly RegisterBL _bussLogic;
        private readonly ILogger<RegisterController> _logger;

        public RegisterController(ILogger<RegisterController> logger)
        {
            _bussLogic = new RegisterBL(logger);
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Register(IFormCollection form, IFormFile FileNPWP, IFormFile FilePowerOfAttorey)
        {
            var responseAPI = await _bussLogic.Register(form, FileNPWP, FilePowerOfAttorey);
            if (responseAPI.Success)
            {
                return Json(new { success = true, message = "Register Success." });
            }
            else
            {
                return Json(new { success = false, message = "Register failed, please contact the administator." });
            }
        }




    }
}
