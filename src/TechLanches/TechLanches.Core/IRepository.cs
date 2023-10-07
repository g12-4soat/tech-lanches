using TechLanches.Core;

namespace TechLanches.Domain
{
    public interface IRepository<T>  where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}