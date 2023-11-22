using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esign.FundRaising
{
    [Table("FundDetailContent")]
    public class FundDetailContent : FullAuditedEntity<long>
    {
        public long FundId { get; set; }
        public string Header { get; set; }
        public string ReasonCreatedFund { get; set; }
        public string Content { get; set; }
    }
}
