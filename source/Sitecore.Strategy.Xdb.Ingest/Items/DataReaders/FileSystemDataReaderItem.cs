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
    public class FileSystemDataReaderItem : DataReaderItem
    {
        public FileSystemDataReaderItem(Item innerItem) : base(innerItem)
        {
        }
        
        public static implicit operator FileSystemDataReaderItem(Item item)
        {
            if (item == null) { return null; }
            var template = TemplateManager.GetTemplate(item.TemplateID, item.Database);
            if (!template.InheritsFrom(IngestionTemplateIDs.FileSystemDataReader)) { return null; }
            return new FileSystemDataReaderItem(item);
        }

        public string FilePath
        {
            get
            {
                return base[FileSystemDataReaderFieldIDs.FilePath];
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
            var reader = Activator.CreateInstance(type) as FileSystemDataReader;
            if (reader == null)
            {
                Log.SingleError(string.Format("The type {0} is not a {1}", type.FullName, typeof(FileSystemDataReader).FullName), this);
                return null;
            }
            reader.FilePath = this.FilePath;
            return reader;
        }
    }
}