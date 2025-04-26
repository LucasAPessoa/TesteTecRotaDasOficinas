namespace RO.DevTest.Application.Common.Results
{
    public class DeleteOperationResult
    {
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; } = new();
        public string Message { get; set; } = string.Empty;

        public static DeleteOperationResult Success()
        {
            return new DeleteOperationResult { 
                Succeeded = true,

            };

        }

        public static DeleteOperationResult Failure(params string[] errors)
        {
            return new DeleteOperationResult
            {
                Succeeded = false,
                Errors = errors.ToList(),
            };
        }
    }
}
