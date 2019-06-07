using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.CapPhats.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.CapPhats
{
    public interface ICapPhatAppService
    {
        void CreateOrEditCapPhat(CapPhatInput CapPhatInput);
        CapPhatInput GetCapPhatForEdit(int id);
        void DeleteCapPhat(int id);
        PagedResultDto<CapPhatOutput> GetCapPhats(CapPhatFilter input);
        CapPhatForViewDto GetCapPhatForView(int id);
    }
}
