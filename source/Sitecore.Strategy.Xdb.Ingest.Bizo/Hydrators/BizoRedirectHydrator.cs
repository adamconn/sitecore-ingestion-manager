using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Web;
using Sitecore.Strategy.Xdb.Ingest.Bizo.Models;
using Sitecore.Strategy.Xdb.Ingest.Hydrators;

namespace Sitecore.Strategy.Xdb.Ingest.Bizo.Hydrators
{
    public class BizoRedirectHydrator : BaseHydratorFromNameValueCollection
    {
        public BizoRedirectHydrator(object target) : base(target)
        {
        }

        public override void Hydrate(Stream source, bool applyNullValues = false)
        {
            var collection = GetCollectionFromStream(source);
            source.Close();
            if (collection == null)
            {
                throw new ArgumentOutOfRangeException("source");
            }
            if (this.Target == null)
            {
                throw new NullReferenceException("target");
            }
            if (!(this.Target is IBizoData))
            {
                throw new NotSupportedException("target");
            }
            var info = (IBizoData)this.Target;
            SetPrimitiveProperty<IBizoData, string>("CompanySize", info, "company_size", collection, applyNullValues);
            SetStringCollectionProperty("FunctionalAreas", info, "functional_area", collection, applyNullValues);
            SetPrimitiveProperty<IBizoData, string>("Gender", info, "gender", collection, applyNullValues);
            SetStringCollectionProperty("Groups", info, "group", collection, applyNullValues);
            SetStringCollectionProperty("Industries", info, "industry", collection, applyNullValues);
            SetPrimitiveProperty<IBizoData, string>("Location", info, "location", collection, applyNullValues);
            SetPrimitiveProperty<IBizoData, string>("Seniority", info, "seniority", collection, applyNullValues);
        }

        protected virtual void SetStringCollectionProperty(string propertyName, IBizoData target, string key, NameValueCollection collection, bool applyNullValues)
        {
            var values = collection.GetValues(key);
            if ((values == null || values.Length == 0) && !applyNullValues)
            {
                return;
            }
            var property = target.GetType().GetProperty(propertyName);
            if (property == null)
            {
                return;
            }
            var method = property.GetSetMethod();
            if (method == null)
            {
                return;
            }
            method.Invoke(target, new object[] {values});
        }
    }
}