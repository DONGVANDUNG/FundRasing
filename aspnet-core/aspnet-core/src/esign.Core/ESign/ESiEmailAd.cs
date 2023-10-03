using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace esign.ESign
{
    [Table("ESiEmailAd")]
    public class ESiEmailAd: FullAuditedEntity<long>, IEntity<long>
    {   
        public long SNo { get; set; }
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
        [StringLength(50)]
        public string EmployeeId { get; set; }
        [StringLength(50)]
        public string DisplayName { get; set; }
        [StringLength(50)]
        public string EmailAddress { get; set; }
        [StringLength(50)]
        public string EmailAlias { get; set; }
        [StringLength(50)]
        public string SamAccountName { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(50)]
        public string Division { get; set; }
        [StringLength(50)]
        public string Department { get; set; }
        [StringLength(50)]
        public string Manager { get; set; }
        [StringLength(50)]
        public string Office { get; set; }
        [StringLength(50)]
        public string Company { get; set; }
        [StringLength(150)]
        public string Notes { get; set; }
        [StringLength(50)]
        public string AccountExpiryTime { get; set; }
        [StringLength(50)]
        public string AccountStatus { get; set; }
        [StringLength(50)]
        public string City { get; set; }
        [StringLength(50)]
        public string CommonName { get; set; }
        [StringLength(50)]
        public string EmployeeNumber { get; set; }
        [StringLength(50)]
        public string FullName { get; set; }
        [StringLength(50)]
        public string LogonName { get; set; }
    }
}
