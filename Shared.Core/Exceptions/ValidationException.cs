using Shared.I18n.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Core.Exceptions
{
    /// <summary>
    /// Validation exceptions which is raised if the CRUD operation is not allowed. 
    /// It should contains the information why the operation is prohibited.
    /// </summary>
    public class ValidationException : Exception
    {
        private IList<ValidationResult> validationResults = new List<ValidationResult>();

        public ValidationException(string message, params object[] parameters)
        {
            Add(message, parameters);
        }

        /// <summary>
        /// Adds the message to the validation results.
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="parameters">The parameters to the message</param>
        public void Add(string message, params object[] parameters)
        {
            validationResults.Add(new ValidationResult(message, parameters));
        }

        /// <summary>
        /// Gets validation results as the string.
        /// </summary>
        /// <returns>The validation results as the string</returns>
        public string GetValidationResults()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ValidationResult validationResult in validationResults)
            {
                sb.AppendLine(ResourceUtils.GetString(validationResult.Message, validationResult.Parameters));
            }
            return sb.ToString();
        }
    }
}
