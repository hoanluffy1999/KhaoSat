using Microsoft.EntityFrameworkCore;
using KhaoSat.Data;
using KhaoSat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KhaoSat.Repository
{
    public interface IAnswerRepository : IRepository<Answer>
    {

    }
    public class AnswerRepository:Repository<Answer>,IAnswerRepository
    {
        public AnswerRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
