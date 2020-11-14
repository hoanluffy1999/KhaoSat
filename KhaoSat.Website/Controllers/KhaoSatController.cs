using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using KhaoSat.Models;
using KhaoSat.Utils;
using KhaoSat.Website.Models;

namespace KhaoSat.Website.Controllers
{
    public class KhaoSatController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string _domain;
        private readonly string _websiteDomain;
        public KhaoSatController(IConfiguration config)
        {
            this._config = config;
            this._domain = _config["APIDomain"].ToString();
            this._websiteDomain = _config["WebsiteDomain"].ToString();
        }
        public async Task<IActionResult> Index()
        {

            try
            {
               
                var data = await HttpHelper.GetData<Surveys>($"{_domain}/api/khao-sat/get-active/","", "false");
                //ViewData["CMSDomain"] = _config["CMSDomain"].ToString();
                return PartialView(data);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Erro404");
            }
        }
     
        public async Task<IActionResult> CreateUserSureys(Users inputModel)
        {
            try
            {
                    await HttpHelper.PostData<Users>(inputModel, $"{_domain}/api/khao-sat/create-user-surveys", "false");
                    return Json(new { Result = true, Message = "Khảo sát thành công" });
               
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }
        public async Task<IActionResult> QuestionCustomer()
        {
           
            return View();
        }
       
       
    }
}
