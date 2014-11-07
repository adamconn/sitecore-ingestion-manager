using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Analytics.Model.Framework;

namespace Sitecore.Strategy.Xdb.Ingest.Bizo.Models
{
    public interface IBizoData : IFacet
    {
        string CompanySize { get; set; }
        ICollection<string> FunctionalAreas { get; set; }
        string Gender { get; set; }
        ICollection<string> Groups { get; set; }
        ICollection<string> Industries { get; set; }
        string Location { get; set; }
        string Seniority { get; set; }
    }
}
