namespace GWebsite.AbpZeroTemplate.Core.Models
{
    public class Customer : FullAuditModel
    {
		public long OrganizationId { get; set; }
		public string Name { get; set; }
        public string Address { get; set; }
        public string Info { get; set; }
    }
}