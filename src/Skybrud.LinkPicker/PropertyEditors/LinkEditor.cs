using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;

namespace Skybrud.LinkPicker.PropertyEditors {

    [DataEditor(EditorAlias, EditorType.PropertyValue, "Skybrud LinkPicker Link", EditorView, ValueType = ValueTypes.Json, Group = "Skybrud.dk", Icon = "icon-link")]
    public class LinkEditor : DataEditor {
        private readonly IIOHelper iOHelper;

        /// <summary>
        /// Gets the alias of the editor.
        /// </summary>
        public const string EditorAlias = "Skybrud.LinkPicker.Link";

        /// <summary>
        /// Gets the view of the editor.
        /// </summary>
        public const string EditorView = "/App_Plugins/Skybrud.LinkPicker/Views/Editors/Link.html";

        public LinkEditor(IDataValueEditorFactory dataValueEditorFactory, IIOHelper iOHelper, EditorType type = EditorType.PropertyValue) : base(dataValueEditorFactory, type) {
            this.iOHelper = iOHelper;
        }



        #region Member methods

        /// <inheritdoc />
        protected override IConfigurationEditor CreateConfigurationEditor() => new LinkConfigurationEditor(iOHelper);

        #endregion

    }

}