﻿using Newtonsoft.Json;
using Skybrud.LinkPicker.Models.Tracking;
using System;
using System.Collections.Generic;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.Models.Editors;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Services;

namespace Skybrud.LinkPicker.Tracking
{
    public class LinkMediaTracking : IDataValueReferenceFactory, IDataValueReference
    {
        private readonly IMediaService _mediaService;

        public IDataValueReference GetDataValueReference() => this;

        public LinkMediaTracking(IMediaService mediaService)
        {
            _mediaService = mediaService;
        }

        public IEnumerable<UmbracoEntityReference> GetReferences(object value)
        {
            var references = new List<UmbracoEntityReference>();
            if (value != null)
            {
                var redirectLink = JsonConvert.DeserializeObject<LinkData>(value.ToString());
                if (redirectLink?.Destination?.Type == "media")
                {
                    AddReferenceFromMediaPath(references, redirectLink?.Destination?.Url);
                }
                else if (redirectLink?.Destination?.Type == "content")
                {
                    AddReferenceToContent(references, redirectLink?.Destination?.Key);
                }
            }
            return references;
        }
        private void AddReferenceToContent(List<UmbracoEntityReference> references, string key)
        {
            Udi udi = new GuidUdi("content", Guid.Parse(key));
            references.Add(new UmbracoEntityReference(udi));
        }

        private void AddReferenceFromMediaPath(List<UmbracoEntityReference> references, string imagePath)
        {
            IMedia media = _mediaService.GetMediaByPath(imagePath);
            if (media == null) return;
            Udi udi = new GuidUdi("media", media.Key);
            references.Add(new UmbracoEntityReference(udi));
        }

        public bool IsForEditor(IDataEditor dataEditor) => dataEditor.Alias.InvariantEquals("Skybrud.LinkPicker.Link");
    }

}