using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Strategy.Xdb.Ingest.DataHandlers;
using Sitecore.Strategy.Xdb.Ingest.Items;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Data;
using Sitecore.Strategy.Xdb.Ingest.Items.DataHandlers;

namespace Sitecore.Strategy.Xdb.Ingest.Pipelines.Pull
{
    public class DataHandlerSearchResultItem : SearchResultItem
    {
        public ID DataReaderId { get; set; }
        public ID HydratorId { get; set; }
        public bool Disabled { get; set; }
    }

    public class GetActiveDataHandlers : IPullPipelineProcessor
    {
        public string IndexName { get; set; }

        protected virtual IEnumerable<ID> GetDataHandlerItemIds(Database database)
        {
            var index = ContentSearchManager.GetIndex(string.Format("sitecore_{0}_index", database.Name));
            using (var context = index.CreateSearchContext())
            {
                var result = context.GetQueryable<DataHandlerSearchResultItem>()
                    .Where(item => item.ItemId != IngestionItemIDs.PullDataHandlersFolder)
                    .Where(item => item.Paths.Contains(IngestionItemIDs.PullDataHandlersFolder))
                    .Where(item => ! item.Disabled)
                    .Select(item => item.ItemId)
                    .ToArray();
                return result;
            }
        }
        public virtual void Process(PullPipelineArgs args)
        {
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }
            var database = Sitecore.Context.Database;
            if (database == null)
            {
                throw new NullReferenceException("Sitecore.Context.Database");
            }
            var ids = GetDataHandlerItemIds(database);
            foreach (var id in ids)
            {
                PullDataHandlerItem pdhItem = database.GetItem(id);
                if (pdhItem == null)
                {
                    continue;
                }
                var reader = pdhItem.DataReader.GetDataReader();
                var hydrator = pdhItem.DataHydrator.GetHydrator();
                var handler = new DataHandler() {DataReader = reader, Hydrator = hydrator, ApplyNullValues = pdhItem.ApplyNullValues};
                if (!args.DataHandlers.Contains(handler))
                {
                    args.DataHandlers.Add(handler);
                }
            }
        }
    }
}