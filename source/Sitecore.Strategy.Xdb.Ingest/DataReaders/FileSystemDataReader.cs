using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Sitecore.Diagnostics;

namespace Sitecore.Strategy.Xdb.Ingest.DataReaders
{
    public class FileSystemDataReader : IDataReader
    {
        public string FilePath { get; set; }

        public virtual Stream GetDataStream()
        {
            if (string.IsNullOrEmpty(this.FilePath))
            {
                Log.SingleWarn("No file was specified so cannot read data", this);
            }
            if (!File.Exists(this.FilePath))
            {
                Log.SingleWarn(string.Format("The file {0} does not exist so cannot read data", this.FilePath), this);
                return null;
            }
            Log.Debug(string.Format("Getting stream from the file {0}", this.FilePath), this);
            return File.OpenRead(this.FilePath);
        }
    }
}