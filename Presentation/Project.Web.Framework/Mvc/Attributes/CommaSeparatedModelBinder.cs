using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Project.Web.Framework.Mvc.Attributes
{
    public class CommaSeparatedModelBinder : DefaultModelBinder
    {
        private readonly MethodInfo methodInfo = typeof(Enumerable).GetMethod("ToArray");
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return BindData(bindingContext.ModelType, bindingContext.ModelName, bindingContext);
        }
        private object BindData(Type type, string name, ModelBindingContext bindingContext)
        {
            if (type.GetInterface(typeof(IEnumerable).Name) != null)
            {
                var actualValue = bindingContext.ValueProvider.GetValue(name);
                if (actualValue != null)
                {
                    var valueType = type.GetElementType() ?? type.GetGenericArguments().FirstOrDefault();
                    if (valueType != null && valueType.GetInterface(typeof(IConvertible).Name) != null)
                    {
                        var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(valueType));
                        foreach (var item in actualValue.AttemptedValue.Split(','))
                        {
                            if (!string.IsNullOrWhiteSpace(item))
                                list.Add(Convert.ChangeType(item,valueType));
                        }
                        if (type.IsArray)
                            return methodInfo.MakeGenericMethod(valueType).Invoke(this, new[] { list });

                        return list;
                    }
                }
            }
            return null;
        }
    }
}
