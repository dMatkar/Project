using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.WebPages;
using System.Linq;
using Project.Web.Framework.Models;
using System.Text;

namespace Project.Web.Framework
{
    public static class HtmlExtentions
    {
        public static string FieldNameFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldName(ExpressionHelper.GetExpressionText(expression));
        }
        public static string FieldIdFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            string result = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(ExpressionHelper.GetExpressionText(expression));
            return result.Replace('[', '_').Replace(']', '_');
        }
        public static MvcHtmlString ProjectTextBoxFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            IDictionary<string, object> attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            AddFormControlClass(attributes);
            return htmlHelper.TextBoxFor(expression, attributes);
        }
        public static MvcHtmlString ProjectTextAreaFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            IDictionary<string, object> attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            AddFormControlClass(attributes);
            return htmlHelper.TextAreaFor(expression, attributes);
        }

        public static MvcHtmlString DeleteConfiration<TModel>(this HtmlHelper<TModel> htmlHelper,string buttonSelector) where TModel:BaseEntityModel
        {
            return DeleteConfiration<TModel>(htmlHelper,string.Empty, buttonSelector);
        }
        public static MvcHtmlString DeleteConfiration<TModel>(this HtmlHelper<TModel> htmlHelper,string actionName,string buttonSelector) where TModel : BaseEntityModel
        {
            if (htmlHelper is null)
                return null;

            if (string.IsNullOrEmpty(actionName))
                actionName = "Delete";

            string modelId = htmlHelper.ViewData.ModelMetadata.ModelType.Name + "-delete-confiramation";
            DeleteConfirmationModel deleteConfirmationModel = new DeleteConfirmationModel()
            {
                ActionName=actionName,
                ControllerName=htmlHelper.ViewContext.RouteData.GetRequiredString("controller"),
                Id=htmlHelper.ViewData.Model.Id,
                WindowId= modelId
            };

            StringBuilder window = new StringBuilder();
            window.Append(htmlHelper.Partial("Delete", deleteConfirmationModel).ToHtmlString());

            window.AppendLine("<script>");
            window.AppendLine("$(document).ready(function() {");
            window.AppendFormat("$('#{0}').attr(\"data-toggle\", \"modal\").attr(\"data-target\", \"#{1}\")", buttonSelector, modelId);
            window.AppendLine("});");
            window.AppendLine("</script>");

            return MvcHtmlString.Create(window.ToString());
        }

        public static MvcHtmlString ProjectCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, bool>> expression, object htmlAttributes = null)
        {
            if (expression is null)
                throw new ArgumentNullException("expression");

            IDictionary<string, object> attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            if (attributes.ContainsKey("class"))
                attributes["class"] = $"{attributes["class"]} checkbox";
            attributes.Add("class", "checkbox");

            TagBuilder tagBuilder = new TagBuilder("div")
            {
                InnerHtml = $"<label style=\"margin-bottom:0\">{htmlHelper.CheckBoxFor(expression, null)}</label>"
            };
            foreach (var attribute in attributes.ToDictionary(attributeKey => attributeKey.Key, attributeValue => attributeValue.Value.ToString()))
                tagBuilder.Attributes.Add(attribute);
            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString ProjectDisplayFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression)
        {
            var result = new TagBuilder("div");
            result.Attributes.Add("class", "form-text-row");
            result.InnerHtml = helper.DisplayFor(expression).ToString();

            return MvcHtmlString.Create(result.ToString());
        }
        public static MvcHtmlString ProjectDropDownListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<SelectListItem> selectList, string optionLabel=null, object htmlAttributes = null)
        {
            if (expression == null)
                throw new ArgumentNullException("expression");

            IDictionary<string, object> attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            AddFormControlClass(attributes);
            return htmlHelper.DropDownListFor(expression, selectList, optionLabel, attributes);
        }
        public static MvcHtmlString RenderBootstrapBoxContent(this HtmlHelper htmlHelper, HelperResult helperResult, object htmlAttributes = null)
        {
            if (htmlHelper is null)
                throw new ArgumentNullException("htmlHelper");

            TagBuilder tagBuilder = new TagBuilder("div")
            {
                InnerHtml = helperResult.ToHtmlString(),
                Attributes =
                {
                    new KeyValuePair<string, string>("class","box-body")
                }
            };

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes).ToDictionary(attributeKey => attributeKey.Key, attributeValue => attributeValue.Value.ToString());
                foreach (KeyValuePair<string, string> pair in attributes)
                {
                    if (tagBuilder.Attributes.ContainsKey(pair.Key))
                    {
                        tagBuilder.Attributes.TryGetValue(pair.Key, out string attributeValue);
                        tagBuilder.Attributes[pair.Key] = $"{attributeValue} {pair.Value}";
                    }
                    else
                    {
                        tagBuilder.Attributes.Add(pair);
                    }
                }
            }

            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.Normal));
        }
        public static MvcHtmlString RenderBootstrapBoxHeader(this HtmlHelper htmlHelper, string header, string iconClass, bool useCollapseBtnTools = true, bool useRemoveBtnTool = false)
        {
            if (htmlHelper is null)
                throw new ArgumentNullException("htmlHelper");

            string tagIconHtml = string.Empty;
            if (!string.IsNullOrEmpty(iconClass))
            {
                tagIconHtml = $"<i class=\"{iconClass}\"></i>";
            }

            string toolTagHtml = string.Empty;
            if (useCollapseBtnTools || useRemoveBtnTool)
            {
                TagBuilder btnToolTagBuilder = new TagBuilder("div");
                btnToolTagBuilder.Attributes.Add(new KeyValuePair<string, string>("class", "box-tools pull-right"));

                btnToolTagBuilder.InnerHtml += useCollapseBtnTools ? $"<button type=\"button\" class=\"btn btn-box-tool\" data-widget=\"collapse\"><i class=\"fa fa-minus\"></i></button>" : null;
                btnToolTagBuilder.InnerHtml += useRemoveBtnTool ? $"<button type=\"button\" class=\"btn btn-box-tool\" data-widget=\"remove\"><i class=\"fa fa-remove\"></i></button>" : null;
                toolTagHtml = MvcHtmlString.Create(btnToolTagBuilder.ToString(TagRenderMode.Normal)).ToHtmlString();
            }

            TagBuilder boxHeadTag = new TagBuilder("div")
            {
                InnerHtml = $"{tagIconHtml}<h3 class=\"box-title\">{header ?? ""}</h3>{toolTagHtml}",
                Attributes =
                {
                    new KeyValuePair<string, string>("class","box-header with-border")
                }
            };
            return MvcHtmlString.Create(boxHeadTag.ToString(TagRenderMode.Normal));
        }
        public static void AddFormControlClass(IDictionary<string, object> attributes)
        {
            if (attributes == null && string.IsNullOrEmpty(attributes["class"].ToString()))
                attributes["class"] = "form-control";
            else
                attributes["class"] += "form-control";
        }
    }
}
