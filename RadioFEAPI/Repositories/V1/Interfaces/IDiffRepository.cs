namespace RadioFEAPI.Repositories.V1.Interfaces
{
    public interface IDiffRepository
    {
        Task<FeedbackObject> AddOrUpdate(ComparisonObject comparisonObject);
        Task<ComparisonObject> Get(int id);
    }
}
