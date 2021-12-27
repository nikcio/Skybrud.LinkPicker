using Skybrud.LinkPicker.Models;
using Skybrud.LinkPicker.PropertyEditors;
using System.Collections.Generic;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.Editors;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Web;
using Umbraco.Extensions;

namespace Skybrud.LinkPicker.Factories {

    internal class LinkReferenceFactory : IDataValueReferenceFactory, IDataValueReference {
        private readonly IUmbracoContext umbracoContext;

        public LinkReferenceFactory(IUmbracoContextAccessor umbracoContextAccessor) {
            umbracoContextAccessor.TryGetUmbracoContext(out umbracoContext);
        }

        public IDataValueReference GetDataValueReference() => this;

        public IEnumerable<UmbracoEntityReference> GetReferences(object value) {

            List<UmbracoEntityReference> references = new List<UmbracoEntityReference>();
            if (value is not string json) return references;

            LinkPickerLink link = LinkPickerLink.Deserialize(json, umbracoContext);
            if (link == null) return references;

            switch (link.Type) {

                case LinkPickerType.Content:
                    references.Add(new UmbracoEntityReference(new GuidUdi("content", link.Key)));
                    break;

                case LinkPickerType.Media:
                    references.Add(new UmbracoEntityReference(new GuidUdi("media", link.Key)));
                    break;

            }

            return references;

        }

        public bool IsForEditor(IDataEditor dataEditor) => dataEditor.Alias.InvariantEquals(LinkEditor.EditorAlias);

    }

}