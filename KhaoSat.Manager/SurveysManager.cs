using Microsoft.Extensions.Logging;
using KhaoSat.Models;
using KhaoSat.Repository;
using KhaoSat.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaoSat.Manager
{
    public interface ISurveyManager
    {
        Task<Surveys> Create(Surveys inputModel);
        Task Update(Surveys inputModel);
        Task Delete(long id);
        Task<Surveys> Find_By_Id(long id);
        Task<List<Surveys>> Look_up();
        Task<List<Surveys>> Get_List(string name, int status, int pageSize, int pageNumber);
        Task<Surveys> GetSurveyActive();
        Task CreateUserSurvey(Users users);
        Task<Surveys> ThongKe(long surveysId);
        Task<SurveyViewModel> GetListUserByServeyId(long id);

    }
    public class SurveysManager : ISurveyManager
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<Surveys> _logger;
        public SurveysManager(IUnitOfWork unitOfWork, ILogger<Surveys> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Surveys> Create(Surveys inputModel)
        {
            try
            {
                var result = await _unitOfWork.SurveysRepository.Add(inputModel);
                await _unitOfWork.SaveChange();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Update(Surveys inputModel)
        {
            try
            {
                await _unitOfWork.SurveysRepository.Update(inputModel);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task Delete(long id)
        {
            try
            {
                var item = await _unitOfWork.SurveysRepository.Get(c => c.Id == id);
                item.Status = (byte)RolesEnum.Delete;
                await _unitOfWork.SurveysRepository.Update(item);
                await _unitOfWork.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Surveys> Find_By_Id(long id)
        {
            var survey = await _unitOfWork.SurveysRepository.Get(c => c.Id == id);
            if (survey != null)
            {
                var questions = (await _unitOfWork.QuestionsRepository.FindBy(x => x.SurveyId == survey.Id)).ToList();
                if (questions != null)
                {
                    survey.Questions = questions;
                    foreach (var question in questions)
                    {
                        var answers = (await _unitOfWork.AnswerRepository.FindBy(x => x.QuestionId == question.Id)).ToList();
                        question.Answers = answers;
                    }
                }
            }
            return survey;
        }



        public async Task<List<Surveys>> Get_List(string name, int status, int pageSize = 10, int pageNumber = 0)
        {
            try
            {
                var data = (await _unitOfWork.SurveysRepository.FindBy(x => (x.Status == status || status == (int)StatusEnum.All) && (x.Status != (byte)StatusEnum.Removed || status == (int)StatusEnum.Removed))).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Surveys>> Look_up()
        {
            try
            {
                var data = (await _unitOfWork.SurveysRepository.FindBy(c => c.Status == (int)StatusEnum.Active)).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Surveys> GetSurveyActive()
        {
            try
            {
                var data = (await _unitOfWork.SurveysRepository.FindBy(x => x.Status == (byte)StatusEnum.Active)).ToList().FirstOrDefault();
                if (data == null)
                {
                    throw new Exception("404");
                }

                var questions = (await _unitOfWork.QuestionsRepository.FindBy(x => x.SurveyId == data.Id)).ToList();
                if (questions != null)
                {
                    data.Questions = questions;
                    foreach (var question in questions)
                    {
                        var answers = (await _unitOfWork.AnswerRepository.FindBy(x => x.QuestionId == question.Id)).OrderByDescending(x => x.Id).ToList();
                        question.Answers = answers;
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task CreateUserSurvey(Users users)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.Add(users);
                await _unitOfWork.SaveChange();

                var userSurvey = await _unitOfWork.UserSurveysRepository.Add(new UserSurveys()
                {
                    SurveyId = users.Surveys.Id,
                    UserId = user.Id
                });
                await _unitOfWork.SaveChange();
                foreach (var item in users.ListAnswers)
                {
                    item.UserSurveyId = userSurvey.Id;
                    await _unitOfWork.UserAnswersRepository.Add(item);
                    await _unitOfWork.SaveChange();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SurveyViewModel> GetListUserByServeyId(long id)
        {
            try
            {
                var suveys = await _unitOfWork.SurveysRepository.Get(x => x.Id == id);
                if (suveys == null)
                {
                    throw new Exception("404");
                }
                var userSuveys = (await _unitOfWork.UserSurveysRepository.FindBy(x => x.SurveyId == id)).ToList();
                if (userSuveys == null)
                {
                    throw new Exception("404");

                }
                var listUserAns = new List<UserAnswers>();
                foreach (var userSuvey in userSuveys)
                {

                    var listAns = (await _unitOfWork.UserAnswersRepository.FindBy(x => x.UserSurveyId == userSuvey.Id)).ToList();
                    if (listAns != null)
                    {
                        listUserAns.AddRange(listAns);
                    }
                }

                var suveyViewModel = new SurveyViewModel()
                {
                    Id = suveys.Id,
                    Description = suveys.Description,
                    Title = suveys.Title,
                    Status = suveys.Status,
                    UserAnswers = listUserAns
                };
                return suveyViewModel;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Surveys> ThongKe(long surveysId)
        {
            try
            {
                var survey = await _unitOfWork.SurveysRepository.Get(x => x.Id == surveysId&&x.Status != (byte)StatusEnum.Removed);            
                var userSurveys = (await _unitOfWork.UserSurveysRepository.FindBy(x => x.SurveyId == survey.Id)).ToList();
                int totalUser = userSurveys.Count;
                if (userSurveys == null)
                {
                    throw new Exception("404");
                }
                var listQuestion = new List<Questions>();
                var listAnsUser = new List<UserAnswers>();
                foreach (var item in userSurveys)
                {
                    var ansSurvey = await _unitOfWork.UserAnswersRepository.FindBy(x => x.UserSurveyId == item.Id);
                    listAnsUser.AddRange(ansSurvey);
                }
                if (listAnsUser == null)
                {
                    throw new Exception("404");
                }
                foreach (var item in listAnsUser)
                {
                    var question = await _unitOfWork.QuestionsRepository.Get(x => x.Id == item.QuestionId);
                    if (question != null)
                    {
                        var listAns = (await _unitOfWork.AnswerRepository.FindBy(x => x.QuestionId == question.Id)).ToList();

                        if (listAns != null)
                        {
                            foreach (var ans in listAns)
                            {
                                int soLanChon = (await _unitOfWork.UserAnswersRepository.FindBy(x => x.AnswerId == ans.Id)).ToList().Count;
                                double tile = ((double)soLanChon/(double)totalUser)*100;
                                ans.TiLeChon = tile;
                              
                            }
                        }
                        question.Answers = listAns.ToList();
                        listQuestion.Add(question);
                    }
                }
                survey.Questions = listQuestion;
                return survey;
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
