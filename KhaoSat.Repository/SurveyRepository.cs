using Microsoft.EntityFrameworkCore;
using KhaoSat.Data;
using KhaoSat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KhaoSat.Repository
{
    public interface ISurveyRepository : IRepository<Surveys>
    {

    }
    public class SurveyRepository :Repository<Surveys>,ISurveyRepository
    {
        public SurveyRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
