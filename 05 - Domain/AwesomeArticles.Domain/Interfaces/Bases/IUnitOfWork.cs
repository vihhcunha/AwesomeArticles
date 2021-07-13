using System.Threading.Tasks;

namespace AwesomeArticles.Domain.Interfaces.Bases
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}