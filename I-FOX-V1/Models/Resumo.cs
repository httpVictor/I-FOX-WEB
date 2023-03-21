using MySql.Data.MySqlClient;

namespace I_FOX_V1.Models
{
    public class Resumo
    {

        //atributos
        private int codigo;
        private string tipo, titulo, texto, frente, verso, data_resumo;
        private int caderno;

        static MySqlConnection conexao = FabricaConexao.getConexao();

        //construtor
        public Resumo(string data_resumo, string tipo, string titulo, string texto, string frente, string verso, int caderno)
        {
            this.data_resumo = data_resumo;
            this.tipo = tipo;
            this.titulo = titulo;
            this.texto = texto;
            this.frente = frente;
            this.verso = verso;
            this.caderno = caderno;
        }

        //getters e setters

        public int Codigo { get => codigo; }
        public string Data_resumo { get => data_resumo; set => data_resumo = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Texto { get => texto; set => texto = value; }
        public string Frente { get => frente; set => frente = value; }
        public string Verso { get => verso; set => verso = value; }
        public int Caderno { get => codigo; set => codigo = value; }


        //métodos

        public string cadastrarResumo(string tipo)
        {
            //variável que vai armazenar se o cadastro foi ou não realizado.
            string situacao_cadastro = "";

            try //tente efetuar a conexão, caso dê algum erro cair no catch
            {
                //abrir a conexão (
                conexao.Open();

                //criando o comando de insert
                MySqlCommand inserir = new MySqlCommand("INSERT INTO RESUMO VALUES(@tipo, @data_resumo, @titulo, @texto, @frente, @verso)", conexao);
                MySqlCommand inserirArquivos = new MySqlCommand("INSERT INTO ARQUIVO VALUES()");

                //Passando os valores para os parâmetros para evitar INJEÇÃO DE SQL
                inserir.Parameters.AddWithValue("@tipo", tipo);
                inserir.Parameters.AddWithValue("@data_resumo", Data_resumo);
                inserir.Parameters.AddWithValue("@titulo", Titulo);
                if (Texto != null)
                {
                    inserir.Parameters.AddWithValue("@texto", Texto);
                }
                else
                {
                    inserir.Parameters.AddWithValue("@texto", " ");
                }
                inserir.Parameters.AddWithValue("@frente", " ");
                inserir.Parameters.AddWithValue("@verso", " ");

                //Executando o comando
                inserir.ExecuteNonQuery(); //É um insert, logo não é necessário uma pesquisa (query)!
                situacao_cadastro = "cadastrado";

            }
            catch (Exception e) //o e armazena o tipo de erro que aconteceu!
            {
                situacao_cadastro = "Erro de conexão!" + e;
            }
            finally
            {
                conexao.Close(); //Fechando a conexão, dando certo, ou não!
            }

            return "Resumo cadastrado com sucesso!";

        }
        public string editarResumo()
        {
            return "Resumo editado com sucesso!";
        }

        public string deletarResumo()
        {
            return "Resumo deletado com sucesso!";
        }

        static public string listarResumo()
        {
            return "Resumo listado com sucesso!";
        }
    }
}
