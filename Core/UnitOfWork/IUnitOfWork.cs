using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.UnitOfWork {
    public interface IUnitOfWork : IDisposable{
        void Commit();
        Task CommitAsync();
    }
}
