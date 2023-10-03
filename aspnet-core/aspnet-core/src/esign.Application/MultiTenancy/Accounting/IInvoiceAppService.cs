using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using esign.MultiTenancy.Accounting.Dto;

namespace esign.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
