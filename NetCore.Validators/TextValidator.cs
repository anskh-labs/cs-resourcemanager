using System.Collections.Generic;

namespace NetCore.Validators
{
    public class TextValidator
    {
        private readonly Dictionary<string, string> _errorMessages = new Dictionary<string, string>();
        private static readonly TextValidator _instance = new TextValidator();
        private TextValidator()
        {
            _errorMessages.Add("Required", "{0} Can not be empty.");
        }
        public static TextValidator Instance => _instance;
        public bool Required(string? Text)
        {
            if (string.IsNullOrEmpty(Text) || string.IsNullOrWhiteSpace(Text) || Text == string.Empty || Text == " ")
            {
                return false;
            }
            else return true;
        }
        public string ErrorMessage(string ruleName, string propertyName)
        {
            return _errorMessages.ContainsKey(ruleName) ? string.Format(_errorMessages[ruleName], propertyName) : string.Empty;
        }
    }
}
