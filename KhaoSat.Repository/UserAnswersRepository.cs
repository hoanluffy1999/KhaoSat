using Microsoft.EntityFrameworkCore;
using KhaoSat.Data;
using KhaoSat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KhaoSat.Repository
{
    public interface IUserAnswersRepository :IRepository<UserAnswers>
    {

    }
    public class UserAnswersRepository : Repository<UserAnswers>,IUserAnswersRepository
    {
        public UserAnswersRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
