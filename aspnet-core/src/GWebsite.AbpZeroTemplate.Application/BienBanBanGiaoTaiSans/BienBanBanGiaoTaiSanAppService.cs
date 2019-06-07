using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper.QueryableExtensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.BienBanBanGiaoTaiSans;
using GWebsite.AbpZeroTemplate.Application.Share.BienBanBanGiaoTaiSans.Dto;
using GWebsite.AbpZeroTemplate.Core.Authorization;
using GWebsite.AbpZeroTemplate.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.BienBanBanGiaoTaiSans
{
	[AbpAuthorize(GWebsitePermissions.Pages_Administration_BienBanBanGiaoTaiSan)]
	public class BienBanBanGiaoTaiSanAppService : GWebsiteAppServiceBase, IBienBanBanGiaoTaiSanAppService
	{
		private readonly IRepository<BienBanBanGiaoTaiSan> bienBanBanGiaoTaiSanRepository;
		private readonly IRepository<TaiSanCoDinh> taiSanCoDinhRepository;
		private readonly IRepository<PhongBan> phongBanRepository;

		public BienBanBanGiaoTaiSanAppService(IRepository<BienBanBanGiaoTaiSan> bienBanBanGiaoTaiSanRepository, IRepository<TaiSanCoDinh> taiSanCoDinhRepository, IRepository<PhongBan> phongBanRepository)
		{
			this.bienBanBanGiaoTaiSanRepository = bienBanBanGiaoTaiSanRepository;
			this.taiSanCoDinhRepository = taiSanCoDinhRepository;
			this.phongBanRepository = phongBanRepository;
		}

		#region Public Method

		public void CreateOrEditBienBanBanGiaoTaiSan(BienBanBanGiaoTaiSanInput bienBanBanGiaoTaiSanInput)
		{
			if (bienBanBanGiaoTaiSanInput.Id == 0)
			{
				Create(bienBanBanGiaoTaiSanInput);
			}
			else
			{
				Update(bienBanBanGiaoTaiSanInput);
			}
		}

		public void DeleteBienBanBanGiaoTaiSan(int id)
		{
			var bienBanBanGiaoTaiSanEntity = bienBanBanGiaoTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
			if (bienBanBanGiaoTaiSanEntity != null)
			{
				bienBanBanGiaoTaiSanEntity.IsDelete = true;
				bienBanBanGiaoTaiSanRepository.Update(bienBanBanGiaoTaiSanEntity);
				CurrentUnitOfWork.SaveChanges();
			}
		}

		public BienBanBanGiaoTaiSanInput GetBienBanBanGiaoTaiSanForEdit(int id)
		{
			var bienBanBanGiaoTaiSanEntity = bienBanBanGiaoTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
			if (bienBanBanGiaoTaiSanEntity == null)
			{
				return null;
			}
			return ObjectMapper.Map<BienBanBanGiaoTaiSanInput>(bienBanBanGiaoTaiSanEntity);
		}

		public BienBanBanGiaoTaiSanDto GetBienBanBanGiaoTaiSanForView(int id)
		{
			var bienBanBanGiaoTaiSanEntity = bienBanBanGiaoTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == id);
			if (bienBanBanGiaoTaiSanEntity == null)
			{
				return null;
			}
			return ObjectMapper.Map<BienBanBanGiaoTaiSanDto>(bienBanBanGiaoTaiSanEntity);
		}

		public PagedResultDto<BienBanBanGiaoTaiSanForViewDto> GetBienBanBanGiaoTaiSans(BienBanBanGiaoTaiSanFilter input)
		{
			IQueryable<BienBanBanGiaoTaiSan> query = bienBanBanGiaoTaiSanRepository.GetAll().Where(x => !x.IsDelete);

			// filter by value
			//if (input.TenDonViThanhLy != null)
			//{
			//	query = query.Where(x => x.TenDonViThanhLy.ToLower().Contains(input.TenDonViThanhLy.ToLower()));
			//}
			// IQueryable
			IQueryable<BienBanBanGiaoTaiSanDto> bienBanBanGiaoTaiSanQuery = query.ProjectTo<BienBanBanGiaoTaiSanDto>();

			IQueryable<TaiSanCoDinh> taiSanCoDinhQuery = taiSanCoDinhRepository.GetAll().Where(x => x.IsDelete == false);
			IQueryable<PhongBan> phongBanQuery = phongBanRepository.GetAll().Where(x => x.IsDelete == false);

			IQueryable<BienBanBanGiaoTaiSanForViewDto> bienBanBanGiaoTaiSanOutputQuery = (from bbbg in bienBanBanGiaoTaiSanQuery
																			  join tscd in taiSanCoDinhQuery on bbbg.TaiSanCoDinhId equals tscd.Id
																			  join pb in phongBanQuery on bbbg.PhongBanId equals pb.Id
																			  select new BienBanBanGiaoTaiSanForViewDto()
																			  {
																				  BienBanBanGiaoTaiSan = bbbg,
																				  TenTaiSanCoDinh = tscd.TenTaiSan,
																				  TenPhongBan = pb.TenPhong
																			  });


			var totalCount = bienBanBanGiaoTaiSanOutputQuery.Count();

			// sorting
			if (!string.IsNullOrWhiteSpace(input.Sorting))
			{
				bienBanBanGiaoTaiSanOutputQuery = bienBanBanGiaoTaiSanOutputQuery.OrderBy(input.Sorting);
			}

			// paging
			var items = bienBanBanGiaoTaiSanOutputQuery.PageBy(input).ToList();

			// result
			return new PagedResultDto<BienBanBanGiaoTaiSanForViewDto>(
				totalCount,
				items);
		}

		#endregion

		#region Private Method

		[AbpAuthorize(GWebsitePermissions.Pages_Administration_BienBanBanGiaoTaiSan_Create)]
		private void Create(BienBanBanGiaoTaiSanInput bienBanBanGiaoTaiSanInput)
		{
			//var donViCungCapTaiSanEntity = donViCungCapTaiSanRepository.GetAll().Where(x => !x.IsDelete).FirstOrDefault(x => x.Id == bienBanBanGiaoTaiSanInput.donViCungCapTaiSanId);
			//if (donViCungCapTaiSanEntity == null)
			//	return;

			var bienBanBanGiaoTaiSanEntity = ObjectMapper.Map<BienBanBanGiaoTaiSan>(bienBanBanGiaoTaiSanInput);

			//bienBanBanGiaoTaiSanEntity.donViCungCapTaiSan = donViCungCapTaiSanEntity;

			SetAuditInsert(bienBanBanGiaoTaiSanEntity);
			bienBanBanGiaoTaiSanRepository.Insert(bienBanBanGiaoTaiSanEntity);
			CurrentUnitOfWork.SaveChanges();
		}

		[AbpAuthorize(GWebsitePermissions.Pages_Administration_BienBanBanGiaoTaiSan_Edit)]
		private void Update(BienBanBanGiaoTaiSanInput bienBanBanGiaoTaiSanInput)
		{
			var bienBanBanGiaoTaiSanEntity = bienBanBanGiaoTaiSanRepository.GetAll().Where(x => !x.IsDelete).SingleOrDefault(x => x.Id == bienBanBanGiaoTaiSanInput.Id);
			if (bienBanBanGiaoTaiSanEntity == null)
			{
			}
			ObjectMapper.Map(bienBanBanGiaoTaiSanInput, bienBanBanGiaoTaiSanEntity);
			SetAuditEdit(bienBanBanGiaoTaiSanEntity);
			bienBanBanGiaoTaiSanRepository.Update(bienBanBanGiaoTaiSanEntity);
			CurrentUnitOfWork.SaveChanges();
		}

		#endregion
	}
}
