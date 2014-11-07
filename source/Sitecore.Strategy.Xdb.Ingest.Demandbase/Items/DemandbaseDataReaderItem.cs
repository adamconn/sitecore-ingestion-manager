using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Strategy.Xdb.Ingest.DataReaders;
using Sitecore.Data.Managers;
using Sitecore.Strategy.Xdb.Ingest.Items.DataReaders;

namespace Sitecore.Strategy.Xdb.Ingest.Demandbase.Items
{
    public class DemandbaseDataReaderItem : JsonDataReaderItem
    {
        public DemandbaseDataReaderItem(Item innerItem) : base(innerItem)
        {
        }
        
        public static implicit operator DemandbaseDataReaderItem(Item item)
        {
            if (item == null) { return null; }
            var template = TemplateManager.GetTemplate(item.TemplateID, item.Database);
            if (!template.InheritsFrom(DemandbaseTemplateIDs.DemandbaseDataReader)) { return null; }
            return new DemandbaseDataReaderItem(item);
        }

        public string ApiKey
        {
            get
            {
                return base[DemandbaseFieldIDs.ApiKey];
            }
        }

        public NameValueCollection IPAddressMappings
        {
            get
            {
                NameValueListField field = this.InnerItem.Fields[DemandbaseFieldIDs.IPAddressMappings];
                if (field == null)
                {
                    return null;
                }
                return field.NameValues;
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
            reader.Parameters["key"] = this.ApiKey;
            var ipAddress = HttpContext.Current.Request.UserHostAddress;
            var mappings = this.IPAddressMappings;
            if (mappings != null)
            {
                var overrideAddress = mappings[ipAddress.Replace('.', '_')];
                if (! string.IsNullOrEmpty(overrideAddress))
                {
                    Log.Info(string.Format("Will pass the IP address {0} instead of {1} to Demandbase", overrideAddress, ipAddress), this);
                    ipAddress = overrideAddress;
                }
            }
            reader.Parameters["query"] = ipAddress;
            Log.Debug(string.Format("The IP address {0} will be passed to Demandbase", ipAddress), this);
            return reader;
        }
    }
}