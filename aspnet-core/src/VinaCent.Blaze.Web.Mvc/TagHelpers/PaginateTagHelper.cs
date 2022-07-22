using System;
using System.Collections.Generic;
using System.Linq;
using Abp.Collections.Extensions;
using Abp.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace VinaCent.Blaze.Web.TagHelpers
{
    public class PaginateTagHelper : TagHelper
    {
        private const int RelativePageNumberDisplay = 2;
        private const string ActionAttributeName = "page-action";
        private const string ControllerAttributeName = "page-controller";
        private const string AreaAttributeName = "page-area";
        private const string PageAttributeName = "page-page";
        private const string PageHandlerAttributeName = "page-page-handler";
        private const string FragmentAttributeName = "page-fragment";
        private const string HostAttributeName = "page-host";
        private const string ProtocolAttributeName = "page-protocol";
        private const string RouteAttributeName = "page-route";
        private const string RouteValuesDictionaryName = "page-all-route-data";
        private const string RouteValuesPrefix = "page-route-";

        private IDictionary<string, string> _routeValues;
        private bool _routeLink, _pageLink;
        private IHtmlGenerator Generator { get; }

        public PaginateTagHelper(IHtmlGenerator generator)
        {
            Generator = generator;
        }

        #region AttributeName

        /// <summary>
        /// The name of the action method.
        /// </summary>
        [HtmlAttributeName(ActionAttributeName)]
        public string Action { get; set; }

        [HtmlAttributeName(ControllerAttributeName)]
        public string Controller { get; set; }

        [HtmlAttributeName(ProtocolAttributeName)]
        public string Protocol { get; set; }

        [HtmlAttributeName(HostAttributeName)] public string Host { get; set; }
        [HtmlAttributeName(AreaAttributeName)] public string Area { get; set; }

        [HtmlAttributeName(FragmentAttributeName)]

        public string Fragment { get; set; }

        [HtmlAttributeName(RouteAttributeName)]

        public string Route { get; set; }

        [HtmlAttributeName(PageAttributeName)] public string Page { get; set; }

        [HtmlAttributeName(PageHandlerAttributeName)]

        public string PageHandler { get; set; }

        /// <summary>
        /// Additional parameters for the route.
        /// </summary>
        [HtmlAttributeName(RouteValuesDictionaryName, DictionaryAttributePrefix = RouteValuesPrefix)]
        public IDictionary<string, string> RouteValues
        {
            get
            {
                if (_routeValues == null)
                {
                    _routeValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }

                return _routeValues;
            }
            set => _routeValues = value;
        }

        [HtmlAttributeName("skip-count")] public int SkipCount { get; set; }

        [HtmlAttributeName("p")] public int PageNumber { get; set; } = -1;

        [HtmlAttributeName("p-max-result-count")]
        public int PageMaxResultCount { get; set; } = 5;

        [HtmlAttributeName("max-result-count")]
        public int MaxResultCount { get; set; }

        [HtmlAttributeName("total-count")]
        public long TotalCount { get; set; }

        [HtmlAttributeName("custom-li-active-classes")]
        public string CustomLiActiveClass { get; set; } = "active";

        [HtmlAttributeName("custom-li-classes")]
        public string CustomLiClasses { get; set; } = "page-item";

        [HtmlAttributeName("custom-link-active-classes")]
        public string CustomLinkActiveClass { get; set; } = "current";

        [HtmlAttributeName("custom-link-classes")]
        public string CustomLinkClasses { get; set; } = "page-link";

        [HtmlAttributeName("custom-button-first-text")]
        public string CustomButtonFirstText { get; set; } = "<i class=\"fas fa-angle-double-left\"></i>";

        [HtmlAttributeName("custom-button-previous-text")]
        public string CustomButtonPreviousText { get; set; } = "<i class=\"fas fa-angle-left\"></i>";

        [HtmlAttributeName("custom-button-next-text")]
        public string CustomButtonNextText { get; set; } = "<i class=\"fas fa-angle-right\"></i>";

        [HtmlAttributeName("custom-button-last-text")]
        public string CustomButtonLastText { get; set; } = "<i class=\"fas fa-angle-double-right\"></i>";

        #endregion

        [HtmlAttributeNotBound][ViewContext] public ViewContext ViewContext { get; set; }
        private int PageIndex
        {
            get
            {
                if (PageNumber > 0) return PageNumber;
                return (int)Math.Ceiling((float)SkipCount / MaxResultCount) + 1;
            }
        }

        private int TotalPages
        {
            get
            {
                if (PageNumber > 0) return (int)Math.Ceiling((float)TotalCount / PageMaxResultCount);
                return (int)Math.Ceiling((float)TotalCount / MaxResultCount);
            }
        }

        private string GenerateTag(string linkText, int currentPage, string customLinkClasses)
        {
            if (currentPage < 0)
                return $"<a class=\"{customLinkClasses}\">...</a>";

            TagBuilder tagBuilder;

            _routeValues ??= new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (PageNumber > 0)
            {
                _routeValues["p"] = currentPage.ToString();
            }
            else
            {
                _routeValues["maxResultCount"] = MaxResultCount.ToString();
                _routeValues["skipCount"] = ((currentPage - 1) * MaxResultCount).ToString();
            }

            var routeValues = new RouteValueDictionary(_routeValues);
            if (_pageLink)
                tagBuilder = Generator.GeneratePageLink(ViewContext, linkText, Page, PageHandler, Protocol, Host, Fragment, routeValues, null);
            else if (_routeLink)
                tagBuilder = Generator.GenerateRouteLink(ViewContext, linkText, Route, Protocol, Host, Fragment, routeValues, null);
            else
                tagBuilder = Generator.GenerateActionLink(ViewContext, linkText, Action, Controller, Protocol, Host, Fragment, routeValues, null);

            tagBuilder.TagRenderMode = TagRenderMode.StartTag;
            tagBuilder.AddCssClass(customLinkClasses);

            using var writer = new System.IO.StringWriter();
            tagBuilder.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);

            return writer.ToString() + linkText + "</a>";
        }

        protected virtual string BuildLiTag(int currentPage = -1, string linkText = null)
        {
            var disabledClasses = "";

            // For li tag
            var customLiActiveClasses = PageIndex == currentPage ? $" {CustomLiActiveClass}" : "";

            // For a tag
            var customLinkActiveClasses = PageIndex == currentPage ? $" {CustomLinkActiveClass}" : "";

            if (!linkText.IsNullOrWhiteSpace())
            {
                if (currentPage == 1 || currentPage == TotalPages) { disabledClasses = ""; }
                if (currentPage == PageIndex || currentPage == 0) disabledClasses = " disabled";
                customLiActiveClasses = "";
                customLinkActiveClasses = "";
            }

            // For li tag
            var _customLiClasses = CustomLiClasses.Trim() + disabledClasses + customLiActiveClasses;

            // For a tag
            var _customLinkClasses = CustomLinkClasses.Trim() + disabledClasses + customLinkActiveClasses;

            linkText ??= currentPage.ToString();

            return $"<li class=\"{_customLiClasses}\">" + GenerateTag(linkText, currentPage, _customLinkClasses) + "</li>";
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (output == null)
            {
                throw new ArgumentNullException(nameof(output));
            }

            _routeLink = Route != null;
            _pageLink = Page != null || PageHandler != null;

            output.TagName = "ul";
            output.Attributes.SetAttribute("class", context.AllAttributes["class"]?.Value);

            var item = new List<string>
            {
                BuildLiTag(1, CustomButtonFirstText),
                BuildLiTag(Math.Max(PageIndex - 1,1), CustomButtonPreviousText)
            };

            if (TotalPages <= RelativePageNumberDisplay * 2 + 1)
                item.AddRange(Enumerable.Range(1, TotalPages).Select(pageNo => BuildLiTag(pageNo)).ToList());
            else
            {
                var prev = Math.Max(PageIndex - TotalPages + RelativePageNumberDisplay, 0);
                var next = 0;

                if (PageIndex - RelativePageNumberDisplay > 1) item.Add(BuildLiTag());

                for (var i = PageIndex - RelativePageNumberDisplay - prev; i <= PageIndex + RelativePageNumberDisplay + next; i++)
                    if (i < 1) next++;
                    else if (i <= TotalPages) item.Add(BuildLiTag(i));

                if (PageIndex + RelativePageNumberDisplay < TotalPages)
                    item.Add(BuildLiTag());
            }

            item.Add(BuildLiTag(Math.Min(PageIndex + 1, TotalPages), CustomButtonNextText));
            item.Add(BuildLiTag(TotalPages, CustomButtonLastText));

            output.Content.SetHtmlContent(item.JoinAsString(string.Empty));
        }
    }
}