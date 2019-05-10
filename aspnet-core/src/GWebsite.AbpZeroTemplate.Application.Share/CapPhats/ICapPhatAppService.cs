using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.CapPhats.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.CapPhats
{
    public interface ICapPhatAppService
    {
        void CreateOrEditCapPhat(CapPhatInput capPhatInput);
        CapPhatInput GetCapPhatForEdit(int id);
        void DeleteCapPhat(int id);
        PagedResultDto<CapPhatDto> GetCapPhats(CapPhatFilter input);
        CapPhatForViewDto GetCapPhatForView(int id);
    }
}
