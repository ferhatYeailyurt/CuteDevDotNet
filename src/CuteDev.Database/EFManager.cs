/* Author: Volkan ŞENDAĞ - volkansendag@belsis.com.tr - BELSİS ANKARA */
using System;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Reflection;

namespace CuteDev.Database
{
    /// <summary>
    /// EntityFramework ile ilgili islemleri yapar.(volkansendag - 2015.12.25)
    /// </summary>
    public class EFManager
    {
        /// <summary>
        /// EntityFramework ile hazirlanmis EDMX dosyasi ve model bilgilerini belirtir.(volkansendag - 2015.12.25)
        /// </summary>
        private string entModelName = "EntityFramework.Belsis.Ebys.Model";
        private string connStr;
        private DbConnection connection;

        public EFManager(string entModelName, string connStr)
        {
            this.entModelName = entModelName;
            this.connStr = connStr;
        }

        public EFManager(string entModelName, string connStr, DbConnection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Entity model adina uygun olarak meta yolunu getirir.(volkansendag - 2015.12.25)
        /// </summary>
        private string metaPats
        {
            get
            {
                return string.Format("res://*/{0}.csdl|res://*/{0}.ssdl|res://*/{0}.msl", this.entModelName);
            }
        }

        /// <summary>
        /// Entity Connection String olursturur.(volkansendag - 2015.12.25)
        /// </summary>
        private string entityConnStr
        {
            get
            {
                var entityConnectionStringBuilder = new EntityConnectionStringBuilder
                {
                    Provider = "System.Data.SqlClient",
                    ProviderConnectionString = connStr,
                    Metadata = metaPats,
                };

                return entityConnectionStringBuilder.ConnectionString;
            }
        }

        /// <summary>
        /// Metapats kullanarak EntityConnection icin gerekli MetadataWorkspace olusturur.(volkansendag - 2015.12.25)
        /// </summary>
        private MetadataWorkspace metaDataWS
        {
            get
            {
                return new MetadataWorkspace(metaPats.Split('|'), new Assembly[] { Assembly.GetExecutingAssembly() });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public EntityConnection EntityConnection
        {
            get
            {
                if (this.connStr.isEmpty() || this.entModelName.isEmpty())
                    return null;
                else if (this.connection != null)
                    return new EntityConnection(this.metaDataWS, this.connection, true);
                else
                    return new EntityConnection(entityConnStr);
            }
        }
    }
}
