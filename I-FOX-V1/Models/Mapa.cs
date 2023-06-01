using MySql.Data.MySqlClient;

namespace I_FOX_V1.Models
{
    public class Mapa
    {
        private string usuario;
        private int fases, missoes;
        static MySqlConnection conexao = FabricaConexao.getConexao();






        //Método para verificar as missões
        public void conferirMissoes()
        {

        }

        //Método para contagem de cadernos
        public int contaCaderno(string nome)
        {
            int qntdCadernos = 0;
            try
            {
                conexao.Open();
                MySqlCommand select = new MySqlCommand("SELECT * from CADERNO where FK_USUARIO_nome = @usuario", conexao);
                select.Parameters.AddWithValue("@usuario", nome);
                MySqlDataReader leitor = select.ExecuteReader();

                while (leitor.Read())
                {
                    qntdCadernos++;
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexao.Close();
            }

            return qntdCadernos;
        }

        //Método para contagem de resumos
        public int contaResumos(string nome)
        {
            int qntdResumos = 0;
            try
            {
                conexao.Open();
                MySqlCommand select = new MySqlCommand("SELECT * from CADERNO where FK_USUARIO_nome = @usuario", conexao);
                select.Parameters.AddWithValue("@usuario", nome);
                MySqlDataReader leitor = select.ExecuteReader();

                while (leitor.Read())
                {
                    qntdResumos++;
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conexao.Close();
            }

            return qntdResumos;
        }
        //Método para contagem de flashcards

        //Método para contagem de resumos escritos

        //Método para contagem de pomodoros
    }
}