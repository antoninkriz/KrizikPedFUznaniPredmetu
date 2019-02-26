using System.Threading.Tasks;
using RawRabbit.Common;

namespace KarolinkaUznani.Common.Commands
{
    public interface ICommandHandler<in T>
    {
        Task<Acknowledgement> HandleAsync<TCommand>(TCommand arg) where TCommand : ICommand;
        
    }
}