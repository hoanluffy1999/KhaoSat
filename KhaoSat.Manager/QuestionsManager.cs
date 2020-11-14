using Microsoft.Extensions.Logging;
using KhaoSat.Models;
using KhaoSat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhaoSat.Manager
{
    public interface IQuestionsManager
    {
        Task DeleteList(long surveyId);
        Task CreateList(List<Questions> listQuestion, long surveyId);
        Task UpdateListQuestion(List<Questions> listQuestion, long surveyId);
    }

    public class QuestionsManager : IQuestionsManager
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<Questions> _logger;
        public QuestionsManager(IUnitOfWork unitOfWork, ILogger<Questions> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task CreateList(List<Questions> listQuestion, long surveyId)
        {
            try
            {
                if (listQuestion != null)
                {
                    foreach (var item in listQuestion)
                    {
                        item.SurveyId = surveyId;
                        item.Id = 0;
                        var question = await _unitOfWork.QuestionsRepository.Add(item);
                        
                        await _unitOfWork.SaveChange();
                        if (item.Answers != null)
                        {
                            await DeleteListAnswer(question.Id);
                            await createListAnswer(item.Answers, question.Id);
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeleteListAnswer(long questionId)
        {
            try
            {
                var data = (await _unitOfWork.AnswerRepository.FindBy(x => x.QuestionId == questionId)).ToList();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        await _unitOfWork.AnswerRepository.Delete(item);
                        await _unitOfWork.SaveChange();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task createListAnswer(List<Answer> listAnswer, long questionId)
        {
            try
            {
                foreach (var item in listAnswer)
                {
                    item.QuestionId = questionId;
                    item.Id = 0;
                    await _unitOfWork.AnswerRepository.Add(item);
                    await _unitOfWork.SaveChange();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeleteList(long surveyId)
        {
            try
            {
                var data = (await _unitOfWork.QuestionsRepository.FindBy(x => x.SurveyId == surveyId)).ToList();
                if (data != null)
                {
                    foreach (var item in data)
                    {
                        await _unitOfWork.QuestionsRepository.Delete(item);
                        await _unitOfWork.SaveChange();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task UpdateListQuestion(List<Questions> listQuestionNew, long surveyId)
        {
            try
            {
                var listQuestionOld = (await _unitOfWork.QuestionsRepository.FindBy(x => x.SurveyId == surveyId)).ToList();
                foreach(var item in listQuestionOld)
                {
                    var quesExit =  listQuestionNew.Find(x => x.Id == item.Id);
                    if(quesExit == null)
                    {
                        await _unitOfWork.QuestionsRepository.Delete(item);
                        await _unitOfWork.SaveChange();
                        await DeleteListAnswer(item.Id); 
                    }    
                    
                }    
                if(listQuestionNew !=null)
                {
                    foreach (var question in listQuestionNew)
                    {
                        if (question.Id == 0)
                        {
                            question.SurveyId = surveyId;
                            var ques = await _unitOfWork.QuestionsRepository.Add(question);
                            await _unitOfWork.SaveChange();
                            await DeleteListAnswer(ques.Id);
                            await createListAnswer(ques.Answers, ques.Id);
                        }
                        else if (question.Id > 0)
                        {
                            await _unitOfWork.QuestionsRepository.Update(question);
                            await _unitOfWork.SaveChange();
                            await updateListAnswer(question.Answers, question.Id);
                        }

                    }
                }    
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task updateListAnswer(List<Answer> listAnswerNew, long questionId)
        {
            try
            {
                var listAnswerOld = (await _unitOfWork.AnswerRepository.FindBy(x => x.QuestionId == questionId)).ToList();
                foreach (var item in listAnswerOld)
                {
                    var ansExit = listAnswerNew.Find(x => x.Id == item.Id);
                    if(ansExit == null)
                    {
                        await _unitOfWork.AnswerRepository.Delete(item);
                        await _unitOfWork.SaveChange();
                    }    
                }
                if(listAnswerNew != null)
                {
                    foreach (var ans in listAnswerNew)
                    {
                        if (ans.Id == 0)
                        {
                            ans.QuestionId = questionId;
                            await _unitOfWork.AnswerRepository.Add(ans);
                            await _unitOfWork.SaveChange();
                        }
                        else if (ans.Id > 0)
                        {
                            await _unitOfWork.AnswerRepository.Update(ans);
                            await _unitOfWork.SaveChange();
                        }

                    }
                }    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
