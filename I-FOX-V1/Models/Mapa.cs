using MySql.Data.MySqlClient;

namespace I_FOX_V1.Models
{
    public class Mapa
    {
        private string usuario;
        private int fases, missoes;
        static MySqlConnection conexao = FabricaConexao.getConexao();


        //Método para verificar as missões
        public void conferirMissoes(string nome)
        {
            int missoes = 0;
            if (missao1(nome))
            {
                missoes = 1;
                if (missao2(nome))
                {
                    missoes = 2;
                    if (missao3(nome))
                    {
                        missoes = 3;
                        if (missao4(nome))
                        {
                            missoes = 4;
                            if (missao5(nome))
                            {
                                missoes = 5;
                                if (missao6(nome))
                                {
                                    missoes = 6;
                                }
                            }
                        }
                    }
                }
            }

            //Fazendo o update com o número de missões concluídas
            try
            {
                conexao.Open();
                MySqlCommand comando = new MySqlCommand("UPDATE USUARIO SET missoes = @missoes where nome = @nome", conexao);
                comando.Parameters.AddWithValue("@missoes", missoes);
                comando.Parameters.AddWithValue("@nome", nome);

                comando.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally { conexao.Close(); }
            
        }

        public bool missao1(string nome)
        {
            bool statusMissao = false;
            if(contaCaderno(nome) >= 1)
            {
                statusMissao= true;
            }
            return statusMissao;    
        }
        
        public bool missao2(string nome)
        {
            bool statusMissao = false;
            if(contaCaderno(nome) >= 1)
            {
                statusMissao= true;
            }
            return statusMissao;    
        }

        public bool missao3(string nome)
        {
            bool statusMissao = false;
            if (contaCaderno(nome) >= 1)
            {
                statusMissao = true;
            }
            return statusMissao;
        }

        public bool missao4(string nome)
        {
            bool statusMissao = false;
            if (contaCaderno(nome) >= 1)
            {
                statusMissao = true;
            }
            return statusMissao;
        }

        public bool missao5(string nome)
        {
            bool statusMissao = false;
            if (contaCaderno(nome) >= 1)
            {
                statusMissao = true;
            }
            return statusMissao;
        }

        public bool missao6(string nome)
        {
            bool statusMissao = false;
            if (contaCaderno(nome) >= 1)
            {
                statusMissao = true;
            }
            return statusMissao;
        }

        //MÉTODOS DE CONTAGEM
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
        public int contaFlashCard(string nome)
        {
            return 2;
        }

        //Método para contagem de resumos escritos
        public int contaEscrito()
        {
            return 3;
        }
        //Método para contagem de pomodoros
        public int contaPomos()
        {
            return 4;
        }
    }
}