using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Configuration;
using Sitecore.Diagnostics;
using Sitecore.Strategy.Contacts.DataProviders;
using Sitecore.Strategy.Contacts.Pipelines.GetFacetMemberValues;
using Sitecore.Strategy.Xdb.Ingest.Demandbase.Config;
using Sitecore.Strategy.Xdb.Ingest.Demandbase.Models;

namespace Sitecore.Strategy.Xdb.Ingest.Demandbase.Pipelines
{
    public class GetFacetMemberValues
    {
        public virtual void Process(GetFacetMemberValuesArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNullOrEmpty(args.FacetName, "args.FacetName");

            var type = ContactFacetHelper.GetContractTypeForFacet(args.FacetName);

            if (type == null
            || !typeof(IDemandbaseData).IsAssignableFrom(type))
            {
                return;
            }

            var taxonomy = Factory.CreateObject("demandbaseTaxonomy", true) as DemandbaseTaxonomy;
            if (taxonomy == null
            || (!taxonomy.MemberValues.ContainsKey(args.MemberName)))
            {
                return;
            }

            taxonomy
                .MemberValues[args.MemberName]
                .ForEach( m => args.Values.Add(m.Description, m.Value));

        }

    }
}