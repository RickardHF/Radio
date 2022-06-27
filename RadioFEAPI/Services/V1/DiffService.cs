namespace RadioFEAPI.Services.V1
{
    public class DiffService : IDiffService
    {
        private readonly IDiffRepository _diffRepository;

        public DiffService(IDiffRepository diffRepository)
        {
            _diffRepository = diffRepository;
        }

        public async Task<ComparisonResult> Get(int id)
        {
            // Fetch selected object to compare
            var diff = await _diffRepository.Get(id);

            // Check if we have a valid object
            if(diff == null) throw new ArgumentException("Selected object is not found");

            // Check if left and right are same length. Return correct message if they are not
            if (! diff.IsSameLength()) return new ComparisonResult { Message = "inputs are of different size" };

            // Check if left and right are equal. Return correct message if so
            if (diff.IsEqual()) return new ComparisonResult { Message = "inputs were equal" };

            // Returns result of comparison
            return new ComparisonResult { Message = "inputs vary", DifferencesLeft = diff.GetLeftDifference(), DifferencesRight = diff.GetRightDifference() };
        }

        public async Task<FeedbackObject> Set(ComparisonInsertion comp)
        {
            // Try to retrieve existing object
            var currentObject = await _diffRepository.Get(comp.Id);
            if(currentObject == null)
            {
                // Inserts current data if nothing exists for current Id. Checks if we want to set the right or left side of the object.
                return await _diffRepository.AddOrUpdate(comp.Side == SideToSet.Right ? new ComparisonObject { Id = comp.Id, Right = comp.Input} : new ComparisonObject { Id = comp.Id, Left = comp.Input });
            }

            // Checks which side to update
            if(comp.Side == SideToSet.Left)
            {
                // If incoming value is left, update left side
                currentObject = currentObject with { Left = comp.Input };
            } else
            {
                // If incoming value is right, update right side
                currentObject = currentObject with { Right = comp.Input };
            }

            // Inserts updated object
            return await _diffRepository.AddOrUpdate(currentObject);
        }
    }
}
