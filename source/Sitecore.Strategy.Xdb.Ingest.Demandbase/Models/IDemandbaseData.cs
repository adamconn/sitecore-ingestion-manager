using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Analytics.Model.Framework;

namespace Sitecore.Strategy.Xdb.Ingest.Demandbase.Models
{
    public interface IDemandbaseData : IFacet
    {
        string RegistryCompanyName { get; }
        string RegistryCity { get; }
        string RegistryState { get; }
        string RegistryZipCode { get; }
        int RegistryAreaCode { get; }
        string RegistryCountry { get; }
        string RegistryCountryCode { get; }
        double RegistryLatitude { get; }
        double RegistryLongitude { get; }
        string CompanyName { get; }
        string DemandbaseSid { get; }
        string MarketingAlias { get; }
        string Industry { get; }
        string SubIndustry { get; }
        uint EmployeeCount { get; }
        bool Isp { get; }
        string PrimarySic { get; }
        string StreetAddress { get; }
        string City { get; }
        string State { get; }
        string Zip { get; }
        string Country { get; }
        string CountryName { get; }
        string Phone { get; }
        string StockTicker { get; }
        string Website { get; }
        ulong AnnualSales { get; }
        string RevenueRange { get; } //taxonomy
        string EmployeeRange { get; } //taxonomy
        bool B2b { get; }
        bool B2c { get; }
        string Traffic { get; } //taxonomy
        double Latitude { get; }
        double Longitude { get; }
        bool Fortune1000 { get; }
        bool Forbes2000 { get; }
        string InformationLevel { get; } //taxonomy
        string Audience { get; } //taxonomy
        string AudienceSegment { get; }
        string Ip { get; }
        string RegistryDmaCode { get; }
        string RegistryCountryCode3 { get; }
        //WatchLists {get;}

    }
}
