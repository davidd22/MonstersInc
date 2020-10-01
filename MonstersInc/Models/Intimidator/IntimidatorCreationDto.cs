using Microsoft.Extensions.DependencyInjection;
using MonstersIncLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MonstersInc
{
    public class IntimidatorCreationDto : IValidatableObject
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(20)]
        public string PhoneNumber { get; set; }
        [Required]

        public int TentaclesNumber { get; set; }
        [Required]
        [MaxLength(20)]
        public string ClientSecret { get; set; }

        public  IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ClientSecret.Length < 8)
            {
                yield return new ValidationResult(nameof(ClientSecret) + " should be at least 8 characters long");
            }
            else if (!ClientSecret.Any(char.IsUpper))
            {
                yield return new ValidationResult(nameof(ClientSecret) + " should include One capital letter");
            }
            else if (!ClientSecret.Any(char.IsDigit))
            {
                yield return new ValidationResult(nameof(ClientSecret) + " should include One at least one digit");
            }
            else if (!ClientSecret.Any(char.IsLetterOrDigit))
            {
                yield return new ValidationResult(nameof(ClientSecret) + " should include One non-alphanumeric letter");
            }
        }
    }
}
