using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using Sitecore.Diagnostics;

namespace Sitecore.Strategy.Xdb.Ingest.Hydrators
{
    /// <summary>
    /// Reads a NameValueCollection from the stream and hydrates
    /// the target using values from the NameValueCollection.
    /// The stream will be read as a query string.
    /// </summary>
    public abstract class BaseHydratorFromNameValueCollection : IHydrator
    {
        protected BaseHydratorFromNameValueCollection(object target)
        {
            this.Target = target;
            this.Encoding = Encoding.UTF8;
        }
        public object Target { get; private set; }
        protected Encoding Encoding { get; set; }
        public abstract void Hydrate(Stream source, bool applyNullValues = false);

        protected virtual NameValueCollection GetCollectionFromStream(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            var data = new StreamReader(stream, Encoding.UTF8).ReadToEnd();
            stream.Close();
            return HttpUtility.ParseQueryString(data);
        }

        protected virtual void SetPrimitiveProperty<TInstance, TValue>(string propertyName, TInstance instance, string key, NameValueCollection collection, bool applyNullValues)
        {
            var value = collection[key];
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (value.Split(',').All(v => string.IsNullOrWhiteSpace(v)))
                {
                    value = null;
                }
            }
            if (string.IsNullOrEmpty(value) && ! applyNullValues)
            {
                return;
            }
            var property = typeof(TInstance).GetProperty(propertyName);
            var setter = GetPropertySetter<TInstance, TValue>(property);
            if (setter == null)
            {
                return;
            }
            var converter = TypeDescriptor.GetConverter(typeof(TValue));
            if (!converter.CanConvertFrom(typeof(string)))
            {
                Log.SingleError(string.Format("Cannot convert from {0} to string so the property {1} will not be set", typeof(TValue).FullName, propertyName), this);
                return;
            }
            var value2 = (TValue)converter.ConvertFrom(value);
            setter(instance, value2);
        }
        protected virtual Action<TInstance, TValue> GetPropertySetter<TInstance, TValue>(PropertyInfo property)
        {
            var instance = Expression.Parameter(typeof(TInstance), "instance");
            var argument = Expression.Parameter(typeof(TValue), "value");
            var setMethod = property.GetSetMethod();
            var setterCall = Expression.Call(instance, setMethod, Expression.Convert(argument, typeof(TValue)));
            return (Action<TInstance, TValue>)Expression.Lambda(setterCall, instance, argument).Compile();
        }
    }
}