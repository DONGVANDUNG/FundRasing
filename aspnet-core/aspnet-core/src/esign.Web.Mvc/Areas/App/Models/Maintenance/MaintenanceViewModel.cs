using System.Collections.Generic;
using esign.Caching.Dto;

namespace esign.Web.Areas.App.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
    }
}