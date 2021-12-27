using Newtonsoft.Json.Linq;
using Skybrud.Essentials.Json;
using Skybrud.LinkPicker.Models;
using System;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace Skybrud.LinkPicker.PropertyEditors.ValueConverters {

    public class LinkValueConverter : PropertyValueConverterBase {

        private readonly IUmbracoContextAccessor _umbracoContextAccessor;

        #region Constructors

        public LinkValueConverter(IUmbracoContextAccessor umbracoContextAccessor) {
            _umbracoContextAccessor = umbracoContextAccessor;
        }

        #endregion

        #region Member methods

        public override bool IsConverter(IPublishedPropertyType propertyType) {
            return propertyType.EditorAlias == "Skybrud.LinkPicker.Link";
        }

        public override object ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object source, bool preview) {
            return source is string str && str.DetectIsJson() ? JsonUtils.ParseJsonObject(str) : null;
        }

        public override object ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview) {
            _umbracoContextAccessor.TryGetUmbracoContext(out var umbracoContext);
            return inter is JObject json ? new LinkPickerLink(json, umbracoContext, propertyType.DataType.ConfigurationAs<LinkConfiguration>()) : null;
        }

        public override object ConvertIntermediateToXPath(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview) {
            return null;
        }

        public override PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType) {
            return PropertyCacheLevel.Snapshot;
        }

        public override Type GetPropertyValueType(IPublishedPropertyType propertyType) {
            return typeof(LinkPickerLink);
        }

        #endregion

    }

}