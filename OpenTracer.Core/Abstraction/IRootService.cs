namespace OpenTracerPackage.Core.Abstraction
{
    public interface IRootService<T> : IRepository<T> where T : class,IEntityRoot
    {
    }
}
