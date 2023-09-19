using System;
using System.ComponentModel.DataAnnotations;

namespace tower_api.Models.Requests
{
	public class ApiCardCreateRequest
	{

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name should be at most 50 characters")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Credit Card number is required")]
        [RegularExpression("^[0-9]{16}$", ErrorMessage = "Invalid Credit Card number")]
        public required string CreditCard { get; set; }

        [Required(ErrorMessage = "CVC is required")]
        [RegularExpression(@"^\d{3}$", ErrorMessage = "CVC should be a 3-digit number")]
        public required string CVC { get; set; }

        [Required(ErrorMessage = "Expiry Date is required")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{2}$", ErrorMessage = "Invalid Expiry Date (MM/YY)")]
        [CustomValidation(typeof(ApiCardCreateRequest), "ValidateExpiryDate")]
        public required string ExpiryDate { get; set; }

        // Custom validation logic for ExpiryDate
        public static ValidationResult ValidateExpiryDate(string expiryDate)
        {
            var currentDate = DateTime.Now;
            if (DateTime.TryParseExact(expiryDate, "MM/yy", null, System.Globalization.DateTimeStyles.None, out var parsedExpiryDate))
            {
                if (parsedExpiryDate < currentDate)
                {
                    return new ValidationResult("Expiry Date should be greater than or equal to the current month and year.");
                }
            }
            return ValidationResult.Success;
        }
    }
}

