using AdvertisementApp.Common;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.ValidationExtensions
{
    public static class ValidationResultExtension
    {
        //Validationresult sınıfına extension yazdık.
        public static List<CustomValidationError> ConvertToCustomValidationError(this ValidationResult validation)
        {
            List<CustomValidationError> errors = new();

            foreach (var error in validation.Errors)
            {
                errors.Add(new()
                {
                    ErrorMessage=error.ErrorMessage,
                    PropertyName=error.PropertyName,
                });
            }

            return errors;
        }
    }
}
