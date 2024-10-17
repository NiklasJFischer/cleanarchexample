using ChatApi.Data;
using ChatApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ChatApi.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class LogController : ApiController
    {


        public static void AddLog(Log log)
        {
            ArgumentNullException.ThrowIfNull(log, nameof(log));

            log.Id = Guid.NewGuid();
            LogStorage.logs.Add(log);
        }
    }
}
