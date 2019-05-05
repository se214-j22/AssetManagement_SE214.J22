using GWebsite.AbpZeroTemplate.Core.Models;
using System;

namespace GWebsite.AbpZeroTemplate.Application.Share.DemoModels.Dto
{
    /// <summary>
    /// <model cref="DemoModel"></model>
    /// </summary>
    public class DemoModelDto
    {
        public int? Value { get; set; }
        public string Info { get; set; }
        public DateTime? Date { get; set; }
    }
}
