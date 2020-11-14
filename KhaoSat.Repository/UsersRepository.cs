using Microsoft.EntityFrameworkCore;
using KhaoSat.Data;
using KhaoSat.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace KhaoSat.Repository
{
    public interface IUserRepository : IRepository<Users>
    {

    }
    public class UserRepository : Repository<Users>, IUserRepository
    {
        public UserRepository(DbContext context) : base(context)
        {

        }
    }
}
