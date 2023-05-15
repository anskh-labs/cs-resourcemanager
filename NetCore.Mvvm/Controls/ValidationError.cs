namespace NetCore.Mvvm.Controls
{
    public class ValidationError
    {
        public ValidationError(string message, ErrorLevel errorLevel)
        {
            Message = message;
            Level = errorLevel;
        }

        public string Message { get; private set; }
        public ErrorLevel Level { get; private set; }
    }
}
