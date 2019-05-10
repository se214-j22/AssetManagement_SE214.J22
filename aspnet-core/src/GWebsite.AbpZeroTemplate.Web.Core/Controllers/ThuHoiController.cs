using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using GWebsite.AbpZeroTemplate.Application.Share.ThuHois;
using GWebsite.AbpZeroTemplate.Application.Share.ThuHois.Dto;
using Microsoft.AspNetCore.Mvc;

namespace GWebsite.AbpZeroTemplate.Application.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ThuHoiController : GWebsiteControllerBase
    {
        private readonly IThuHoiAppService thuHoiAppservice;

        public ThuHoiController(IThuHoiAppService thuHoiAppservice)
        {
            this.thuHoiAppservice = thuHoiAppservice;
        }
        [HttpGet]
        public string GetTest()
        {
            return "Test";
        }

        [HttpGet]
        public PagedResultDto<ThuHoiDto> GetThuHoisByFilter(ThuHoiFilter ThuHoiFilter)
        {
            return thuHoiAppservice.GetThuHois(ThuHoiFilter);
        }
        [HttpPost]
        public void CreateOrEditThuHoi([FromBody] ThuHoiInput input)
        {
            thuHoiAppservice.CreateOrEditThuHoi(input);
        }

        [HttpGet]
        public ThuHoiInput GetThuHoiForEdit(int id)
        {
            return thuHoiAppservice.GetThuHoiForEdit(id);
        }
        [HttpDelete("{id}")]
        public void DeleteThuHoi(int id)
        {
            thuHoiAppservice.DeleteThuHoi(id);
        }
        [HttpGet]
        public ThuHoiForViewDto GetThuHoiForView(int id)
        {
            return thuHoiAppservice.GetThuHoiForView(id);
        }
    }
}

