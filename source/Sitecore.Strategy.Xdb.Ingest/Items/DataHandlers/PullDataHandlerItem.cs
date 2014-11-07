using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Managers;

namespace Sitecore.Strategy.Xdb.Ingest.Items.DataHandlers
{
    public class PullDataHandlerItem : DataHandlerItem
    {
        public PullDataHandlerItem(Item innerItem) : base(innerItem)
        {
        }
        public static implicit operator PullDataHandlerItem(Item item)
        {
            if (item == null) { return null; }
            var template = TemplateManager.GetTemplate(item.TemplateID, item.Database);
            if (!template.InheritsFrom(IngestionTemplateIDs.PullDataHandler))
            {
                return null;
            }
            return new PullDataHandlerItem(item);
        }
    }
}