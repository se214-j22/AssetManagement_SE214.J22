using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share;
using GWebsite.AbpZeroTemplate.Application.Share.Bidding;
using GWebsite.AbpZeroTemplate.Application.Share.Bidding.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;

namespace GWebsite.AbpZeroTemplate.Web.Core.Suppliers
{
    public class SupplierAppService : GWebsiteAppServiceBase, ISupplierAppService
    {
        private readonly IRepository<Supplier, int> _supplierRepository;
        private readonly IRepository<Bidding, int> _biddingRepository;
        private readonly IRepository<SupplierType, int> _supplierTypeRepository;

        public SupplierAppService(IRepository<Supplier, int> supplierRepository, IRepository<Bidding, int> biddingRepository, IRepository<SupplierType, int> supplierTypeRepository)
        {
            this._supplierRepository = supplierRepository;
            this._biddingRepository = biddingRepository;
            this._supplierTypeRepository = supplierTypeRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="biddingProduct"></param>
        /// <returns>SupplierDto</returns>
        public async Task<BiddingProduct> BiddingProductAsync(BiddingSaved biddingSaved)
        {
            var bidding = this.ObjectMapper.Map<Bidding>(biddingSaved);
            await this._biddingRepository.InsertAndGetIdAsync(bidding);
            //_context.Biddings.Add(bidding);
            //await _context.SaveChangesAsync();
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<BiddingProduct>(bidding);
        }

        /// <summary>
        /// when  change owner of  product 
        /// 1, change status of current product of supplier
        /// 2, change status new supplier of product and update  start date and end date of bidding
        /// </summary>
        /// <param name="biddingSaved"></param>
        /// <returns></returns>
        public async Task<BiddingProduct> ChangeOwnerBiddingProductAsync(BiddingSaved biddingSaved)
        {
            var current = await this._biddingRepository.GetAllIncluding(p => p.Supplier, p1 => p1.Product).FirstOrDefaultAsync(x => x.ProductId == biddingSaved.ProductId && x.Status == 1);
            if (current != null)
            {
                current.Status = 0;
                await this._biddingRepository.UpdateAsync(current);
            }
            var entity = await this._biddingRepository.GetAllIncluding(p => p.Supplier, p1 => p1.Product).FirstOrDefaultAsync(x => x.ProductId == biddingSaved.ProductId && x.SupplierId == biddingSaved.SupplierId);
            this.ObjectMapper.Map(biddingSaved, entity);
            entity = await this._biddingRepository.UpdateAsync(entity);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<BiddingProduct>(entity);
        }

        public async Task<SupplierDto> CreateSupplierAsync(SupplierSavedDto supplierSavedDto)
        {
            var supplier = this.ObjectMapper.Map<Supplier>(supplierSavedDto);
            await this._supplierRepository.InsertAndGetIdAsync(supplier);
            //_context.Biddings.Add(bidding);
            //await _context.SaveChangesAsync();
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<SupplierDto>(supplier);
        }

        public Task DeleteBiddingAsync(EntityDto<int> input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<SupplierDto>> GetAllBiddingPassAsync(GetMenuClientInput input)
        {
            var query = this._supplierRepository.GetAllIncluding().Include(p => p.Biddings).ThenInclude(p => p.Product);
            //query = query.Select(p => p.Biddings.Select(pc => pc.Status == 1));
            //&& p.Biddings.FirstOrDefault(b => b.Status == 1) != null;
            var select = query.Where(p => p.Biddings.Count > 0);
            var totalCount = await select.CountAsync();
            var items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<SupplierDto>(
             totalCount,
             items.Select(item => this.ObjectMapper.Map<SupplierDto>(item)).ToList());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<SupplierDto> GetSupplierByIdAsync(EntityDto<int> input)
        {
            var entity = await this._supplierRepository.GetAllIncluding(p => p.Biddings).FirstOrDefaultAsync(x => x.Id == input.Id);
            return this.ObjectMapper.Map<SupplierDto>(entity);
        }


        /// <summary>
        ///  status = 0 tham gia dau thau
        ///  status =1 trung thau
        ///  status =2 huy thau
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="productId"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<SupplierDto>> GetSupplierByProductAsync(GetMenuClientInput input, int productId)
        {
            var query = this._supplierRepository.GetAllIncluding().Include(p => p.Biddings).ThenInclude(p => p.Product).ThenInclude(p => p.Image);
            var select = query.Where(p => p.Biddings.FirstOrDefault(b => b.ProductId == productId) != null);
            var totalCount = await select.CountAsync();
            var items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<SupplierDto>(
             totalCount,
             items.Select(item => this.ObjectMapper.Map<SupplierDto>(item)).ToList());
        }

        public async Task<PagedResultDto<SupplierDto>> GetSupplierByTypeAsync(GetMenuClientInput input, string SupplierType)
        {
            var query = this._supplierRepository.GetAllIncluding().Include(p => p.SupplierType).Include(p => p.Biddings).ThenInclude(p => p.Product).ThenInclude(p => p.Image);
            var select = query.Where(sup => sup.SupplierType.Name.Equals(SupplierType));
            var totalCount = await select.CountAsync();
            var items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<SupplierDto>(
             totalCount,
             items.Select(item => this.ObjectMapper.Map<SupplierDto>(item)).ToList());
        }

        public async Task<IEnumerable<SupplierTypeDto>> GetSupplierTypesAsync()
        {
            var querry = this._supplierTypeRepository.GetAll();
            return querry.Select(item => this.ObjectMapper.Map<SupplierTypeDto>(item)).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierSavedDto"></param>
        /// <returns></returns>
        public async Task<SupplierDto> UpdateSupplierAsync(SupplierSavedDto supplierSavedDto)
        {
            var entity = await this._supplierRepository.GetAllIncluding(p => p.Biddings).FirstOrDefaultAsync(x => x.Id == supplierSavedDto.Id);
            this.ObjectMapper.Map(supplierSavedDto, entity);
            entity = await this._supplierRepository.UpdateAsync(entity);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<SupplierDto>(entity);
        }
    }
}
