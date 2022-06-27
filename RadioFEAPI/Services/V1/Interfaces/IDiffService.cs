using RadioFEAPI.Models.V1;

namespace RadioFEAPI.Services.V1.Interfaces
{
    public interface IDiffService
    {
        public Task<FeedbackObject> Set(ComparisonInsertion comp);
        public Task<ComparisonResult> Get(int id);
    }
}
