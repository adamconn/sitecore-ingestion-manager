using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Analytics.Model.Framework;

namespace Sitecore.Strategy.Xdb.Ingest.Bizo.Models
{
    public class BizoData : Facet, IBizoData
    {
        public BizoData()
        {
            base.EnsureAttribute<string>("company_size");
            base.EnsureAttribute<string>("functional_area");
            base.EnsureAttribute<string>("group");
            base.EnsureAttribute<string>("gender");
            base.EnsureAttribute<string>("industry");
            base.EnsureAttribute<string>("location");
            base.EnsureAttribute<string>("seniority");
        }
        public string CompanySize
        {
            get { return base.GetAttribute<string>("company_size"); }
            set { base.SetAttribute("company_size", value); }
        }

        protected virtual ICollection<string> GetAttributeAsCollection(string name)
        {
            var value = base.GetAttribute<string>(name);
            if (value == null)
            {
                return null;
            }
            return value.Split(',');
        }
        protected virtual void SetAttributeAsString(string name, ICollection<string> values)
        {
            string value = null;
            if (values != null && values.Count > 0)
            {
                value = string.Join(",", values);
            }
            base.SetAttribute(name, value);
        }

        public ICollection<string> FunctionalAreas
        {
            get { return GetAttributeAsCollection("functional_area"); }
            set { SetAttributeAsString("functional_area", value); }
        }
        public ICollection<string> Groups
        {
            get { return GetAttributeAsCollection("group"); }
            set { SetAttributeAsString("group", value); }
        }
        public string Gender
        {
            get { return base.GetAttribute<string>("gender"); }
            set { base.SetAttribute("gender", value); }
        }
        public ICollection<string> Industries
        {
            get { return GetAttributeAsCollection("industry"); }
            set { SetAttributeAsString("industry", value); }
        }
        public string Location
        {
            get { return base.GetAttribute<string>("location"); }
            set { base.SetAttribute("location", value); }
        }
        public string Seniority
        {
            get { return base.GetAttribute<string>("seniority"); }
            set { base.SetAttribute("seniority", value); }
        }
    }
}