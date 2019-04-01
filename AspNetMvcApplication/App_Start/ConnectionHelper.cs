using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using System;
using System.Configuration;

namespace AspNetMvcApplication {
    public static class ConnectionHelper {

        static readonly Type[] PersistentTypes = new Type[]{
            typeof(PersistentTypes.Order),
            typeof(PersistentTypes.Customer)
        };

        public static void Connect() {
            XpoDefault.DataLayer = CreateDataLayer();
        }

        static IDataLayer CreateDataLayer() {
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //connStr = XpoDefault.GetConnectionPoolString(connStr);  // Uncomment this line if you use a network database like MSSQL, Oracle, PostgreSql etc.
            ReflectionDictionary dictionary = new ReflectionDictionary();
            dictionary.GetDataStoreSchema(PersistentTypes);   // Pass all of your persistent object types to this method.
            AutoCreateOption autoCreateOption = AutoCreateOption.DatabaseAndSchema;  // Use AutoCreateOption.DatabaseAndSchema if the database or tables does not exists. Use AutoCreateOption.SchemaAlreadyExists if the database already exists.
            IDataStore provider = XpoDefault.GetConnectionProvider(connStr, autoCreateOption);
            return new ThreadSafeDataLayer(dictionary, provider);
        }
    }
}