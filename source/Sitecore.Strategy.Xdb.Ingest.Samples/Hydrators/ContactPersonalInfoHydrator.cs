using System;
using System.IO;
using System.Linq;
using System.Web;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Strategy.Xdb.Ingest.Hydrators;

namespace Sitecore.Strategy.Xdb.Ingest.Samples.Hydrators
{
    public class ContactPersonalInfoHydrator : BaseHydratorFromNameValueCollection
    {
        public ContactPersonalInfoHydrator(object target) : base(target)
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
            if (!(this.Target is IContactPersonalInfo))
            {
                throw new NotSupportedException("target");
            }
            var info = (IContactPersonalInfo)this.Target;
            SetPrimitiveProperty<IContactPersonalInfo, DateTime?>("BirthDate", info, "dob", collection, applyNullValues);
            SetPrimitiveProperty<IContactPersonalInfo, string>("FirstName", info, "first", collection, applyNullValues);
            SetPrimitiveProperty<IContactPersonalInfo, string>("Gender", info, "gender", collection, applyNullValues);
            SetPrimitiveProperty<IContactPersonalInfo, string>("JobTitle", info, "job", collection, applyNullValues);
            SetPrimitiveProperty<IContactPersonalInfo, string>("MiddleName", info, "middle", collection, applyNullValues);
            SetPrimitiveProperty<IContactPersonalInfo, string>("Nickname", info, "nickname", collection, applyNullValues);
            SetPrimitiveProperty<IContactPersonalInfo, string>("Suffix", info, "suffix", collection, applyNullValues);
            SetPrimitiveProperty<IContactPersonalInfo, string>("Surname", info, "last", collection, applyNullValues);
            SetPrimitiveProperty<IContactPersonalInfo, string>("Title", info, "title", collection, applyNullValues);
        }
    }
}