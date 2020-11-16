using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using KhaoSat.CMS.Models;
using KhaoSat.Models;
using KhaoSat.Utils;

namespace KhaoSat.CMS.Controllers
{
    [Route("khao-sat")]
    public class SurveysController : Controller
    {
        private readonly IConfiguration _config;
        private readonly string _domain;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ISession _session => _httpContextAccessor.HttpContext.Session;
        public SurveysController(IConfiguration config, IHttpContextAccessor httpContextAccessor)
        {
            this._config = config;
            this._domain = _config["APIDomain"].ToString();
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet("get-list")]
        public async Task<IActionResult> Get_List(string name, int status)
        {
            
           
            var data = await HttpHelper.GetData<List<Surveys>>($"{_domain}/api/khao-sat/get-list", $"name={name}&status={status}");
            return PartialView("_List", data);
        }
        [HttpGet("thong-ke")]
        public async Task<IActionResult> Thong_Ke(long id)
        {
          
            var data = await HttpHelper.GetData<Surveys>($"{_domain}/api/khao-sat/thong-ke-khao-sat-theo-ma", $"id={id}");
            return View("_thongke", data);
        }
       
        [HttpGet("them-moi")]
        public async Task<IActionResult> Create()
        {
           
            
            return View("_Create");
        }
        
        [HttpGet("sua")]
        public async Task<IActionResult> Update(long id)
        {
            
            var data = await HttpHelper.GetData<Surveys>($"{_domain}/api/khao-sat/find-by-id", $"id={id}");
            return View("_Update", data);
        }
        
        [HttpGet("lookup")]
        public async Task<IActionResult> Lookup()
        {
            var data = await HttpHelper.GetData<List<LookupModels>>($"{_domain}/api/khao-sat/look-up");
            return Json(new { Result = false, data = data });
        }
       
        [HttpPost("create-or-update")]
        public async Task<IActionResult> Create_Or_Update(Surveys inputModel)
        {
            try
            {
                if (inputModel.Id == 0)
                {
                    await HttpHelper.PostData<Surveys>(inputModel, $"{_domain}/api/khao-sat/create");
                    return Json(new { Result = true, Message = "Thêm mới dữ liệu thành công" });
                }
                else
                {
                    await HttpHelper.PostData<Surveys>(inputModel, $"{_domain}/api/khao-sat/update");
                    return Json(new { Result = true, Message = "Cập nhật dữ liệu thành công" });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }
        
        [HttpGet("danh-sach")]
        public async Task<IActionResult> Index()
        {
            
            ViewBag.Title = "Danh sách khảo sát";
            return View();
        }
        [HttpGet("CreateQuestion")]
        public async Task<IActionResult> CreateQuestion()
        {
            return PartialView("_CreateQuestion");
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var data = await HttpHelper.GetData<Surveys>($"{_domain}/api/khao-sat/find-by-id", $"id={id}");
                return Json(new { Result = false, data = data });
            }
            catch(Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }
        
        [HttpPost("delete-or-restore")]
        public async Task<IActionResult> Delete(Surveys inputmodel)
        {
            try
            {
                if (inputmodel.Status == (byte)StatusEnum.Removed)
                {
                    await HttpHelper.PostData<Surveys>(inputmodel, $"{_domain}/api/khao-sat/delete");
                    return Json(new { Result = true });
                }
                else
                {
                    await HttpHelper.PostData<Surveys>(inputmodel, $"{_domain}/api/khao-sat/update-status");
                    return Json(new { Result = true });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }
       
        [HttpPost("update-status")]
        public async Task<IActionResult> UpdateStatus(Surveys inputModel)
        {
            try
            {
                await HttpHelper.PostData<Surveys>(inputModel, $"{_domain}/api/khao-sat/update-status");
                return Json(new { Result = true, Message = "Cập nhật dữ liệu thành công" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = false, Message = ex.Message });
            }
        }
       
    }
}
