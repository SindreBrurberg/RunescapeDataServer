using System.Data.SqlClient;

namespace Sql {
    class Connection {
        public static string Username {private get; set;}
        public static string Password {private get; set;}
        public static string DataSource {private get; set;}
        public static string Catalog {private get; set;}
        public static string CS() {
			SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = DataSource; 
                builder.UserID = Username;            
                builder.Password = Password;     
                builder.InitialCatalog = Catalog;
			return builder.ConnectionString;
		}
    }
}