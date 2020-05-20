using System.Threading;
using System.Threading.Tasks;

namespace AglTest.Infrastructure.RestClient
{
    public interface IRestClient
    {
        Task<TResp> GetAsync<TResp>(string url, CancellationToken cancellationToken = default);
        //Task<TResp> PostAsync<TReq, TResp>(string url, TReq payload, CancellationToken cancellationToken);
        //Task<TResp> PutAsync<TReq, TResp>(string url, TReq payload, CancellationToken cancellationToken);
        //Task<TResp> DeleteAsync<TReq, TResp>(string url, CancellationToken cancellationToken);
    }
}