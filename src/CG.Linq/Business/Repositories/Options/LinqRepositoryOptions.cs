using CG.Properties;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;

namespace CG.Business.Repositories.Options
{
    /// <summary>
    /// This class represents configuration options for a LINQ based
    /// repository.
    /// </summary>
    public class LinqRepositoryOptions : RepositoryOptions
    {
        // *******************************************************************
        // Properties.
        // *******************************************************************

        #region Properties

        /// <summary>
        /// This property contains the connection string for the LINQ connection.
        /// </summary>
        [Required(ErrorMessageResourceName = "LinqRepositoryOptions_CS",
                  ErrorMessageResourceType = typeof(Resources))]
        public string ConnectionString { get; set; }

        /// <summary>
        /// This property contains the name of the associated database, as parsed
        /// from the <see cref="ConnectionString"/> property.
        /// </summary>
        public string DatabaseName
        {
            get
            {
                var parser = new DbConnectionStringBuilder();
                parser.ConnectionString = ConnectionString;
                if (false == parser.TryGetValue("database", out var databaseName))
                {
                    databaseName = "unknown";
                }
                return $"{databaseName}";
            }
        }

        /// <summary>
        /// This property contains the name of the associated server, as parsed
        /// from the <see cref="ConnectionString"/> property.
        /// </summary>
        public string ServerName
        {
            get
            {
                var parser = new DbConnectionStringBuilder();
                parser.ConnectionString = ConnectionString;
                if (false == parser.TryGetValue("server", out var serverName))
                {
                    serverName = "unknown";
                }
                return $"{serverName}";
            }
        }

        /// <summary>
        /// This property indicated whether the LINQ connection is trusted, as parsed
        /// from the <see cref="ConnectionString"/> property.
        /// </summary>
        public bool TrustedConnection
        {
            get
            {
                var parser = new DbConnectionStringBuilder();
                parser.ConnectionString = ConnectionString;
                if (false == parser.TryGetValue("server", out var flag))
                {
                    flag = "False";
                }
                return bool.Parse($"{flag}");
            }
        }

        /// <summary>
        /// This property indicated whether the LINQ connection supports multiple active
        /// result sets, as parsed from the <see cref="ConnectionString"/> property.
        /// </summary>
        public bool MultipleActiveResultSets
        {
            get
            {
                var parser = new DbConnectionStringBuilder();
                parser.ConnectionString = ConnectionString;
                if (false == parser.TryGetValue("multipleactiveresultsets", out var flag))
                {
                    flag = "False";
                }
                return bool.Parse($"{flag}");
            }
        }

        #endregion
    }
}
