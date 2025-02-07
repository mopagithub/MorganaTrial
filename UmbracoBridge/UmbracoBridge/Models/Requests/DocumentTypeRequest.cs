using System.ComponentModel.DataAnnotations;
using UmbracoBridge.Models.Validation;

namespace UmbracoBridge.Models.Requests
{
    public class DocumentTypeRequest : IValidatableObject
    {
        [Required(ErrorMessage = "Alias must have a value")]
        public required string Alias { get; set; }
        [Required(ErrorMessage = "Name must have a value")]
        public required string Name { get; set; }
        [Required(ErrorMessage = "Description must have a value")]
        public required string Description { get; set; }

        [PrefixValidation("icon-")]
        public string? Icon {  get; set; }
        public bool? AllowedAsRoot { get; set; }
        public bool? VariesByCulture { get; set; }
        public bool? VariesBySegment { get; set; }
        public object? Collection {  get; set; }
        public bool? IsElement { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();
            if(string.IsNullOrEmpty(Alias))
            {
                results.Add(new ValidationResult("Alias cannot be empty", new[] {nameof(Alias) }));
            }

            if (string.IsNullOrEmpty(Name))
            {
                results.Add(new ValidationResult("Name cannot be empty", new[] { nameof(Name) }));
            }

            if (string.IsNullOrEmpty(Description))
            {
                results.Add(new ValidationResult("Description cannot be empty", new[] { nameof(Description) }));
            }

            return results;
        }
    }
}
