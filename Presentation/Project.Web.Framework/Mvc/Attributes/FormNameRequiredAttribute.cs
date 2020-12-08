using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Project.Web.Framework.Mvc.Attributes
{
    public class FormNameRequiredAttribute : ActionMethodSelectorAttribute
    {
        #region Fields

        private readonly string[] _submitButtonNames;
        private readonly FormValueRequirement _formValueRequirement;

        #endregion

        #region Constructors

        public FormNameRequiredAttribute(params string[] submitButtonNames) : this(FormValueRequirement.Equal, submitButtonNames)
        {
        }

        public FormNameRequiredAttribute(FormValueRequirement formValueRequirement, params string[] submitButtonNames)
        {
            _formValueRequirement = formValueRequirement;
            _submitButtonNames = submitButtonNames;
        }

        #endregion

        #region Methods

        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            foreach (var buttonName in _submitButtonNames)
            {
                try
                {
                    switch (_formValueRequirement)
                    {
                        case FormValueRequirement.Equal:
                            {
                                if (controllerContext.HttpContext.Request.Form.AllKeys.Any(name => name.Equals(buttonName, StringComparison.InvariantCultureIgnoreCase)))
                                    return true;
                                break;
                            }
                        case FormValueRequirement.StartsWith:
                            {
                                if (controllerContext.HttpContext.Request.Form.AllKeys.Any(name => name.StartsWith(buttonName, StringComparison.InvariantCultureIgnoreCase)))
                                    return true;
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            }
            return false;
        }

        #endregion
    }

    public enum FormValueRequirement
    {
        Equal,
        StartsWith
    }
}
