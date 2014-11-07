using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ContentSearch;
using Sitecore.Data;
using Sitecore.Strategy.Xdb.Ingest.Items;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Strategy.Xdb.Ingest.DataHandlers;
using Sitecore.Strategy.Xdb.Ingest.Items.DataHandlers;

namespace Sitecore.Strategy.Xdb.Ingest.Pipelines.Push
{
    public class PushDataHandlerSearchResultItem : SearchResultItem
    {
        public string HandlerName { get; set; }
        public ID DataReaderId { get; set; }
        public ID HydratorId { get; set; }
        public bool Disabled { get; set; }
    }

    public class GetPushHandlerFromUrl : IPushPipelineProcessor
    {
        protected virtual IEnumerable<ID> GetDataHandlerItemIds(string handlerName, Database database)
        {
            var index = ContentSearchManager.GetIndex(string.Format("sitecore_{0}_index", database.Name));
            using (var context = index.CreateSearchContext())
            {
                var result = context.GetQueryable<PushDataHandlerSearchResultItem>()
                    .Where(item => item.ItemId != IngestionItemIDs.PushDataHandlersFolder)
                    .Where(item => item.Paths.Contains(IngestionItemIDs.PushDataHandlersFolder))
                    .Where(item => item.HandlerName.Equals(handlerName))
                    .Where(item => !item.Disabled)
                    .Select(item => item.ItemId)
                    .ToArray();
                return result;
            }
        }

        public virtual void Process(PushPipelineArgs args)
        {
            if (args == null) { throw new ArgumentNullException("args"); }
            if (args.HttpContext == null) { throw new NullReferenceException("args.HttpContext"); }
            if (args.HttpContext.Request == null) { throw new NullReferenceException("args.HttpContext.Request"); }
            var handlerName = args.HttpContext.Request.Url.Segments.Last().Replace("/", string.Empty);
            if (string.IsNullOrEmpty(handlerName))
            {
                return;
            }
            if (args == null)
            {
                throw new ArgumentNullException("args");
            }
            var database = Sitecore.Context.Database;
            if (database == null)
            {
                throw new NullReferenceException("Sitecore.Context.Database");
            }
            var ids = GetDataHandlerItemIds(handlerName, database);
            foreach (var id in ids)
            {
                PushDataHandlerItem pdhItem = database.GetItem(id);
                if (pdhItem == null)
                {
                    continue;
                }
                var reader = pdhItem.DataReader.GetDataReader();
                var hydrator = pdhItem.DataHydrator.GetHydrator();
                var handler = new DataHandler() { DataReader = reader, Hydrator = hydrator, ApplyNullValues = pdhItem.ApplyNullValues };
                if (!args.DataHandlers.Contains(handler))
                {
                    args.DataHandlers.Add(handler);
                }
            }

        }
    }
}