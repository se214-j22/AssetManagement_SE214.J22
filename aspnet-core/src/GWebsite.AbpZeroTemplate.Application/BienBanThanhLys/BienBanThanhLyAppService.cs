using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper.QueryableExtensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.BienBanThanhLys;
using GWebsite.AbpZeroTemplate.Application.Share.BienBanThanhLys.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.BienBanThanhLys
{
	[AbpAuthorize(GWebsitePermissions.Pages_Administration_BienBanThanhLy)]
	public class BienBanThanhLyAppService : GWebsiteAppServiceBase, IBienBanThanhLyAppService
	{
		private readonly IRepository<BienBanThanhLy> bienBanThanhLyRepository;
		private readonly IRepository<TaiSanCoDinh> taiSanCoDinhRepository;

		public BienBanThanhLyAppService(IRepository<BienBanThanhLy> bienBanThanhLyRepository, IRepository<TaiSanCoDinh> taiSanCoDinhRepository)
		{
			this.bienBanThanhLyRepository = bienBanThanhLyRepository;
			this.taiSanCoDinhRepository = taiSanCoDinhRepository;
		}

		#region Public Method

		public void CreateOrEditBienBanThanhLy(BienBanThanhLyInput bienBanThanhLyInput)
		{
			if (bienBanThanhLyInput.Id == 0)
			{
				Create(bienBanThanhLyInput);
			}
			else
			{
				Update(bienBanThanhLyInput);
			}
		}

		public void DeleteBienBanThanhLy(int id)
		{
			var bienBanThanhLyEntity = bienBanThanhLyRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
			if (bienBanThanhLyEntity != null)
			{
				bienBanThanhLyEntity.IsDelete = true;
				bienBanThanhLyRepository.Update(bienBanThanhLyEntity);
				CurrentUnitOfWork.SaveChanges();
			}
		}

		public BienBanThanhLyInput GetBienBanThanhLyForEdit(int id)
		{
			var bienBanThanhLyEntity = bienBanThanhLyRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
			if (bienBanThanhLyEntity == null)
			{
				return null;
			}
			return ObjectMapper.Map<BienBanThanhLyInput>(bienBanThanhLyEntity);
		}

		public BienBanThanhLyDto GetBienBanThanhLyForView(int id)
		{
			var bienBanThanhLyEntity = bienBanThanhLyRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
			if (bienBanThanhLyEntity == null)
			{
				return null;
			}
			return ObjectMapper.Map<BienBanThanhLyDto>(bienBanThanhLyEntity);
		}

		public PagedResultDto<BienBanThanhLyForViewDto> GetBienBanThanhLys(BienBanThanhLyFilter input)
		{
			IQueryable<BienBanThanhLy> query = bienBanThanhLyRepository.GetAll().Where(x => !x.IsDelete);

			// filter by value
			if (input.TenDonViThanhLy != null)
			{
				query = query.Where(x => x.TenDonViThanhLy.ToLower().Contains(input.TenDonViThanhLy.ToLower()));
			}
			// IQueryable
			IQueryable<BienBanThanhLyDto> bienBanThanhLyQuery = query.ProjectTo<BienBanThanhLyDto>();

			IQueryable<TaiSanCoDinh> taiSanCoDinhQuery = taiSanCoDinhRepository.GetAll().Where(x => x.IsDelete == false);

			IQueryable<BienBanThanhLyForViewDto> bienBanThanhLyOutputQuery = (from bbtl in bienBanThanhLyQuery
																		join tscd in taiSanCoDinhQuery
																		on bbtl.TaiSanCoDinhId equals tscd.Id
																		select new BienBanThanhLyForViewDto()
																		{
																			BienBanThanhLy = bbtl,
																			TenTaiSanCoDinh = tscd.TenTaiSan
																		});


			var totalCount = bienBanThanhLyOutputQuery.Count();

			// sorting
			if (!string.IsNullOrWhiteSpace(input.Sorting))
			{
				bienBanThanhLyOutputQuery = bienBanThanhLyOutputQuery.OrderBy(input.Sorting);
			}

			// paging
			var items = bienBanThanhLyOutputQuery.PageBy(input).ToList();

			// result
			return new PagedResultDto<BienBanThanhLyForViewDto>(
				totalCount,
				items);
		}

		#endregion

		#region Private Method

		[AbpAuthorize(GWebsitePermissions.Pages_Administration_BienBanThanhLy_Create)]
		private void Create(BienBanThanhLyInput bienBanThanhLyInput)
		{
			//var donViCungCapTaiSanEntity = donViCungCapTaiSanRepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x => x.Id == bienBanThanhLyInput.donViCungCapTaiSanId);
			//if (donViCungCapTaiSanEntity == null)
			//	return;

			var bienBanThanhLyEntity = ObjectMapper.Map<BienBanThanhLy>(bienBanThanhLyInput);

			//bienBanThanhLyEntity.donViCungCapTaiSan = donViCungCapTaiSanEntity;

			SetAuditInsert(bienBanThanhLyEntity);
			bienBanThanhLyRepository.Insert(bienBanThanhLyEntity);
			CurrentUnitOfWork.SaveChanges();
		}

		[AbpAuthorize(GWebsitePermissions.Pages_Administration_BienBanThanhLy_Edit)]
		private void Update(BienBanThanhLyInput bienBanThanhLyInput)
		{
			var bienBanThanhLyEntity = bienBanThanhLyRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == bienBanThanhLyInput.Id);
			if (bienBanThanhLyEntity == null)
			{
			}
			ObjectMapper.Map(bienBanThanhLyInput, bienBanThanhLyEntity);
			SetAuditEdit(bienBanThanhLyEntity);
			bienBanThanhLyRepository.Update(bienBanThanhLyEntity);
			CurrentUnitOfWork.SaveChanges();
		}

		#endregion
	}
}
