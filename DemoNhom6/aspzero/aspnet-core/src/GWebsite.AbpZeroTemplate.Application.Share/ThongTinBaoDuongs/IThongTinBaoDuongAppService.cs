using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ThongTinBaoDuongs.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.ThongTinBaoDuongs
{
    public interface IThongTinBaoDuongAppService
    {
        void CreateOrEditThongTinBaoDuong(ThongTinBaoDuongInput xeInput);
        ThongTinBaoDuongInput GetThongTinBaoDuongForEdit(int id);
        void DeleteThongTinBaoDuong(int id);
        PagedResultDto<ThongTinBaoDuongDto> GetThongTinBaoDuongs(ThongTinBaoDuongFilter input);
        ThongTinBaoDuongForViewDto GetThongTinBaoDuongForView(int id);
    }
}