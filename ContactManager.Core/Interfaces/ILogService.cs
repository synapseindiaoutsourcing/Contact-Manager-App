using ContactManager.Core.Entities;

using System;
using System.Threading.Tasks;

namespace ContactManager.Core.Interfaces
{
    public interface ILogService
    {
        Task CreateLog(int CreatedBy, Exception ex);
    }
}
