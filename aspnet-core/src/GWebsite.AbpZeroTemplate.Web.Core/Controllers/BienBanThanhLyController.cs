using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.BienBanThanhLys;
using GWebsite.AbpZeroTemplate.Application.Share.BienBanThanhLys.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
	[Route("api/[controller]/[action]")]
	public class BienBanThanhLyController : GWebsiteControllerBase
	{
		private readonly IBienBanThanhLyAppService bienBanThanhLyAppService;

		public BienBanThanhLyController(IBienBanThanhLyAppService bienBanThanhLyAppService)
		{
			this.bienBanThanhLyAppService = bienBanThanhLyAppService;
		}

		[HttpGet]
		public PagedResultDto<BienBanThanhLyForViewDto> GetBienBanThanhLysByFilter(BienBanThanhLyFilter bienBanThanhLyFilter)
		{
			return bienBanThanhLyAppService.GetBienBanThanhLys(bienBanThanhLyFilter);
		}

		[HttpGet]
		public BienBanThanhLyInput GetBienBanThanhLyForEdit(int id)
		{
			return bienBanThanhLyAppService.GetBienBanThanhLyForEdit(id);
		}

		[HttpPost]
		public void CreateOrEditBienBanThanhLy([FromBody] BienBanThanhLyInput input)
		{
			bienBanThanhLyAppService.CreateOrEditBienBanThanhLy(input);
		}

		[HttpDelete("{id}")]
		public void DeleteBienBanThanhLy(int id)
		{
			bienBanThanhLyAppService.DeleteBienBanThanhLy(id);
		}

		[HttpGet]
		public BienBanThanhLyDto GetBienBanThanhLyForView(int id)
		{
			return bienBanThanhLyAppService.GetBienBanThanhLyForView(id);
		}
	}
}