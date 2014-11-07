using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Strategy.Xdb.Ingest.DataReaders;

namespace Sitecore.Strategy.Xdb.Ingest.Items.DataReaders
{
    public class DataReaderItem : CustomItem
    {
        public DataReaderItem(Item innerItem) : base(innerItem)
        {
        }
        public static implicit operator DataReaderItem(Item item)
        {
            if (item == null) { return null; }
            var template = TemplateManager.GetTemplate(item.TemplateID, item.Database);
            if (!template.InheritsFrom(IngestionTemplateIDs.DataReader)) { return null; }
            return new DataReaderItem(item);
        }

        public Type DataReaderType
        {
            get
            {
                var typeName = base[DataReaderFieldIDs.ReaderType];
                if (string.IsNullOrEmpty(typeName))
                {
                    return null;
                }
                return Type.GetType(typeName);
            }
        }

        public Type CustomItemType
        {
            get
            {
                var typeName = base[DataReaderFieldIDs.CustomItemType];
                if (string.IsNullOrEmpty(typeName))
                {
                    return null;
                }
                return Type.GetType(typeName);
            }
        }

        public virtual IDataReader GetDataReader()
        {
            var item2 = Activator.CreateInstance(this.CustomItemType, new[] {this.InnerItem}) as DataReaderItem;
            if (item2 == null)
            {
                Log.SingleError(string.Format("The item {0} could not be converted into a DataReaderItem", this.InnerItem.ID.ToString()), this);
                return null;
            }
            var type = item2.DataReaderType;
            if (type == null)
            {
                Log.SingleError(string.Format("Could not determine the data reader type for the item {0}", this.InnerItem.ID.ToString()), this);
                return null;
            }
            var reader = Activator.CreateInstance(type) as IDataReader;
            if (reader == null)
            {
                Log.SingleError(string.Format("The type {0} is not a {1}", type.FullName, typeof(IDataReader).FullName), this);
                return null;
            }
            return reader;
        }
    }
}