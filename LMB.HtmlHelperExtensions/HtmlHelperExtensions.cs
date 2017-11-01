using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMB.HtmlHelperExtensions
{
    /// <summary>
    /// Supports the rendering of HTML controls in a view.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        #region Attributes
        #endregion

        #region Properties
        #endregion

        #region Constants
        #endregion

        #region Constructors
        #endregion

        #region Methods

        #region Private

        /// <summary>
        /// Returns an HTML button element by using the specified HTML helper,
        /// the name of the form field, the id of the form field and the text for the button.
        /// </summary>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the System.Web.Mvc.ViewDataDictionary key that is used to look up the value.</param>
        /// <param name="buttonHtmlAttributes">An object that contains the HTML attributes to set for the button element.</param>
        /// <param name="iconHtmlAttributes">An object that contains the HTML attributes to set for the icon element.</param>
        /// <param name="text">The button text to display.</param>
        /// <param name="onclickUri">The URI to be redirected when the user clicks on the button.</param>
        /// <param name="isDisabled">Specifies that the button should be disabled.</param>
        /// <param name="id">The name of the form field.</param>
        /// <returns></returns>
        /// <returns>
        /// An HTML button element with an icon and text.
        /// </returns>
        /// <remarks>
        /// The location href property (location.href) will be automatically setting when not informed at onclick Url parameter.
        /// </remarks>
        private static IHtmlString Button(this HtmlHelper helper, string id, string name, string text, string onclickUri, bool? isDisabled, IDictionary<string, object> buttonHtmlAttributes, IDictionary<string, object> iconHtmlAttributes)
        {
            var fullName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("Value cannot be null or empty.", "name");
            }

            var buttonTagBuilder = new TagBuilder("button");
            var iconTagBuilder = new TagBuilder("i");

            buttonTagBuilder.MergeAttribute("name", fullName, true);

            if (!string.IsNullOrEmpty(id))
            {
                buttonTagBuilder.GenerateId(id);
            }
            else
            {
                buttonTagBuilder.GenerateId(fullName);
            }

            if (!string.IsNullOrEmpty(onclickUri))
            {
                if (onclickUri.Contains("location.href"))
                {
                    buttonTagBuilder.MergeAttribute("onclick", onclickUri);
                }
                else
                {
                    buttonTagBuilder.MergeAttribute("onclick", $"location.href='{onclickUri}'");
                }
            }

            if (isDisabled.HasValue && isDisabled.Value)
            {
                buttonTagBuilder.MergeAttribute("disabled", "disabled");
            }

            if (buttonHtmlAttributes != null)
            {
                buttonTagBuilder.MergeAttributes(buttonHtmlAttributes);
            }

            if (iconHtmlAttributes != null)
            {
                iconTagBuilder.MergeAttributes(iconHtmlAttributes);
            }

            buttonTagBuilder.InnerHtml += iconTagBuilder.ToString();

            if (!string.IsNullOrEmpty(text))
            {
                buttonTagBuilder.InnerHtml += $" {text}";
            }

            return MvcHtmlString.Create(buttonTagBuilder.ToString());
        }

        /// <summary>
        /// Returns an HTML link element rendered as a button by using the specified HTML helper,
        /// the name of the form field, the id of the form field and the text for the link.
        /// </summary>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the System.Web.Mvc.ViewDataDictionary key that is used to look up the value.</param>
        /// <param name="linkHtmlAttributes">An object that contains the HTML attributes to set for the link element.</param>
        /// <param name="iconHtmlAttributes">An object that contains the HTML attributes to set for the icon element.</param>
        /// <param name="text">The button text to display.</param>
        /// <param name="uri">The URI to be redirected when the user clicks on the link.</param>
        /// <param name="isDisabled">Specifies that the link should be disabled.</param>
        /// <param name="id">The name of the form field.</param>
        /// <returns></returns>
        /// <returns>
        /// An HTML a element with an icon, text and redered as a button.
        /// </returns>
        /// <remarks>
        /// The btn btn-default class will be automatically setting when not informed at linkHtmlAttributes parameter.
        /// </remarks>
        private static IHtmlString LinkButton(this HtmlHelper helper, string id, string name, string text, string uri, bool? isDisabled, IDictionary<string, object> linkHtmlAttributes, IDictionary<string, object> iconHtmlAttributes)
        {
            var fullName = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(name);

            if (String.IsNullOrEmpty(fullName))
            {
                throw new ArgumentException("Value cannot be null or empty.", "name");
            }

            if (String.IsNullOrEmpty(uri))
            {
                throw new ArgumentException("Value cannot be null or empty.", "uri");
            }

            var aTagBuilder = new TagBuilder("a");
            var iconTagBuilder = new TagBuilder("i");

            aTagBuilder.MergeAttribute("name", fullName, true);

            if (!string.IsNullOrEmpty(id))
            {
                aTagBuilder.GenerateId(id);
            }
            else
            {
                aTagBuilder.GenerateId(fullName);
            }

            if (isDisabled.HasValue && isDisabled.Value)
            {
                aTagBuilder.MergeAttribute("href", "javascript:function() { return false; }");
                aTagBuilder.MergeAttribute("style", "pointer-events:none; cursor:default; opacity: 0.6;");
            }
            else if (!string.IsNullOrEmpty(uri))
            {
                aTagBuilder.MergeAttribute("href", uri);
            }

            if (linkHtmlAttributes != null)
            {
                aTagBuilder.MergeAttributes(linkHtmlAttributes);
            }

            if (iconHtmlAttributes != null)
            {
                iconTagBuilder.MergeAttributes(iconHtmlAttributes);
            }

            if (!aTagBuilder.Attributes.Any(a => a.Key.Equals("class", StringComparison.InvariantCultureIgnoreCase) &&
                                                 a.Value.Equals("btn ", StringComparison.InvariantCultureIgnoreCase)))
            {
                aTagBuilder.AddCssClass("btn");
            }

            if (!aTagBuilder.Attributes.Any(a => a.Key.Equals("class", StringComparison.InvariantCultureIgnoreCase) &&
                                                 a.Value.Contains("btn-")))
            {
                aTagBuilder.AddCssClass("btn-default");
            }

            aTagBuilder.InnerHtml += iconTagBuilder.ToString();

            if (!string.IsNullOrEmpty(text))
            {
                aTagBuilder.InnerHtml += $" {text}";
            }

            return MvcHtmlString.Create(aTagBuilder.ToString());
        }

        #endregion

        #region Public

        #region Button Helper

        /// <summary>
        /// Returns an HTML button element by using the specified HTML helper and the name of the form field.
        /// </summary>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="name">
        /// The name of the form field and the System.Web.Mvc.ViewDataDictionary key that is used to look up the value.
        /// </param>
        /// <param name="iconHtmlAttributes">An object that contains the HTML attributes to set for the icon element.</param>
        /// <returns>
        /// An HTML button element with an icon.
        /// </returns>
        public static IHtmlString Button(this HtmlHelper helper, string name, object iconHtmlAttributes)
        {
            return Button(helper, id: null, name: name, text: null, onclickUri: null, isDisabled: null,
                buttonHtmlAttributes: null, iconHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(iconHtmlAttributes));
        }

        /// <summary>
        /// Returns an HTML button element by using the specified HTML helper,
        /// the name of the form field and the text for the button.
        /// </summary>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the System.Web.Mvc.ViewDataDictionary key that is used to look up the value.</param>
        /// <param name="text">The button text to display.</param>
        /// <param name="iconHtmlAttributes">An object that contains the HTML attributes to set for the icon element.</param>
        /// <returns>
        /// An HTML button element with an icon and text.
        /// </returns>
        public static IHtmlString Button(this HtmlHelper helper, string name, string text, object iconHtmlAttributes)
        {
            return Button(helper, id: null, name: name, text: text, onclickUri: null, isDisabled: null,
                buttonHtmlAttributes: null, iconHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(iconHtmlAttributes));
        }

        /// <summary>
        /// Returns an HTML button element by using the specified HTML helper and the name of the form field.
        /// </summary>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the System.Web.Mvc.ViewDataDictionary key that is used to look up the value.</param>
        /// <param name="buttonHtmlAttributes">An object that contains the HTML attributes to set for the button element.</param>
        /// <param name="iconHtmlAttributes">An object that contains the HTML attributes to set for the icon element.</param>
        /// <returns>
        /// An HTML button element with an icon.
        /// </returns>
        public static IHtmlString Button(this HtmlHelper helper, string name, object buttonHtmlAttributes, object iconHtmlAttributes)
        {
            return Button(helper, id: null, name: name, text: null, onclickUri: null, isDisabled: null,
                buttonHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(buttonHtmlAttributes),
                iconHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(iconHtmlAttributes));
        }

        /// <summary>
        /// Returns an HTML button element by using the specified HTML helper,
        /// the name of the form field and the text for the button.
        /// </summary>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the System.Web.Mvc.ViewDataDictionary key that is used to look up the value.</param>
        /// <param name="buttonHtmlAttributes">An object that contains the HTML attributes to set for the button element.</param>
        /// <param name="iconHtmlAttributes">An object that contains the HTML attributes to set for the icon element.</param>
        /// <param name="text">The button text to display.</param>
        /// <returns></returns>
        /// <returns>
        /// An HTML button element with an icon and text.
        /// </returns>
        public static IHtmlString Button(this HtmlHelper helper, string name, string text, object buttonHtmlAttributes, object iconHtmlAttributes)
        {
            return Button(helper, id: null, name: name, text: text, onclickUri: null, isDisabled: null,
                buttonHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(buttonHtmlAttributes),
                iconHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(iconHtmlAttributes));
        }

        /// <summary>
        /// Returns an HTML button element by using the specified HTML helper and the name of the form field.
        /// </summary>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the System.Web.Mvc.ViewDataDictionary key that is used to look up the value.</param>
        /// <param name="buttonHtmlAttributes">An object that contains the HTML attributes to set for the button element.</param>
        /// <param name="iconHtmlAttributes">An object that contains the HTML attributes to set for the icon element.</param>
        /// <param name="isDisabled">Specifies that the button should be disabled.</param>
        /// <returns>
        /// An HTML button element with an icon.
        /// </returns>
        public static IHtmlString Button(this HtmlHelper helper, string name, bool? isDisabled, object buttonHtmlAttributes, object iconHtmlAttributes)
        {
            return Button(helper, id: null, name: name, text: null, onclickUri: null, isDisabled: isDisabled,
                buttonHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(buttonHtmlAttributes),
                iconHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(iconHtmlAttributes));
        }

        /// <summary>
        /// Returns an HTML button element by using the specified HTML helper,
        /// the name of the form field and the text for the button.
        /// </summary>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the System.Web.Mvc.ViewDataDictionary key that is used to look up the value.</param>
        /// <param name="buttonHtmlAttributes">An object that contains the HTML attributes to set for the button element.</param>
        /// <param name="iconHtmlAttributes">An object that contains the HTML attributes to set for the icon element.</param>
        /// <param name="text">The button text to display.</param>
        /// <param name="onclickUri">The URI to be redirected when the user clicks on the button.</param>
        /// <returns></returns>
        /// <returns>
        /// An HTML button element with an icon and text.
        /// </returns>
        /// <remarks>
        /// The location href property (location.href) will be automatically setting when not informed at onclick Url parameter.
        /// </remarks>
        public static IHtmlString Button(this HtmlHelper helper, string name, string text, string onclickUri, object buttonHtmlAttributes, object iconHtmlAttributes)
        {
            return Button(helper, id: null, name: name, text: text, onclickUri: onclickUri, isDisabled: null,
                buttonHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(buttonHtmlAttributes),
                iconHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(iconHtmlAttributes));
        }

        /// <summary>
        /// Returns an HTML button element by using the specified HTML helper and the name of the form field.
        /// </summary>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the System.Web.Mvc.ViewDataDictionary key that is used to look up the value.</param>
        /// <param name="buttonHtmlAttributes">An object that contains the HTML attributes to set for the button element.</param>
        /// <param name="iconHtmlAttributes">An object that contains the HTML attributes to set for the icon element.</param>
        /// <param name="isDisabled">Specifies that the button should be disabled.</param>
        /// <param name="onclickUri">The URI to be redirected when the user clicks on the button.</param>
        /// <returns>
        /// An HTML button element with an icon.
        /// </returns>
        /// <remarks>
        /// The location href property (location.href) will be automatically setting when not informed at onclick Url parameter.
        /// </remarks>
        public static IHtmlString Button(this HtmlHelper helper, string name, string onclickUri, bool? isDisabled, object buttonHtmlAttributes, object iconHtmlAttributes)
        {
            return Button(helper, id: null, name: name, text: null, onclickUri: onclickUri, isDisabled: isDisabled,
                buttonHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(buttonHtmlAttributes),
                iconHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(iconHtmlAttributes));
        }

        /// <summary>
        /// Returns an HTML button element by using the specified HTML helper,
        /// the name of the form field and the text for the button.
        /// </summary>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the System.Web.Mvc.ViewDataDictionary key that is used to look up the value.</param>
        /// <param name="buttonHtmlAttributes">An object that contains the HTML attributes to set for the button element.</param>
        /// <param name="iconHtmlAttributes">An object that contains the HTML attributes to set for the icon element.</param>
        /// <param name="text">The button text to display.</param>
        /// <param name="onclickUri">The URI to be redirected when the user clicks on the button.</param>
        /// <param name="isDisabled">Specifies that the button should be disabled.</param>
        /// <returns></returns>
        /// <returns>
        /// An HTML button element with an icon and text.
        /// </returns>
        /// <remarks>
        /// The location href property (location.href) will be automatically setting when not informed at onclick Url parameter.
        /// </remarks>
        public static IHtmlString Button(this HtmlHelper helper, string name, string text, string onclickUri, bool? isDisabled, object buttonHtmlAttributes, object iconHtmlAttributes)
        {
            return Button(helper, id: null, name: name, text: text, onclickUri: onclickUri, isDisabled: isDisabled,
                buttonHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(buttonHtmlAttributes),
                iconHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(iconHtmlAttributes));
        }

        /// <summary>
        /// Returns an HTML button element by using the specified HTML helper,
        /// the name of the form field, the id of the form field and the text for the button.
        /// </summary>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the System.Web.Mvc.ViewDataDictionary key that is used to look up the value.</param>
        /// <param name="buttonHtmlAttributes">An object that contains the HTML attributes to set for the button element.</param>
        /// <param name="iconHtmlAttributes">An object that contains the HTML attributes to set for the icon element.</param>
        /// <param name="text">The button text to display.</param>
        /// <param name="onclickUri">The URI to be redirected when the user clicks on the button.</param>
        /// <param name="isDisabled">Specifies that the button should be disabled.</param>
        /// <param name="id">The name of the form field.</param>
        /// <returns></returns>
        /// <returns>
        /// An HTML button element with an icon and text.
        /// </returns>
        /// <remarks>
        /// The location href property (location.href) will be automatically setting when not informed at onclick Url parameter.
        /// </remarks>
        public static IHtmlString Button(this HtmlHelper helper, string id, string name, string text, string onclickUri, bool? isDisabled, object buttonHtmlAttributes, object iconHtmlAttributes)
        {
            return Button(helper, id: id, name: name, text: text, onclickUri: onclickUri, isDisabled: isDisabled,
                buttonHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(buttonHtmlAttributes),
                iconHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(iconHtmlAttributes));
        }

        #endregion

        #region LinkButton Helper

        /// <summary>
        /// Returns an HTML link element rendered as a button by using the specified HTML helper and the name of the form field.
        /// </summary>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="name">
        /// The name of the form field and the System.Web.Mvc.ViewDataDictionary key that is used to look up the value.
        /// </param>
        /// <param name="uri">The URI to be redirected when the user clicks on the link.</param>
        /// <param name="iconHtmlAttributes">An object that contains the HTML attributes to set for the icon element.</param>
        /// <returns>
        /// An HTML link element with an icon and rendered as a button.
        /// </returns>
        /// <remarks>
        /// The btn btn-default class will be automatically setting when not informed at linkHtmlAttributes parameter.
        /// </remarks>
        public static IHtmlString LinkButton(this HtmlHelper helper, string name, string uri, object iconHtmlAttributes)
        {
            return LinkButton(helper, id: null, name: name, text: null, uri: uri, isDisabled: null,
                linkHtmlAttributes: null, iconHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(iconHtmlAttributes));
        }

        /// <summary>
        /// Returns an HTML link element rendered as a button by using the specified HTML helper,
        /// the name of the form field and the text for the link.
        /// </summary>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the System.Web.Mvc.ViewDataDictionary key that is used to look up the value.</param>
        /// <param name="text">The link text to display.</param>
        /// <param name="uri">The URI to be redirected when the user clicks on the link.</param>
        /// <param name="iconHtmlAttributes">An object that contains the HTML attributes to set for the icon element.</param>
        /// <returns>
        /// An HTML link element with an icon, text and rendered as a button.
        /// </returns>
        /// <remarks>
        /// The btn btn-default class will be automatically setting when not informed at linkHtmlAttributes parameter.
        /// </remarks>
        public static IHtmlString LinkButton(this HtmlHelper helper, string name, string text, string uri, object iconHtmlAttributes)
        {
            return LinkButton(helper, id: null, name: name, text: text, uri: uri, isDisabled: null,
                linkHtmlAttributes: null, iconHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(iconHtmlAttributes));
        }

        /// <summary>
        /// Returns an HTML link element rendered as a button by using the specified HTML helper and the name of the form field.
        /// </summary>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the System.Web.Mvc.ViewDataDictionary key that is used to look up the value.</param>
        /// <param name="uri">The URI to be redirected when the user clicks on the link.</param>
        /// <param name="linkHtmlAttributes">An object that contains the HTML attributes to set for the link element.</param>
        /// <param name="iconHtmlAttributes">An object that contains the HTML attributes to set for the icon element.</param>
        /// <returns>
        /// An HTML link element with an icon and rendered as a button.
        /// </returns>
        /// <remarks>
        /// The btn btn-default class will be automatically setting when not informed at linkHtmlAttributes parameter.
        /// </remarks>
        public static IHtmlString LinkButton(this HtmlHelper helper, string name, string uri, object linkHtmlAttributes, object iconHtmlAttributes)
        {
            return LinkButton(helper, id: null, name: name, text: null, uri: uri, isDisabled: null,
                linkHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(linkHtmlAttributes),
                iconHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(iconHtmlAttributes));
        }

        /// <summary>
        /// Returns an HTML link element rendered as a button by using the specified HTML helper,
        /// the name of the form field and the text for the link.
        /// </summary>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the System.Web.Mvc.ViewDataDictionary key that is used to look up the value.</param>
        /// <param name="linkHtmlAttributes">An object that contains the HTML attributes to set for the link element.</param>
        /// <param name="iconHtmlAttributes">An object that contains the HTML attributes to set for the icon element.</param>
        /// <param name="text">The link text to display.</param>
        /// <param name="uri">The URI to be redirected when the user clicks on the link.</param>
        /// <returns></returns>
        /// <returns>
        /// An HTML link element with an icon, text and rendered as a button.
        /// </returns>
        /// <remarks>
        /// The btn btn-default class will be automatically setting when not informed at linkHtmlAttributes parameter.
        /// </remarks>
        public static IHtmlString LinkButton(this HtmlHelper helper, string name, string text, string uri, object linkHtmlAttributes, object iconHtmlAttributes)
        {
            return LinkButton(helper, id: null, name: name, text: text, uri: uri, isDisabled: null,
                linkHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(linkHtmlAttributes),
                iconHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(iconHtmlAttributes));
        }

        /// <summary>
        /// Returns an HTML link element rendered as a button by using the specified HTML helper and the name of the form field.
        /// </summary>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the System.Web.Mvc.ViewDataDictionary key that is used to look up the value.</param>
        /// <param name="linkHtmlAttributes">An object that contains the HTML attributes to set for the link element.</param>
        /// <param name="iconHtmlAttributes">An object that contains the HTML attributes to set for the icon element.</param>
        /// <param name="isDisabled">Specifies that the link should be disabled.</param>
        /// <param name="uri">The URI to be redirected when the user clicks on the link.</param>
        /// <returns>
        /// An HTML link element with an icon and rendered as a button.
        /// </returns>
        /// <remarks>
        /// The btn btn-default class will be automatically setting when not informed at linkHtmlAttributes parameter.
        /// </remarks>
        public static IHtmlString LinkButton(this HtmlHelper helper, string name, string uri, bool? isDisabled, object linkHtmlAttributes, object iconHtmlAttributes)
        {
            return LinkButton(helper, id: null, name: name, text: null, uri: uri, isDisabled: isDisabled,
                linkHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(linkHtmlAttributes),
                iconHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(iconHtmlAttributes));
        }

        /// <summary>
        /// Returns an HTML link element rendered as a button by using the specified HTML helper,
        /// the name of the form field and the text for the link.
        /// </summary>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the System.Web.Mvc.ViewDataDictionary key that is used to look up the value.</param>
        /// <param name="linkHtmlAttributes">An object that contains the HTML attributes to set for the link element.</param>
        /// <param name="iconHtmlAttributes">An object that contains the HTML attributes to set for the icon element.</param>
        /// <param name="text">The link text to display.</param>
        /// <param name="uri">The URI to be redirected when the user clicks on the link.</param>
        /// <param name="isDisabled">Specifies that the link should be disabled.</param>
        /// <returns></returns>
        /// <returns>
        /// An HTML link element with an icon, text and rendered as a button.
        /// </returns>
        /// <remarks>
        /// The btn btn-default class will be automatically setting when not informed at linkHtmlAttributes parameter.
        /// </remarks>
        public static IHtmlString LinkButton(this HtmlHelper helper, string name, string text, string uri, bool? isDisabled, object linkHtmlAttributes, object iconHtmlAttributes)
        {
            return LinkButton(helper, id: null, name: name, text: text, uri: uri, isDisabled: isDisabled,
                linkHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(linkHtmlAttributes),
                iconHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(iconHtmlAttributes));
        }

        /// <summary>
        /// Returns an HTML link element rendered as a button by using the specified HTML helper,
        /// the name of the form field, the id of the form field and the text for the link.
        /// </summary>
        /// <param name="helper">The HTML helper instance that this method extends.</param>
        /// <param name="name">The name of the form field and the System.Web.Mvc.ViewDataDictionary key that is used to look up the value.</param>
        /// <param name="linkHtmlAttributes">An object that contains the HTML attributes to set for the link element.</param>
        /// <param name="iconHtmlAttributes">An object that contains the HTML attributes to set for the icon element.</param>
        /// <param name="text">The link text to display.</param>
        /// <param name="uri">The URI to be redirected when the user clicks on the link.</param>
        /// <param name="isDisabled">Specifies that the link should be disabled.</param>
        /// <param name="id">The name of the form field.</param>
        /// <returns></returns>
        /// <returns>
        /// An HTML link element with an icon, text and rendered as a button.
        /// </returns>
        /// <remarks>
        /// The btn btn-default class will be automatically setting when not informed at linkHtmlAttributes parameter.
        /// </remarks>
        public static IHtmlString LinkButton(this HtmlHelper helper, string id, string name, string text, string uri, bool? isDisabled, object linkHtmlAttributes, object iconHtmlAttributes)
        {
            return LinkButton(helper, id: id, name: name, text: text, uri: uri, isDisabled: isDisabled,
                linkHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(linkHtmlAttributes),
                iconHtmlAttributes: HtmlHelper.AnonymousObjectToHtmlAttributes(iconHtmlAttributes));
        }

        #endregion

        #endregion

        #region Protected
        #endregion

        #endregion

        #region Events
        #endregion
    }
}