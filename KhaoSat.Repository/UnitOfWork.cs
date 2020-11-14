using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using KhaoSat.Models;
using KhaoSat.Data;
using Microsoft.EntityFrameworkCore.Storage;
namespace KhaoSat.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        
        public IAnswerRepository AnswerRepository { get; }
        public IQuestionsRepository QuestionsRepository { get; }
        public ISurveyRepository SurveysRepository { get; }
        public IUserRepository UserRepository { get; }
        public IUserAnswersRepository UserAnswersRepository { get; }
        public IUserSurveysRepository UserSurveysRepository { get; }
        
        Task CreateTransaction();
        Task Commit();
        Task Rollback();
        Task SaveChange();
    }
    public class UnitOfWork : IUnitOfWork
    {
        KhaoSatDbContext _dbContext;
        IDbContextTransaction _transaction;

        public UnitOfWork(IDbContextFactory<KhaoSatDbContext> dbContextFactory, Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContextFactory.GetContext();
           
            UserAnswersRepository = new UserAnswersRepository(_dbContext);
            UserRepository = new UserRepository(_dbContext);
            UserSurveysRepository = new UserSurveysRepository(_dbContext);
            SurveysRepository = new SurveyRepository(_dbContext);
            AnswerRepository = new AnswerRepository(_dbContext);
            QuestionsRepository = new QuestionsRepository(_dbContext);
           
        }
        #region Transaction
        public async Task CreateTransaction()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _transaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
        }

        public async Task SaveChange()
        {
            await _dbContext.SaveChangesAsync();
        }

        #endregion

        private bool disposedValue = false; // To detect redundant calls

        

        public IAnswerRepository AnswerRepository { get; }

        public IQuestionsRepository QuestionsRepository { get; }

        public ISurveyRepository SurveysRepository { get; }

        public IUserRepository UserRepository { get; }

        public IUserAnswersRepository UserAnswersRepository { get; }

        public IUserSurveysRepository UserSurveysRepository { get; }

        
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
