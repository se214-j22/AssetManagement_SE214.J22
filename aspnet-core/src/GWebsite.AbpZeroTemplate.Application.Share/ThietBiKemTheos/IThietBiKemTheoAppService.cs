using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ThietBiKemTheos.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.ThietBiKemTheos
{
   public interface IThietBiKemTheoAppService 
    {
        void CreateOrEditThietBiKemTheo(ThietBiKemTheoInput tbktInput);
        ThietBiKemTheoInput GetThietBiKemTheoForEdit(int id);
        void DeleteThietBiKemTheo(int id);
        PagedResultDto<ThietBiKemTheoDto> GetThietBiKemTheos(ThietBiKemTheoFilter input);
       ThietBiKemTheoForViewDto GetThietBiKemTheoForView(int id);
    }
}
