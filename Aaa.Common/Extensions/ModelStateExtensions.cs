using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Aaa.Common
{
    public static class ModelStateExtensions
    {
        /// <summary>
        /// Reassigns the errors for the provided property name (key) to a generic property. The errors will still show up in 
        /// the ValidationSummary, but will not be linked to a specific field in the model.
        /// </summary>
        /// <param name="key">Name of the property in the model.</param>
        public static void MakeErrorsGenericForKey(this System.Web.Mvc.ModelStateDictionary modelState, string key)
        {
            if (modelState.ContainsKey(key))
            {
                foreach (var error in modelState[key].Errors)
                {
                    modelState.AddRuleErrors(new RulesException(string.Empty, error.ErrorMessage));
                }
                modelState.Remove(key);
            }
        }
    }
}
