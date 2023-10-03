using System.Threading.Tasks;
using esign.Sessions.Dto;

namespace esign.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
