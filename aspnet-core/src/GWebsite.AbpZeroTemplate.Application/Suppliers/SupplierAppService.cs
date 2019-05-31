using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share;
using GWebsite.AbpZeroTemplate.Application.Share.Bidding;
using GWebsite.AbpZeroTemplate.Application.Share.Bidding.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Web.Core.Suppliers
{
    public class SupplierAppService : GWebsiteAppServiceBase, ISupplierAppService
    {
        private readonly IRepository<Supplier, int> _supplierRepository;
        private readonly IRepository<Bidding, int> _biddingRepository;
        private readonly IRepository<SupplierType, int> _supplierTypeRepository;
        public SupplierAppService(IRepository<Supplier, int> supplierRepository, IRepository<Bidding, int> biddingRepository, IRepository<SupplierType, int> supplierTypeRepository)
        {
            this.supplierRepository = supplierRepository;
            this.biddingRepository = biddingRepository;
            this.supplierTypeRepository = supplierTypeRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="biddingProduct"></param>
        /// <returns>SupplierDto</returns>
        public async Task<BiddingProduct> BiddingProductAsync(BiddingSaved biddingSaved)
        {
            var bidding = this.ObjectMapper.Map<Bidding>(biddingSaved);
            await this.biddingRepository.InsertAndGetIdAsync(bidding);
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



            try
            {
                var current = await _biddingRepository.GetAllIncluding(p => p.Supplier, p1 => p1.Product).FirstOrDefaultAsync(x => x.ProductId == biddingSaved.ProductId && x.Status == 1);
                if (current != null)
                {
                    current.Status = 0;
                    await _biddingRepository.UpdateAsync(current);
                }

                var entity = await _biddingRepository.GetAllIncluding(p => p.Supplier, p1 => p1.Product).FirstOrDefaultAsync(x => x.ProductId == biddingSaved.ProductId && x.SupplierId == biddingSaved.SupplierId);
                ObjectMapper.Map(biddingSaved, entity);
                entity = await _biddingRepository.UpdateAsync(entity);
                await CurrentUnitOfWork.SaveChangesAsync();
                return ObjectMapper.Map<BiddingProduct>(entity);
            }
            catch
            {
                current.Status = 0;
                await this.biddingRepository.UpdateAsync(current);
            }
            var entity = await this.biddingRepository.GetAllIncluding(p => p.Supplier, p1 => p1.Product).FirstOrDefaultAsync(x => x.ProductId == biddingSaved.ProductId && x.SupplierId == biddingSaved.SupplierId);
            this.ObjectMapper.Map(biddingSaved, entity);
            entity = await this.biddingRepository.UpdateAsync(entity);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<BiddingProduct>(entity);
        }

        public async Task<SupplierDto> CreateSupplierAsync(SupplierSavedDto supplierSavedDto)
        {
            var supplier = this.ObjectMapper.Map<Supplier>(supplierSavedDto);
            await this.supplierRepository.InsertAndGetIdAsync(supplier);
            //_context.Biddings.Add(bidding);
            //await _context.SaveChangesAsync();
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<SupplierDto>(supplier);
        }

        public async Task DeleteBiddingAsync(EntityDto<int> input)
        {
            var query = await this._supplierRepository.FirstOrDefaultAsync(item => item.Id == input.Id);
            await this._supplierRepository.DeleteAsync(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<SupplierDto>> GetAllBiddingPassAsync(GetMenuClientInput input)
        {
            var query = this.supplierRepository.GetAllIncluding().Include(p => p.Biddings).ThenInclude(p => p.Product);
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
            var entity = await this.supplierRepository.GetAllIncluding(p => p.Biddings).FirstOrDefaultAsync(x => x.Id == input.Id);
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
            var query = this.supplierRepository.GetAllIncluding().Include(p => p.Biddings).ThenInclude(p => p.Product).ThenInclude(p => p.Image);
            var select = query.Where(p => p.Biddings.FirstOrDefault(b => b.ProductId == productId) != null);
            var totalCount = await select.CountAsync();
            var items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<SupplierDto>(
             totalCount,
             items.Select(item => this.ObjectMapper.Map<SupplierDto>(item)).ToList());
        }

        public async Task<PagedResultDto<SupplierDto>> GetSupplierByTypeAsync(GetMenuClientInput input, string SupplierType)
        {
            var query = this.supplierRepository.GetAllIncluding().Include(p => p.SupplierType).Include(p => p.Biddings).ThenInclude(p => p.Product).ThenInclude(p => p.Image);
            var select = query.Where(sup => sup.SupplierType.Name.Equals(SupplierType));
            var totalCount = await select.CountAsync();
            var items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<SupplierDto>(
             totalCount,
             items.Select(item => this.ObjectMapper.Map<SupplierDto>(item)).ToList());
        }
        /// <summary>
        /// get all category of supplier  with pagination
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<SupplierTypeDto>> GetSupplierTypesAsync(GetMenuClientInput input)
        {
            var query = this._supplierTypeRepository.GetAllIncluding(p => p.Suppliers);
            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<SupplierTypeDto>(
            totalCount,
            items.Select(item => {
                var data = this.ObjectMapper.Map<SupplierTypeDto>(item);
                data.IsInCludeSupplier = item.Suppliers.Count > 0;
                return data;
            }).ToList());
        }

        /// <summary>
        /// filter
        /// </summary>
        /// <param name="input"></param>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="intput"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<SupplierTypeDto>> GetSupplierTypesWithFilterAsync(GetMenuClientInput input, string code, string name, int status)
        {
            var query = _supplierTypeRepository.GetAllIncluding(p => p.Suppliers).Where(p=>p.Name.Contains(name)|| p.Code.Contains(code)|| p.Status.Equals(status));
            var totalCount = await query.CountAsync();
            if (totalCount == 0)
            {
                query = _supplierTypeRepository.GetAllIncluding(p => p.Suppliers);
            }
            var items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<SupplierTypeDto>(
            totalCount,
            items.Select(item => {
                var data = this.ObjectMapper.Map<SupplierTypeDto>(item);
                data.IsInCludeSupplier = item.Suppliers.Count > 0;
                return data;
            }).ToList());
        }
        /// <summary>
        /// toggle status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<SupplierTypeDto> ToggleStatusSupplierCatalogAsync(EntityDto<int> input)
        {
            var query = await _supplierTypeRepository.GetAllIncluding(p => p.Suppliers).FirstOrDefaultAsync(item=>item.Id== input.Id);
            query.Status = query.Status == 1 ? 2 : 1;
            query = await _supplierTypeRepository.UpdateAsync(query);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<SupplierTypeDto>(query);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierSavedDto"></param>
        /// <returns></returns>
        public async Task<SupplierDto> UpdateSupplierAsync(SupplierSavedDto supplierSavedDto)
        {
            var entity = await this.supplierRepository.GetAllIncluding(p => p.Biddings).FirstOrDefaultAsync(x => x.Id == supplierSavedDto.Id);
            this.ObjectMapper.Map(supplierSavedDto, entity);
            entity = await this.supplierRepository.UpdateAsync(entity);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<SupplierDto>(entity);
        }


        public async Task DeleteSupplierCatalogAsync(EntityDto<int> input)
        {
            var query = await this._supplierTypeRepository.FirstOrDefaultAsync(item => item.Id == input.Id);
            await this._supplierTypeRepository.DeleteAsync(query);
        }

        public async Task<SupplierTypeDto> UpdateSupplierCatalogAsync(SupplierTypeSavedDto supplierTypeSavedDto)
        {
            var entity = await this._supplierTypeRepository.GetAllIncluding(p => p.Suppliers).FirstOrDefaultAsync(item => item.Id == supplierTypeSavedDto.Id);
            this.ObjectMapper.Map(supplierTypeSavedDto, entity);
            entity = await this._supplierTypeRepository.UpdateAsync(entity);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<SupplierTypeDto>(entity);
        }


        public async Task<SupplierTypeDto> CreateSupplierCatalogAsync(SupplierTypeSavedDto supplierTypeSavedDto)
        {
            var supplier = ObjectMapper.Map<SupplierType>(supplierTypeSavedDto);
            await _supplierTypeRepository.InsertAndGetIdAsync(supplier);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<SupplierTypeDto>(supplier);
        }


    }
}
