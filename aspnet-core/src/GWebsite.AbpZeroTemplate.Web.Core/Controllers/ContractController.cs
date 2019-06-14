using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.BidProfile.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.Contract;
using GWebsite.AbpZeroTemplate.Application.Share.Contract.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.MenuClients.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ContractController : GWebsiteControllerBase
    {
        private readonly IContractAppService _ContractAppService;

        public ContractController(IContractAppService ContractService)
        {
            this._ContractAppService = ContractService;
        }

        [HttpPost]
        public async Task<ContractDto> CreateContract(ContractSavedDto dto)
        {
            return await this._ContractAppService.CreateContractAsync(dto);
        }

        [HttpGet]
        public async Task<PagedResultDto<ContractDto>> GetContracts(GetMenuClientInput input)
        {
            return await this._ContractAppService.GetContractsAsync(input);
        }

        [HttpGet]
        public async Task<ContractDto> GetContractById(EntityDto<int> input)
        {
            return await this._ContractAppService.GetContractByIdAsync(input);
        }

        [HttpGet("approve")]
        public async Task<ContractDto> ApproveContract(EntityDto<int> input)
        {
            return await this._ContractAppService.ApproveContract(input);
        }

        [HttpGet("close")]
        public async Task<ContractDto> CloseContract(EntityDto<int> input)
        {
            return await this._ContractAppService.CloseContract(input);
        }

        [HttpGet("approvedBidProfile")]
        public async Task<PagedResultDto<BidProfileDto>> GetApprovedBidProfile()
        {
            return await this._ContractAppService.GetApprovedBidProfile();
        }

        [HttpPut]
        public async Task<ContractDto> updateContract([FromBody]  ContractSavedDto ContractSaved)
        {
            return await this._ContractAppService.updateContractAsync(ContractSaved);
        }

        [HttpDelete]
        public async Task deleteContract(EntityDto<int> input)
        {
             await this._ContractAppService.DeleteContractAsync(input);
        }
    }
}
