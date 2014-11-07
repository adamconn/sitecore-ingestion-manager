using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Analytics.Tracking;
using Sitecore.Analytics.Web;

namespace Sitecore.Strategy.Xdb.Ingest.Pipelines
{
    public class EnsureContactIsLoaded
    {
        public virtual void Process(IngestionPipelineArgs args)
        {
            if (args.Contact != null)
            {
                return;
            }
            if (Sitecore.Analytics.Tracker.Current == null)
            {
                Sitecore.Analytics.Tracker.Initialize();
            }
            var cookie = new ContactKeyCookie();
            if (!cookie.IsNewContact)
            {
                var contactManager = Sitecore.Configuration.Factory.CreateObject("tracking/contactManager", true) as ContactManager;
                if (contactManager != null)
                {
                    args.Contact = contactManager.LoadContactReadOnly(cookie.ContactId);
                }
            }

        }
    }
}