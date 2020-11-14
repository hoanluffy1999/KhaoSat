using Microsoft.EntityFrameworkCore;
using KhaoSat.Data;
using KhaoSat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KhaoSat.Repository
{
    public interface IUserSurveysRepository: IRepository<UserSurveys>
    {

    }
    public class UserSurveysRepository :Repository<UserSurveys> , IUserSurveysRepository
    {
        public UserSurveysRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
