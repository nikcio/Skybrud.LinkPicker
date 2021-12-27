using Skybrud.LinkPicker.Factories;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;

namespace Skybrud.LinkPicker.Composers {

    internal class LinkPickerComposer : IComposer {
        public void Compose(IUmbracoBuilder builder) {
            builder.DataValueReferenceFactories().Append<LinkReferenceFactory>();
        }
    }

}