using System.ComponentModel.DataAnnotations;

namespace UmbracoBridge.Models.Validation
{
    public class PrefixValidationAttribute : ValidationAttribute
    {
        private readonly string _prefix;

        public PrefixValidationAttribute(string prefix)
        {
            _prefix = prefix;   
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value != null && value is string strValue && strValue.StartsWith(_prefix))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult($"The field must begin with the prefix '{_prefix}'.");

        }
    }
}
