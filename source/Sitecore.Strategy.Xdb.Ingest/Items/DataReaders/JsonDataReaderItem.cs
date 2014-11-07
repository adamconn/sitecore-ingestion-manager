using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Strategy.Xdb.Ingest.DataReaders;

namespace Sitecore.Strategy.Xdb.Ingest.Items.DataReaders
{
    public class JsonDataReaderItem : DataReaderItem
    {
        public JsonDataReaderItem(Item innerItem) : base(innerItem)
        {
        }
        public static implicit operator JsonDataReaderItem(Item item)
        {
            if (item == null) { return null; }
            var template = TemplateManager.GetTemplate(item.TemplateID, item.Database);
            if (!template.InheritsFrom(IngestionTemplateIDs.JsonDataReader)) { return null; }
            return new JsonDataReaderItem(item);
        }
        public string Endpoint
        {
            get
            {
                return base[JsonDataReaderFieldIDs.Endpoint];
            }
        }
        public bool UseOAuth
        {
            get
            {
                CheckboxField field = this.InnerItem.Fields[JsonDataReaderFieldIDs.UseOAuth];
                if (field == null)
                {
                    return false;
                }
                return field.Checked;
            }
        }
        public string OAuthApiKey
        {
            get
            {
                return base[JsonDataReaderFieldIDs.OAuthApiKey];
            }
        }
        public string OAuthSecret
        {
            get
            {
                return base[JsonDataReaderFieldIDs.OAuthSecret];
            }
        }

        public override IDataReader GetDataReader()
        {
            var type = this.DataReaderType;
            if (type == null)
            {
                Log.SingleError(string.Format("Could not determine the data reader type for the item {0}", this.InnerItem.ID.ToString()), this);
                return null;
            }
            var reader = Activator.CreateInstance(type) as JsonDataReader;
            if (reader == null)
            {
                Log.SingleError(string.Format("The type {0} is not a {1}", type.FullName, typeof(JsonDataReader).FullName), this);
                return null;
            }
            reader.Endpoint = this.Endpoint;
            reader.UseOauth = this.UseOAuth;
            reader.OAuthApiKey = this.OAuthApiKey;
            reader.OAuthSecret = this.OAuthSecret;
            return reader;
        }

    }
}