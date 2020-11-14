using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Tls;
using KhaoSat.Manager;
using KhaoSat.Models;
using KhaoSat.Utils;

namespace KhaoSat.API.Controllers
{
    [Route("api/khao-sat")]
    [ApiController]
    public class SurveysController : ControllerBase
    {
        private readonly ISurveyManager _surveysManager;
        private readonly IQuestionsManager _questionsManager;
        public SurveysController(ISurveyManager Surveys, IQuestionsManager questionsManager)
        {
            this._surveysManager = Surveys;
            this._questionsManager = questionsManager;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Surveys inputModel)
        {
            try
            {

                inputModel.CreatedDate = DateTime.Now;
                var data = await _surveysManager.Create(inputModel);
                if (inputModel.Questions != null)
                {
                    await CreateQuestion(inputModel);
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task CreateQuestion(Surveys inputmodel)
        {
            try
            {
                await _questionsManager.DeleteList(inputmodel.Id);
                await _questionsManager.CreateList(inputmodel.Questions, inputmodel.Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet("get-active")]
        public async Task<IActionResult> Find_By_ID()
        {
            try
            {
                var data = await _surveysManager.GetSurveyActive();
                return Ok(data);
            }
            catch (Exception ex)
            {

                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] Surveys inputModel)
        {
            try
            {
                var data = await _surveysManager.Find_By_Id(inputModel.Id);
                if (data == null)
                {
                    throw new Exception(MessageConst.DATA_NOT_FOUND);
                }
                inputModel.UpdatedDate = DateTime.Now;
                inputModel.CreatedDate = data.CreatedDate;
                await _surveysManager.Update(inputModel);
                if (inputModel.Questions != null)
                {
                    await _questionsManager.UpdateListQuestion(inputModel.Questions, inputModel.Id);
                }
                return Ok(data);

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("update-status")]
        public async Task<IActionResult> UpdateStatus([FromBody] Surveys inputModel)
        {
            try
            {
                var data = await _surveysManager.Find_By_Id(inputModel.Id);
                if (data == null)
                {
                    throw new Exception($"{MessageConst.DATA_NOT_FOUND}");
                }
                data.Status = inputModel.Status;
                await _surveysManager.Update(data);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] Surveys inputModel)
        {
            try
            {
                var data = await _surveysManager.Find_By_Id(inputModel.Id);
                if (data == null)
                {
                    throw new Exception($"{MessageConst.DATA_NOT_FOUND}");
                }
                await _surveysManager.Delete(inputModel.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("look-up")]
        public async Task<IActionResult> Look_Up()
        {
            try
            {
                var data = await _surveysManager.Look_up();
                return Ok(data.Select(c => new { Value = c.Id, Title = c.Title }));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("find-by-id")]
        public async Task<IActionResult> Find_By_ID(long id)
        {
            try
            {
                var data = await _surveysManager.Find_By_Id(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("get-list")]
        public async Task<IActionResult> GetList(string name, int status, int pageSize = 10, int pageNumber = 0)
        {
            try
            {
                var data = await _surveysManager.Get_List(name, status, pageSize, pageNumber);
                if (data == null)
                {
                    throw new Exception(MessageConst.DATA_NOT_FOUND);
                }
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("create-user-surveys")]
        public async Task<IActionResult> Create_user_survey(Users inputmodel)
        {
            try
            {
                await _surveysManager.CreateUserSurvey(inputmodel);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("thong-ke-khao-sat-theo-ma")]
        public async Task<IActionResult> SurveyById(long id)
        {
            try
            {
                var data = await _surveysManager.ThongKe(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
