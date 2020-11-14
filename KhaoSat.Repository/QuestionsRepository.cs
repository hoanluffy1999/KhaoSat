using Microsoft.EntityFrameworkCore;
using KhaoSat.Data;
using KhaoSat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KhaoSat.Repository
{
    public interface IQuestionsRepository : IRepository<Questions>
    {

    }
    public class QuestionsRepository : Repository<Questions>,IQuestionsRepository
    {
        public QuestionsRepository(DbContext dbContext):base(dbContext)
        {
            
        }
    }
}
