using Abp.Application.Services.Dto;
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

        [HttpPost]
        public async Task<ContractDto> CreateContract([FromBody]  ContractSaved ContractSaved)
        {
            return await this._ContractAppService.CreateContractAsync(ContractSaved);
        }

        [HttpPut]
        public async Task<ContractDto> updateContract([FromBody]  ContractSaved ContractSaved)
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
