using System.Collections.Generic;
using Abp.Application.Services.Dto;
using esign.Editions.Dto;

namespace esign.Web.Areas.App.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}