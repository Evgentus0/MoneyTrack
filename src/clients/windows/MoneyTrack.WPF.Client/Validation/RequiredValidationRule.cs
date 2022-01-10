using System.Globalization;
using System.Windows.Controls;

namespace MoneyTrack.WPF.Client.Validation
{
    public class RequiredValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            const string message = "It is required filed";

            if (value is null)
                return new ValidationResult(false, message);
            if (string.IsNullOrEmpty(value.ToString()))
                return new ValidationResult(false, message);

            return ValidationResult.ValidResult;
        }
    }
}
