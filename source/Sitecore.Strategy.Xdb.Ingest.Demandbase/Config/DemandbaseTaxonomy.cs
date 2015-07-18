using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using Sitecore.Xml;

namespace Sitecore.Strategy.Xdb.Ingest.Demandbase.Config
{
    public class DemandbaseTaxonomy
    {

        public class FacetMemberValue
        {
            public string Description { get; set; }
            public string Value { get; set; }
        }
        
        public DemandbaseTaxonomy()
        {
            MemberValues = new Dictionary<string, List<FacetMemberValue>>();
        }


        public Dictionary<string, List<FacetMemberValue>> MemberValues { get; private set; }

        public void AddItemName(XmlNode node)
        {
            var facetMemberName = XmlUtil.GetAttribute("name", node);
            if (string.IsNullOrEmpty(facetMemberName))
            {
                return;
            }

            var facetMemberList = new List<FacetMemberValue>();

            foreach (XmlNode childNode in node.ChildNodes)
            {
                var desc = XmlUtil.GetAttribute("description", childNode);
                var value = XmlUtil.GetAttribute("value", childNode);

                if (string.IsNullOrWhiteSpace(desc)) desc=value;
                if (string.IsNullOrWhiteSpace(value)) value = desc;

                if (!string.IsNullOrWhiteSpace(value) 
                &&  !string.IsNullOrWhiteSpace(value))
                {
                    facetMemberList.Add(new FacetMemberValue() {Description = desc, Value = value});
                }
            }

            MemberValues.Add(facetMemberName,facetMemberList);

        }
    }
}