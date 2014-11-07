using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Managers;
using Sitecore.Diagnostics;
using Sitecore.Strategy.Contacts.Items;
using Sitecore.Strategy.Xdb.Ingest.Hydrators;

namespace Sitecore.Strategy.Xdb.Ingest.Items.Hydrators
{
    public class ContactFacetHydratorItem : CustomItem
    {
        public ContactFacetHydratorItem(Item innerItem) : base(innerItem)
        {
        }
        public static implicit operator ContactFacetHydratorItem(Item item)
        {
            if (item == null) { return null; }
            var template = TemplateManager.GetTemplate(item.TemplateID, item.Database);
            if (!template.InheritsFrom(IngestionTemplateIDs.ContactFacetHydrator)) { return null; }
            return new ContactFacetHydratorItem(item);
        }
        public ContactFacetNameItem ContactFacet
        {
            get
            {
                ContactFacetNameItem item = this.Database.GetItem(base[ContactFacetHydratorFieldIDs.ContactFacet]);
                return item;
            }
        }

        public Type HydratorType
        {
            get
            {
                var typeName = base[ContactFacetHydratorFieldIDs.DehydratorType];
                if (string.IsNullOrEmpty(typeName))
                {
                    return null;
                }
                return Type.GetType(typeName);
            }
        }

        public virtual IHydrator GetHydrator()
        {
            var type = this.HydratorType;
            if (type == null)
            {
                Log.SingleError(string.Format("Could not determine the hydrator type for the item {0}", this.InnerItem.ID.ToString()), this);
                return null;
            }
            var cfItem = this.ContactFacet;
            var contact = Sitecore.Analytics.Tracker.Current.Contact;
            if (contact == null)
            {
                Log.Error("No contact is available for this session so nothing can be hydrated", this);
                return null;
            }
            var method1 = contact.GetType().GetMethod("GetFacet");
            var method2 = method1.MakeGenericMethod(cfItem.ContractType);
            var contactFacet = method2.Invoke(contact, new object[]{cfItem.FacetName});
            //
            //
            var hydrator = Activator.CreateInstance(type, new object[] { contactFacet }) as IHydrator;
            if (hydrator == null)
            {
                Log.SingleError(string.Format("The type {0} is not a {1}", type.FullName, typeof(IHydrator).FullName), this);
                return null;
            }
            return hydrator;
        }
    }
}