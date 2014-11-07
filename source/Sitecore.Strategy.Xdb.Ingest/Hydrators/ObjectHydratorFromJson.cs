using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using Sitecore.Diagnostics;

namespace Sitecore.Strategy.Xdb.Ingest.Hydrators
{
    public class ObjectHydratorFromJson : IHydrator
    {
        public ObjectHydratorFromJson(object target)
        {
            this.Target = target;
        }
        public object Target { get; private set; }

        public void Hydrate(Stream source, bool applyNullValues = false)
        {
            var data = new StreamReader(source, Encoding.UTF8).ReadToEnd();
            source.Close();
            if (string.IsNullOrEmpty(data))
            {
                Log.Warn("No data could be read from the stream. This may not indicate an error.", this);
                return;
            }
            var settings = new JsonSerializerSettings();
            settings.ObjectCreationHandling = ObjectCreationHandling.Reuse;
            settings.NullValueHandling = NullValueHandling.Ignore;
            JsonConvert.PopulateObject(data, this.Target, settings);
        }
    }
}