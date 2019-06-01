using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using GWebsite.AbpZeroTemplate.Application;
using GWebsite.AbpZeroTemplate.Application.Share.Contract;
using GWebsite.AbpZeroTemplate.Application.Share.Contract.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using GWebsite.AbpZeroTemplate.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using System.Linq.Dynamic.Core;

namespace GWebsite.AbpZeroTemplate.Web.Core.Contracts
{
    public class ContractAppService : GWebsiteAppServiceBase, IContractAppService
    {
        private readonly IRepository<Contract, int> _contractRepository;
        public ContractAppService(IRepository<Contract, int> contractRepository)
        {
            this._contractRepository = contractRepository;
        }

        public async Task<ContractDto> CreateContractAsync(ContractSaved contractSaved)
        {
            var contract = ObjectMapper.Map<Contract>(contractSaved);
            await _contractRepository.InsertAndGetIdAsync(contract);
            await CurrentUnitOfWork.SaveChangesAsync();
            return ObjectMapper.Map<ContractDto>(contract);
        }
        /// <summary>
        /// delete contract
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task DeleteContractAsync(EntityDto<int> input)
        {
            var query = await this._contractRepository.FirstOrDefaultAsync(item=>item.Id==input.Id);
            await this._contractRepository.DeleteAsync(query);
        }
        
        /// <summary>
        /// get contract with correct id contract
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ContractDto> GetContractByIdAsync(EntityDto<int> input)
        {
            var entity = await this._contractRepository.GetAllIncluding().Include(p => p.Guarantee).Include(p => p.GaranteeContract).Include(p => p.Bidding).FirstOrDefaultAsync(item=>item.Id==input.Id);

            return this.ObjectMapper.Map<ContractDto>(entity);
        }
        /// <summary>
        /// get  all contract
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ContractDto>> GetContractsAsync(GetMenuClientInput input)
        {
            var query = this._contractRepository.GetAllIncluding().Include(p => p.Guarantee).Include(p=>p.GaranteeContract).Include(p=>p.Bidding);
            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<ContractDto>(
             totalCount,
             items.Select(item => this.ObjectMapper.Map<ContractDto>(item)).ToList());
        }

        public async Task<ContractDto> updateContractAsync(ContractSaved contractSaved)
        {
            var entity = this._contractRepository.GetAllIncluding().Include(p => p.Guarantee).Include(p => p.GaranteeContract).Include(p => p.Bidding).FirstOrDefault(item=>item.Id==contractSaved.Id);
            this.ObjectMapper.Map(contractSaved, entity);
            entity = await this._contractRepository.UpdateAsync(entity);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<ContractDto>(entity);
        }
    }
}
