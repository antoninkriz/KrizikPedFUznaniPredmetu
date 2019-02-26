using System.Threading.Tasks;

namespace KarolinkaUznani.Common.Requests
{
    public interface IRequestHandler<in TRequest, TResponse>
    {
        Task<TResponse> HandleAsync(TRequest request);
    }
}