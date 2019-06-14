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
using GWebsite.AbpZeroTemplate.Application.Share.BidProfile.Dto;

namespace GWebsite.AbpZeroTemplate.Web.Core.Contracts
{
    public class ContractAppService : GWebsiteAppServiceBase, IContractAppService
    {
        private readonly IRepository<Contract, int> _contractRepository;
        private readonly IRepository<BidUnit, int> bidUnitRepository;
        private readonly IRepository<BidProfile, int> bidProfileRepository;

        public ContractAppService(IRepository<Contract, int> contractRepository, IRepository<BidUnit, int> bidUnitRepository, IRepository<BidProfile, int> bidProfileRepository)
        {
            this._contractRepository = contractRepository;
            this.bidUnitRepository = bidUnitRepository;
            this.bidProfileRepository = bidProfileRepository;
        }

        public async Task<ContractDto> ApproveContract(EntityDto<int> input)
        {
            var entity = await this._contractRepository.GetAllIncluding(p => p.Guarantee, p => p.GaranteeContract, p => p.BidProfile, p => p.Supplier).FirstOrDefaultAsync(item => item.Id == input.Id);

            if (entity.Status != 3)
            {
                entity.Status = 1;
            }
            await this.CurrentUnitOfWork.SaveChangesAsync();

            return this.ObjectMapper.Map<ContractDto>(entity);
        }

        public async Task<ContractDto> CloseContract(EntityDto<int> input)
        {
            var entity = await this._contractRepository.GetAllIncluding(p => p.Guarantee, p => p.GaranteeContract, p => p.BidProfile, p => p.Supplier).FirstOrDefaultAsync(item => item.Id == input.Id);

            if (entity.Status == 2)
            {
                entity.Status = 3;
            }
            await this.CurrentUnitOfWork.SaveChangesAsync();

            return this.ObjectMapper.Map<ContractDto>(entity);
        }

        //public async Task<ContractDto> CreateContractAsync(ContractSavedDto contractSaved)
        //{
        //    var contract = this.ObjectMapper.Map<Contract>(contractSaved);
        //    await this._contractRepository.InsertAndGetIdAsync(contract);
        //    await this.CurrentUnitOfWork.SaveChangesAsync();
        //    return this.ObjectMapper.Map<ContractDto>(contract);
        //}

        public async Task<ContractDto> CreateContractAsync(ContractSavedDto contractSaved)
        {
            BidProfile bidProfile = await this.bidProfileRepository.GetAll().FirstOrDefaultAsync(bp => bp.Id.Equals(contractSaved.BidProfileId));

            Contract contract = this.ObjectMapper.Map<Contract>(contractSaved);

            contract.Status = 2;

            int id = await this._contractRepository.InsertAndGetIdAsync(contract);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            Contract newContract = await this._contractRepository.GetAllIncluding(c => c.BidProfile, c => c.Supplier).Where(c => c.Id.Equals(id)).FirstOrDefaultAsync();
            return this.ObjectMapper.Map<ContractDto>(newContract);
        }

        /// <summary>
        /// delete contract
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>

        public async Task DeleteContractAsync(EntityDto<int> input)
        {
            var query = await this._contractRepository.FirstOrDefaultAsync(item => item.Id == input.Id);
            await this._contractRepository.DeleteAsync(query);
        }

        public async Task<PagedResultDto<BidProfileDto>> GetApprovedBidProfile()
        {
            IQueryable<BidProfile> query = this.bidUnitRepository.GetAllIncluding(bu => bu.BidProfile)
                                                    .Where(bu => bu.Status == 1 && bu.BidProfile.Status == 1)
                                                    .Select(bu => bu.BidProfile)
                                                    //.ToListAsync()
                                                    ;

            int totalCount = await query.CountAsync();
            return new PagedResultDto<BidProfileDto>(
             totalCount,
             query.Select(item => this.ObjectMapper.Map<BidProfileDto>(item)).ToList());
        }

        /// <summary>
        /// get contract with correct id contract
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<ContractDto> GetContractByIdAsync(EntityDto<int> input)
        {
            var entity = await this._contractRepository.GetAllIncluding(p => p.Guarantee, p => p.GaranteeContract, p => p.BidProfile, p => p.Supplier).FirstOrDefaultAsync(item => item.Id == input.Id);

            return this.ObjectMapper.Map<ContractDto>(entity);
        }
        /// <summary>
        /// get  all contract
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<ContractDto>> GetContractsAsync(GetMenuClientInput input)
        {
            var query = this._contractRepository.GetAllIncluding(p => p.Guarantee, p => p.GaranteeContract, p => p.BidProfile, p => p.Supplier);
            var totalCount = await query.CountAsync();
            var items = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            return new PagedResultDto<ContractDto>(
             totalCount,
             items.Select(item => this.ObjectMapper.Map<ContractDto>(item)).ToList());
        }

        public async Task<ContractDto> updateContractAsync(ContractSavedDto contractSaved)
        {
            var entity = this._contractRepository.GetAllIncluding(p => p.Guarantee, p => p.GaranteeContract, p => p.BidProfile, p => p.Supplier).FirstOrDefault(item => item.Id == contractSaved.Id);
            this.ObjectMapper.Map(contractSaved, entity);
            entity = await this._contractRepository.UpdateAsync(entity);
            await this.CurrentUnitOfWork.SaveChangesAsync();
            return this.ObjectMapper.Map<ContractDto>(entity);
        }
    }
}
