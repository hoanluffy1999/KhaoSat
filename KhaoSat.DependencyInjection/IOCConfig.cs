using System;
using KhaoSat.Repository;
using KhaoSat.Data;
using KhaoSat.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using KhaoSat.Manager;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace KhaoSat.DependencyInjection
{
    public class IOCConfig
    {
        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            // Db
            services.AddDbContext<KhaoSatDbContext>(ServiceLifetime.Scoped, ServiceLifetime.Singleton);

            services.AddTransient<IDbContextFactory<KhaoSatDbContext>, KhaoSatDbContextFactory>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();



           
            services.AddTransient<ISurveyManager, SurveysManager>();
           
            services.AddTransient<IQuestionsManager, QuestionsManager>();
         
        }
    }
}
