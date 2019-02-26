using System.Threading.Tasks;

namespace KarolinkaUznani.Common.Database
{
    public interface IDatabaseSeeder
    {
        Task SeedAsync();
    }
}