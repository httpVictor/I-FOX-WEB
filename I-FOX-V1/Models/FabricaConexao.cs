using MySql.Data.MySqlClient;

namespace I_FOX_V1.Models
{
    public class FabricaConexao
    {
        public static MySqlConnection getConexao()
        {
            return new MySqlConnection(
                Configuration().GetConnectionString("sesi"));

            //Default
            //casaNepo
        }

        private static IConfigurationRoot Configuration()
        {
            IConfigurationBuilder builder =
                new ConfigurationBuilder().SetBasePath(
                    Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            return builder.Build();
        }
    }
}
