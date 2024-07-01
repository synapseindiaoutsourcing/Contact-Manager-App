using ContactManager.Core.Interfaces;
using ContactManager.Core.Entities;

namespace ContactManager.Core.Services
{

    public class LogService : ILogService
    {
        private readonly IAsyncRepository<LogInfo> _logRepository;

        public LogService(IAsyncRepository<LogInfo> logRepository)
        {
            _logRepository = logRepository;

        }

        public async Task CreateLog(int CreatedBy, Exception ex)
        {
            var log = new LogInfo()
            {
                CreatedBy = CreatedBy,
                CreatedDate = DateTime.Now,
                ExtraData = (ex.InnerException != null ? (ex.InnerException.InnerException != null) : false) ? ex.InnerException.InnerException.Message : null,
                IsActive = true,
                InnerMessage = ex.InnerException != null ? (ex.InnerException.Message) : null,
                Message = ex.Message,
                Source = ex.Source,
                StackTrace = ex.StackTrace
            };

            await _logRepository.AddAsync(log);
        }
       
    }
}
