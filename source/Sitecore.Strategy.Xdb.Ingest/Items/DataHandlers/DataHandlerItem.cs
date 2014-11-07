using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Managers;
using Sitecore.Strategy.Contacts.Items;
using Sitecore.Strategy.Xdb.Ingest.Items.DataReaders;
using Sitecore.Strategy.Xdb.Ingest.Items.Hydrators;

namespace Sitecore.Strategy.Xdb.Ingest.Items.DataHandlers
{
    public class DataHandlerItem : CustomItem
    {
        public DataHandlerItem(Item innerItem) : base(innerItem)
        {
        }
        public static implicit operator DataHandlerItem(Item item)
        {
            if (item == null) { return null; }
            var template = TemplateManager.GetTemplate(item.TemplateID, item.Database);
            if (!template.InheritsFrom(IngestionTemplateIDs.IngestionDataHandler))
            {
                return null;
            }
            return new DataHandlerItem(item);
        }

        public bool ApplyNullValues
        {
            get
            {
                CheckboxField field = this.InnerItem.Fields[DataHandlerFieldIDs.ApplyNullValues];
                if (field == null)
                {
                    return false;
                }
                return field.Checked;
            }
        }
        public bool Disabled
        {
            get
            {
                CheckboxField field = this.InnerItem.Fields[DataHandlerFieldIDs.Disabled];
                if (field == null)
                {
                    return false;
                }
                return field.Checked;
            }
        }

        public DataReaderItem DataReader
        {
            get
            {
                DataReaderItem item = this.Database.GetItem(this[DataHandlerFieldIDs.DataReader]);
                var item2 = Activator.CreateInstance(item.CustomItemType, new object[] { item.InnerItem }) as DataReaderItem;
                return item2;
            }
        }

        public ContactFacetHydratorItem DataHydrator
        {
            get
            {
                ContactFacetHydratorItem item = this.Database.GetItem(this[DataHandlerFieldIDs.DataHydrator]);
                return item;
            }
        }

        public ContactFacetNameItem ContactFacet
        {
            get
            {
                ContactFacetNameItem item = this.Database.GetItem(this[ContactFacetHydratorFieldIDs.ContactFacet]);
                return item;
            }
        }
    }
}