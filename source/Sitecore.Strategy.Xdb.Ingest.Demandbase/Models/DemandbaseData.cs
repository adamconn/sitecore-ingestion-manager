using Newtonsoft.Json;
using Sitecore.Analytics.Model.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Strategy.Xdb.Ingest.Demandbase.Models
{
    public class DemandbaseData : Facet, IDemandbaseData
    {
        public DemandbaseData()
        {
            base.EnsureAttribute<string>("name");
            base.EnsureAttribute<string>("registry_company_name");
            base.EnsureAttribute<string>("registry_city");
            base.EnsureAttribute<string>("registry_state");
            base.EnsureAttribute<string>("registry_zip_code");
            base.EnsureAttribute<int>("registry_area_code");
            base.EnsureAttribute<string>("registry_country");
            base.EnsureAttribute<string>("registry_country_code");
            base.EnsureAttribute<double>("registry_latitude");
            base.EnsureAttribute<double>("registry_longitude");
            base.EnsureAttribute<string>("company_name");
            base.EnsureAttribute<string>("demandbase_sid");
            base.EnsureAttribute<string>("marketing_alias");
            base.EnsureAttribute<string>("industry");
            base.EnsureAttribute<string>("sub_industry");
            base.EnsureAttribute<uint>("employee_count");
            base.EnsureAttribute<bool>("isp");
            base.EnsureAttribute<string>("primary_sic");
            base.EnsureAttribute<string>("street_address");
            base.EnsureAttribute<string>("city");
            base.EnsureAttribute<string>("state");
            base.EnsureAttribute<string>("zip");
            base.EnsureAttribute<string>("country");
            base.EnsureAttribute<string>("country_name");
            base.EnsureAttribute<string>("phone");
            base.EnsureAttribute<string>("stock_ticker");
            base.EnsureAttribute<string>("web_site");
            base.EnsureAttribute<ulong>("annual_sales");
            base.EnsureAttribute<string>("revenue_range");
            base.EnsureAttribute<string>("employee_range");
            base.EnsureAttribute<bool>("b2b");
            base.EnsureAttribute<bool>("b2c");
            base.EnsureAttribute<string>("traffic");
            base.EnsureAttribute<double>("latitude");
            base.EnsureAttribute<double>("longitude");
            base.EnsureAttribute<bool>("fortune_1000");
            base.EnsureAttribute<bool>("forbes_2000");
            base.EnsureAttribute<string>("information_level");
            base.EnsureAttribute<string>("audience");
            base.EnsureAttribute<string>("audience_segment");
            base.EnsureAttribute<string>("ip");
            base.EnsureAttribute<string>("registry_dma_code");
            base.EnsureAttribute<string>("registry_country_code3");
            //base.EnsureAttribute<string>("watch_list");
        }
        public static string Name
        {
            get { return "demandbase"; }
        }

        [JsonProperty("registry_company_name")]
        public string RegistryCompanyName
        {
            get { return base.GetAttribute<string>("registry_company_name"); }
            set { base.SetAttribute("registry_company_name", value); }
        }

        [JsonProperty("registry_city")]
        public string RegistryCity
        {
            get { return base.GetAttribute<string>("registry_city"); }
            set { base.SetAttribute("registry_city", value); }
        }

        [JsonProperty("registry_state")]
        public string RegistryState
        {
            get { return base.GetAttribute<string>("registry_state"); }
            set { base.SetAttribute("registry_state", value); }
        }


        [JsonProperty("registry_zip_code")]
        public string RegistryZipCode
        {
            get { return base.GetAttribute<string>("registry_zip_code"); }
            set { base.SetAttribute("registry_zip_code", value); }
        }


        [JsonProperty("registry_area_code")]
        public int RegistryAreaCode
        {
            get { return base.GetAttribute<int>("registry_area_code"); }
            set { base.SetAttribute("registry_area_code", value); }
        }


        [JsonProperty("registry_country")]
        public string RegistryCountry
        {
            get { return base.GetAttribute<string>("registry_country"); }
            set { base.SetAttribute("registry_country", value); }
        }


        [JsonProperty("registry_country_code")]
        public string RegistryCountryCode
        {
            get { return base.GetAttribute<string>("registry_country_code"); }
            set { base.SetAttribute("registry_country_code", value); }
        }


        [JsonProperty("registry_latitude")]
        public double RegistryLatitude
        {
            get { return base.GetAttribute<double>("registry_latitude"); }
            set { base.SetAttribute("registry_latitude", value); }
        }


        [JsonProperty("registry_longitude")]
        public double RegistryLongitude
        {
            get { return base.GetAttribute<double>("registry_longitude"); }
            set { base.SetAttribute<double>("registry_longitude", value); }
        }


        [JsonProperty("company_name")]
        public string CompanyName
        {
            get { return base.GetAttribute<string>("company_name"); }
            set { base.SetAttribute<string>("company_name", value); }
        }


        [JsonProperty("demandbase_sid")]
        public string DemandbaseSid
        {
            get { return base.GetAttribute<string>("demandbase_sid"); }
            set { base.SetAttribute<string>("demandbase_sid", value); }
        }


        [JsonProperty("marketing_alias")]
        public string MarketingAlias
        {
            get { return base.GetAttribute<string>("marketing_alias"); }
            set { base.SetAttribute<string>("marketing_alias", value); }
        }


        [JsonProperty("industry")]
        public string Industry
        {
            get { return base.GetAttribute<string>("industry"); }
            set { base.SetAttribute<string>("industry", value); }
        }


        [JsonProperty("sub_industry")]
        public string SubIndustry
        {
            get { return base.GetAttribute<string>("sub_industry"); }
            set { base.SetAttribute<string>("sub_industry", value); }
        }


        [JsonProperty("employee_count")]
        public uint EmployeeCount
        {
            get { return base.GetAttribute<uint>("employee_count"); }
            set { base.SetAttribute<uint>("employee_count", value); }
        }


        [JsonProperty("isp")]
        public bool Isp
        {
            get { return base.GetAttribute<bool>("isp"); }
            set { base.SetAttribute<bool>("isp", value); }
        }


        [JsonProperty("primary_sic")]
        public string PrimarySic
        {
            get { return base.GetAttribute<string>("primary_sic"); }
            set { base.SetAttribute<string>("primary_sic", value); }
        }


        [JsonProperty("street_address")]
        public string StreetAddress
        {
            get { return base.GetAttribute<string>("street_address"); }
            set { base.SetAttribute<string>("street_address", value); }
        }


        [JsonProperty("city")]
        public string City
        {
            get { return base.GetAttribute<string>("city"); }
            set { base.SetAttribute<string>("city", value); }
        }


        [JsonProperty("state")]
        public string State
        {
            get { return base.GetAttribute<string>("state"); }
            set { base.SetAttribute<string>("state", value); }
        }


        [JsonProperty("zip")]
        public string Zip
        {
            get { return base.GetAttribute<string>("zip"); }
            set { base.SetAttribute<string>("zip", value); }
        }


        [JsonProperty("country")]
        public string Country
        {
            get { return base.GetAttribute<string>("country"); }
            set { base.SetAttribute<string>("country", value); }
        }


        [JsonProperty("country_name")]
        public string CountryName
        {
            get { return base.GetAttribute<string>("country_name"); }
            set { base.SetAttribute<string>("country_name", value); }
        }


        [JsonProperty("phone")]
        public string Phone
        {
            get { return base.GetAttribute<string>("phone"); }
            set { base.SetAttribute<string>("phone", value); }
        }


        [JsonProperty("stock_ticker")]
        public string StockTicker
        {
            get { return base.GetAttribute<string>("stock_ticker"); }
            set { base.SetAttribute<string>("stock_ticker", value); }
        }


        [JsonProperty("web_site")]
        public string Website
        {
            get { return base.GetAttribute<string>("web_site"); }
            set { base.SetAttribute<string>("web_site", value); }
        }


        [JsonProperty("annual_sales")]
        public ulong AnnualSales
        {
            get { return base.GetAttribute<ulong>("annual_sales"); }
            set { base.SetAttribute<ulong>("annual_sales", value); }
        }


        [JsonProperty("revenue_range")]
        public string RevenueRange
        {
            get { return base.GetAttribute<string>("revenue_range"); }
            set { base.SetAttribute<string>("revenue_range", value); }
        }


        [JsonProperty("employee_range")]
        public string EmployeeRange
        {
            get { return base.GetAttribute<string>("employee_range"); }
            set { base.SetAttribute<string>("employee_range", value); }
        }


        [JsonProperty("b2b")]
        public bool B2b
        {
            get { return base.GetAttribute<bool>("b2b"); }
            set { base.SetAttribute<bool>("b2b", value); }
        }


        [JsonProperty("b2c")]
        public bool B2c
        {
            get { return base.GetAttribute<bool>("b2c"); }
            set { base.SetAttribute<bool>("b2c", value); }
        }


        [JsonProperty("traffic")]
        public string Traffic
        {
            get { return base.GetAttribute<string>("traffic"); }
            set { base.SetAttribute<string>("traffic", value); }
        }


        [JsonProperty("latitude")]
        public double Latitude
        {
            get { return base.GetAttribute<double>("latitude"); }
            set { base.SetAttribute<double>("latitude", value); }
        }


        [JsonProperty("longitude")]
        public double Longitude
        {
            get { return base.GetAttribute<double>("longitude"); }
            set { base.SetAttribute<double>("longitude", value); }
        }


        [JsonProperty("fortune_1000")]
        public bool Fortune1000
        {
            get { return base.GetAttribute<bool>("fortune_1000"); }
            set { base.SetAttribute<bool>("fortune_1000", value); }
        }


        [JsonProperty("forbes_2000")]
        public bool Forbes2000
        {
            get { return base.GetAttribute<bool>("forbes_2000"); }
            set { base.SetAttribute<bool>("forbes_2000", value); }
        }


        [JsonProperty("information_level")]
        public string InformationLevel
        {
            get { return base.GetAttribute<string>("information_level"); }
            set { base.SetAttribute<string>("information_level", value); }
        }


        [JsonProperty("audience")]
        public string Audience
        {
            get { return base.GetAttribute<string>("audience"); }
            set { base.SetAttribute<string>("audience", value); }
        }


        [JsonProperty("audience_segment")]
        public string AudienceSegment
        {
            get { return base.GetAttribute<string>("audience_segment"); }
            set { base.SetAttribute<string>("audience_segment", value); }
        }


        [JsonProperty("ip")]
        public string Ip
        {
            get { return base.GetAttribute<string>("ip"); }
            set { base.SetAttribute<string>("ip", value); }
        }


        [JsonProperty("registry_dma_code")]
        public string RegistryDmaCode
        {
            get { return base.GetAttribute<string>("registry_dma_code"); }
            set { base.SetAttribute<string>("ip", value); }
        }


        [JsonProperty("registry_country_code3")]
        public string RegistryCountryCode3
        {
            get { return base.GetAttribute<string>("registry_country_code3"); }
            set { base.SetAttribute<string>("registry_country_code3", value); }
        }

        //[JsonProperty("watch_list")]
        //public List<KeyValuePair<string, string>> WatchList
        //{
        //    get { return base.GetDictionary<string>()<string>("watch_list"); }
        //    set { base.SetAttribute<string>("watch_list", value); }
        //}
    }
}