using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Providers.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.Providers
{
    public interface IProviderAppService
    {
        void CreateOrEditProvider(ProviderInput ProviderInput);
        ProviderInput GetProviderForEdit(int id);
        void DeleteProvider(int id);
        PagedResultDto<ProviderDto> GetProviders(ProviderFilter input);
        ProviderForViewDto GetProviderForView(int id);
    }
}
