using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebsiteBuilder.Core.Service;

namespace WebsiteBuilder.Core.Validation
{
    public static class DataAnnotationsValidator
    {
        public static OperationResult Validate(object obj)
        {
            var context = new ValidationContext(obj, serviceProvider: null, items: null);
            ICollection<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(obj, context, results, validateAllProperties: true);

            var operationResult = new OperationResult();
            foreach (var res in results)
            {
                operationResult.Errors.Add(res.ErrorMessage);
            }
            return operationResult;
        }
    }
}
