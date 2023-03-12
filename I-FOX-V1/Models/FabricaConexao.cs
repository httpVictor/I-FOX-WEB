using MySql.Data.MySqlClient;

namespace I_FOX_V1.Models
{
    public class FabricaConexao
    {
        public static MySqlConnection getConexao()
        {
            return new MySqlConnection(
                Configuration().GetConnectionString("casaNepo"));

            //CASA- "Server=localhost;Database=ifoxteste;User id=root;Password=Euamo.Netiflix"
            //Escola -  "Server=ESN509VMYSQL;Database=ifoxteste;User id=aluno;Password=Senai1234"
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
