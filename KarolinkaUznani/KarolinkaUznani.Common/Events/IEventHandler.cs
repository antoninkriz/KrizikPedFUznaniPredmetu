using System.Threading.Tasks;
using RawRabbit.Common;

namespace KarolinkaUznani.Common.Events
{
    public interface IEventHandler<in T> where T : IEvent
    {
        Task<Acknowledgement> HandleAsync<TEvent>(TEvent arg) where TEvent : IEvent;
    }
}