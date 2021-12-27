using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;

namespace Skybrud.LinkPicker.PropertyEditors {

    public class LinkConfigurationEditor : ConfigurationEditor<LinkConfiguration> {
        public LinkConfigurationEditor(IIOHelper ioHelper) : base(ioHelper) {
        }
    }

}