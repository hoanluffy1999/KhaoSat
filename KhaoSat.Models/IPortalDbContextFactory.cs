using System;
using System.Collections.Generic;
using System.Text;
using KhaoSat.Data;
using Microsoft.EntityFrameworkCore;
namespace KhaoSat.Models
{
    public interface IKhaoSatDbContextFactory : IDbContextFactory<KhaoSatDbContext>
    {

    }
    public class KhaoSatDbContextFactory : IKhaoSatDbContextFactory
    {
        private readonly DbContextOptions<KhaoSatDbContext> _options;
        private KhaoSatDbContext _context;
        public KhaoSatDbContextFactory(DbContextOptions<KhaoSatDbContext> options)
        {
            _options = options;
        }
        public KhaoSatDbContext GetContext()
        {
            if (_context == null) _context = new KhaoSatDbContext(_options);
            return _context;
        }
    }
}
