namespace TechLanches.Core
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}