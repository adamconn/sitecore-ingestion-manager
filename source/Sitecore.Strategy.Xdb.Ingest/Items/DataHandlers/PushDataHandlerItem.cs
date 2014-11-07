using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Managers;

namespace Sitecore.Strategy.Xdb.Ingest.Items.DataHandlers
{
    public class PushDataHandlerItem : DataHandlerItem
    {
        public PushDataHandlerItem(Item innerItem) : base(innerItem)
        {
        }
        public static implicit operator PushDataHandlerItem(Item item)
        {
            if (item == null) { return null; }
            var template = TemplateManager.GetTemplate(item.TemplateID, item.Database);
            if (!template.InheritsFrom(IngestionTemplateIDs.PushDataHandler))
            {
                return null;
            }
            return new PushDataHandlerItem(item);
        }
        public string HandlerName
        {
            get
            {
                return this[PushHandlerFieldIDs.HandlerName];
            }
        }
    }
}