using System.Threading.Tasks;

namespace KarolinkaUznani.Common.Database
{
    public interface IDatabaseInitializer
    {
        Task InitializeAsync();
    }
}