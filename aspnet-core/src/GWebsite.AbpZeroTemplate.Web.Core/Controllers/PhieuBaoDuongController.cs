using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.PhieuBaoDuongs;
using GWebsite.AbpZeroTemplate.Application.Share.PhieuBaoDuongs.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
	[Route("api/[controller]/[action]")]
	public class PhieuBaoDuongController : GWebsiteControllerBase
	{
		private readonly IPhieuBaoDuongAppService phieuBaoDuongAppService;

		public PhieuBaoDuongController(IPhieuBaoDuongAppService phieuBaoDuongAppService)
		{
			this.phieuBaoDuongAppService = phieuBaoDuongAppService;
		}

		[HttpGet]
		public PagedResultDto<PhieuBaoDuongForViewDto> GetPhieuBaoDuongsByFilter(PhieuBaoDuongFilter phieuBaoDuongFilter)
		{
			return phieuBaoDuongAppService.GetPhieuBaoDuongs(phieuBaoDuongFilter);
		}

		[HttpGet]
		public PhieuBaoDuongInput GetPhieuBaoDuongForEdit(int id)
		{
			return phieuBaoDuongAppService.GetPhieuBaoDuongForEdit(id);
		}

		[HttpPost]
		public void CreateOrEditPhieuBaoDuong([FromBody] PhieuBaoDuongInput input)
		{
			phieuBaoDuongAppService.CreateOrEditPhieuBaoDuong(input);
		}

		[HttpDelete("{id}")]
		public void DeletePhieuBaoDuong(int id)
		{
			phieuBaoDuongAppService.DeletePhieuBaoDuong(id);
		}

		[HttpGet]
		public PhieuBaoDuongDto GetPhieuBaoDuongForView(int id)
		{
			return phieuBaoDuongAppService.GetPhieuBaoDuongForView(id);
		}
	}
}
