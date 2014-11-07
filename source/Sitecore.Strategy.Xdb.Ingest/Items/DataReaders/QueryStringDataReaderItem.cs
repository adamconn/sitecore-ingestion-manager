using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Strategy.Xdb.Ingest.DataReaders;
using Sitecore.Data.Managers;

namespace Sitecore.Strategy.Xdb.Ingest.Items.DataReaders
{
    public class QueryStringDataReaderItem : DataReaderItem
    {
        public QueryStringDataReaderItem(Item innerItem) : base(innerItem)
        {
        }

        public static implicit operator QueryStringDataReaderItem(Item item)
        {
            if (item == null) { return null; }
            var template = TemplateManager.GetTemplate(item.TemplateID, item.Database);
            if (!template.InheritsFrom(IngestionTemplateIDs.QueryStringDataReader)) { return null; }
            return new QueryStringDataReaderItem(item);
        }
        public override IDataReader GetDataReader()
        {
            var type = this.DataReaderType;
            if (type == null)
            {
                Log.SingleError(string.Format("Could not determine the data reader type for the item {0}", this.InnerItem.ID.ToString()), this);
                return null;
            }
            if (HttpContext.Current == null)
            {
                Log.Error("HttpContext.Current is needed in order to read the query string", this);
                return null;
            }
            var reader = Activator.CreateInstance(type, new object[] { HttpContext.Current }) as QueryStringDataReader;
            if (reader == null)
            {
                Log.SingleError(string.Format("The type {0} is not a {1}", type.FullName, typeof(QueryStringDataReader).FullName), this);
                return null;
            }
            return reader;
        }
    }
}