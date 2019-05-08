using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.QuanLyVanHanhs.Dto;

namespace GWebsite.AbpZeroTemplate.Application.Share.QuanLyVanHanhs
{
    public interface IQuanLyVanHanhAppService
    {
        void CreateOrEditQuanLyVanHanh(QuanLyVanHanhInput xeInput);
        QuanLyVanHanhInput GetQuanLyVanHanhForEdit(int id);
        void DeleteQuanLyVanHanh(int id);
        PagedResultDto<QuanLyVanHanhDto> GetQuanLyVanHanhs(QuanLyVanHanhFilter input);
        QuanLyVanHanhForViewDto GetQuanLyVanHanhForView(int id);
    }
}