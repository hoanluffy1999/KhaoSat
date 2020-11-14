using System;
using System.Collections.Generic;
using System.Text;

namespace KhaoSat.Data
{
    public interface ITransaction : IDisposable
    {
        void Commit();

        void Rollback();
    }
}
