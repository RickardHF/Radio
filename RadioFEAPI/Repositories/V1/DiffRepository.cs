namespace RadioFEAPI.Repositories.V1
{
    public class DiffRepository : IDiffRepository
    {
        private readonly IDictionary<int, ComparisonObject> _comparisons;
        public DiffRepository()
        {
            _comparisons = new Dictionary<int, ComparisonObject>();
        }
        public Task<FeedbackObject> AddOrUpdate(ComparisonObject comparisonObject)
        {
            try
            {
                // Attempts to add or update the object
                _comparisons[comparisonObject.Id] = comparisonObject;
                //Return success statement
                return Task.FromResult(new FeedbackObject { Success = true, Message = "Successfully added item" });
            }catch (Exception ex)
            {
                // Return failed statement
                return Task.FromResult(new FeedbackObject() { Success = false, Message = ex.Message });
            }
        }

        public Task<ComparisonObject> Get(int id)
        {
            // Return the selected object, or null if it doesn't exist
            return Task.FromResult(_comparisons.TryGetValue(id, out var comparisonObject) ? comparisonObject : null);
        }
    }
}
