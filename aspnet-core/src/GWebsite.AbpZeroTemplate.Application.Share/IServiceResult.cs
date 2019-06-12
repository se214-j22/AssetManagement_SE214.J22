using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share
{
    public interface IServiceResult
    {
        bool IsSuccess { get; set; }
        string Message { get; set; }
        dynamic Payload { get; set; }
    }
}
