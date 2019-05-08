using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Share.Models
{
    public interface IModelAppService
    {
        void CreateOrEditModel(ModelInput modelInput);
        ModelInput GetModelForEdit(string model);
        void DeleteModel( int id);
        PagedResultDto<ModelDto> GetModels(ModelFilter input);
        ModelForViewDto GetModelForView(string model);
    }
}
