using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.BienBanBanGiaoTaiSans;
using GWebsite.AbpZeroTemplate.Application.Share.BienBanBanGiaoTaiSans.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
	[Route("api/[controller]/[action]")]
	public class BienBanBanGiaoTaiSanController : GWebsiteControllerBase
	{
		private readonly IBienBanBanGiaoTaiSanAppService bienBanBanGiaoTaiSanAppService;

		public BienBanBanGiaoTaiSanController(IBienBanBanGiaoTaiSanAppService bienBanBanGiaoTaiSanAppService)
		{
			this.bienBanBanGiaoTaiSanAppService = bienBanBanGiaoTaiSanAppService;
		}

		[HttpGet]
		public PagedResultDto<BienBanBanGiaoTaiSanForViewDto> GetBienBanBanGiaoTaiSansByFilter(BienBanBanGiaoTaiSanFilter bienBanBanGiaoTaiSanFilter)
		{
			return bienBanBanGiaoTaiSanAppService.GetBienBanBanGiaoTaiSans(bienBanBanGiaoTaiSanFilter);
		}

		[HttpGet]
		public BienBanBanGiaoTaiSanInput GetBienBanBanGiaoTaiSanForEdit(int id)
		{
			return bienBanBanGiaoTaiSanAppService.GetBienBanBanGiaoTaiSanForEdit(id);
		}

		[HttpPost]
		public void CreateOrEditBienBanBanGiaoTaiSan([FromBody] BienBanBanGiaoTaiSanInput input)
		{
			bienBanBanGiaoTaiSanAppService.CreateOrEditBienBanBanGiaoTaiSan(input);
		}

		[HttpDelete("{id}")]
		public void DeleteBienBanBanGiaoTaiSan(int id)
		{
			bienBanBanGiaoTaiSanAppService.DeleteBienBanBanGiaoTaiSan(id);
		}

		[HttpGet]
		public BienBanBanGiaoTaiSanDto GetBienBanBanGiaoTaiSanForView(int id)
		{
			return bienBanBanGiaoTaiSanAppService.GetBienBanBanGiaoTaiSanForView(id);
		}
	}
}