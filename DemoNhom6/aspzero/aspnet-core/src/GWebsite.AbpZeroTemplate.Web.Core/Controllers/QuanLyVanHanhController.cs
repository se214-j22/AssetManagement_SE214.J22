using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.QuanLyVanHanhs;
using GWebsite.AbpZeroTemplate.Application.Share.QuanLyVanHanhs.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")] 
    public class QuanLyVanHanhController : GWebsiteControllerBase
    {
        private readonly IQuanLyVanHanhAppService quanlyvanhanhAppService;

        public QuanLyVanHanhController(IQuanLyVanHanhAppService quanlyvanhanhAppService)
        {
            this.quanlyvanhanhAppService = quanlyvanhanhAppService;
        }

        [HttpGet]
        public PagedResultDto<QuanLyVanHanhDto> GetQuanLyVanHanhsByFilter(QuanLyVanHanhFilter quanlyvanhanhFilter)
        {
            return quanlyvanhanhAppService.GetQuanLyVanHanhs(quanlyvanhanhFilter);
        }

        [HttpGet]
        public QuanLyVanHanhInput GetQuanLyVanHanhForEdit(int id)
        {
            return quanlyvanhanhAppService.GetQuanLyVanHanhForEdit(id);
        }

        [HttpPost]
        public void CreateOrEditQuanLyVanHanh([FromBody] QuanLyVanHanhInput input)
        {
            quanlyvanhanhAppService.CreateOrEditQuanLyVanHanh(input);
        }

        [HttpDelete("{id}")]
        public void DeleteQuanLyVanHanh(int id)
        {
            quanlyvanhanhAppService.DeleteQuanLyVanHanh(id);
        }

        [HttpGet]
        public QuanLyVanHanhForViewDto GetQuanLyVanHanhForView(int id)
        {
            return quanlyvanhanhAppService.GetQuanLyVanHanhForView(id);
        }
    }
}
