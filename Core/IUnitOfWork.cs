using System.Threading.Tasks;

namespace pc_store.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}