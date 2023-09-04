namespace OnlineEvent.Abstract.UnitOfWorks
{
    public interface IUnitOfWork
    {
        // Bu katmanda SaveChanges() ve SaveChangesAsync() methotlarını kontrol ediyoruz.

        Task CommitAsync();
        void Commit();
        Task DisposeAsync();
    }
}
