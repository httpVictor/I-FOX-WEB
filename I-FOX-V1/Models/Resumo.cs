using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;

namespace I_FOX_V1.Models
{
    public class Resumo
    {

        //atributos
        private int codigo;
        private string tipo, titulo, texto, frente, verso, data_resumo;
        private int caderno;

        static MySqlConnection conexao = FabricaConexao.getConexao();

        //CONSTRUTOR VAZIO
        public Resumo()
        {
        }
        //CONSTRUTOR PARA CADASTRO DE RESUMO
        public Resumo(string data_resumo, string tipo, string titulo, string texto, int caderno)
        {
            this.data_resumo = data_resumo;
            this.tipo = tipo;
            this.titulo = titulo;
            this.texto = texto;
            this.caderno = caderno;
        }
        //CONSTRUTOR PARA LISTAGEM DE RESUMOS
        public Resumo(int codigo, string data_resumo, string tipo, string titulo, int caderno)
        {
            this.codigo = codigo;
            this.data_resumo = data_resumo;
            this.tipo = tipo;
            this.titulo = titulo;
            this.caderno = caderno;
        }

        public Resumo(int codigo, string data_resumo, string tipo, string titulo, string texto, int caderno)
        {
            this.codigo = codigo;
            this.data_resumo = data_resumo;
            this.tipo = tipo;
            this.titulo = titulo;
            this.texto = texto;
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
        public int Caderno { get => caderno; set => caderno = value; }


        //MÉTODOS CRUD
        public string cadastrarResumo()
        {
            //variável que vai armazenar se o cadastro foi ou não realizado.
            string situacao_cadastro = "";

            try //tente efetuar a conexão, caso dê algum erro cair no catch
            {
                //abrir a conexão 
                conexao.Open();

                //criando o comando de insert
                MySqlCommand inserir = new MySqlCommand("INSERT INTO RESUMO(tipo, data_resumo, titulo, FK_CADERNO_codigo, texto) VALUES(@tipo, @data_resumo, @titulo, @FK_CADERNO_codigo, @texto)", conexao);

                //Passando os valores para os parâmetros para evitar INJEÇÃO DE SQL
                inserir.Parameters.AddWithValue("@tipo", Tipo);
                inserir.Parameters.AddWithValue("@data_resumo", Data_resumo);
                inserir.Parameters.AddWithValue("@titulo", Titulo);
                inserir.Parameters.AddWithValue("@texto", Texto);
                inserir.Parameters.AddWithValue("@FK_CADERNO_codigo", Caderno);

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

            return situacao_cadastro;

        }

        public static string editarResumo(string titulo, string texto, int codigo)
        {
            string sit_update = "";
            try
            {
                    conexao.Open();

                    //CRIANDO COMANDO DE INSERIR USUÁRIOS NO BANCO DE DADOS
                    MySqlCommand inserir = new MySqlCommand("UPDATE RESUMO SET titulo = @titulo, texto = @texto WHERE codigo = @codigo", conexao);
                    inserir.Parameters.AddWithValue("@titulo", titulo);
                    inserir.Parameters.AddWithValue("@texto", texto);
                    inserir.Parameters.AddWithValue("@codigo", codigo);
                    
                    inserir.ExecuteNonQuery();
                    sit_update = "Atualizado com sucesso";
                    

            }
            catch (Exception e)
            {
                sit_update = "Erro de conexão" + e;
            }
            finally
            {
                conexao.Close();
            }
            return sit_update;
        }

        static public string deletarResumo(int id_resumo)
        {
            //variável que vai armazenar se o cadastro foi ou não realizado.
            string situacao_deletar = "";

            //Apagando primeiro arquivos presentes em tabelas que se ligam em resumos

            //Apagando os cards presentes nesse resumo

            try //tente efetuar a conexão, caso dê algum erro cair no catch
            {
                //abrir a conexão 
                conexao.Open();

                //criando o comando de insert
                MySqlCommand inserir = new MySqlCommand("DELETE FROM RESUMO WHERE codigo = @codigo", conexao);

                //Passando os valores para os parâmetros para evitar INJEÇÃO DE SQL
                inserir.Parameters.AddWithValue("@codigo", id_resumo);

                //Executando o comando
                inserir.ExecuteNonQuery(); //É um insert, logo não é necessário uma pesquisa (query)!
                situacao_deletar = "deletado com sucesso";

            }
            catch (Exception e) //o e armazena o tipo de erro que aconteceu!
            {
                situacao_deletar = "Erro de conexão!" + e;
            }
            finally
            {
                conexao.Close(); //Fechando a conexão, dando certo, ou não!
            }

            return situacao_deletar;
        }
   
        //LISTAR
        //Método para listagem de resumos de um usuário
        static public List<Resumo> listarResumo(int id_caderno)
        {
            List<Resumo> listaResumo = new List<Resumo>();
            try
            {
                conexao.Open(); //Abrindo conexão
                MySqlCommand pesquisaResumo = new MySqlCommand("SELECT * FROM RESUMO where FK_CADERNO_codigo = @id", conexao);
                pesquisaResumo.Parameters.AddWithValue("@id", id_caderno);

                //Quando se executa uma pesquisa, tem como retorno as linhas de uma tabela que são guardadas em um leitor
                MySqlDataReader leitorBanco = pesquisaResumo.ExecuteReader();

                while (leitorBanco.Read()) //Enquanto for possível ler ele
                {
                    //Definindo os atributos que vão vir do banco

                    listaResumo.Add(new Resumo(
                        int.Parse(leitorBanco["codigo"].ToString()),
                        ((DateTime) leitorBanco["data_resumo"]).Date.ToString(),
                        (string)leitorBanco["tipo"],
                        (string)leitorBanco["titulo"],
                        int.Parse(leitorBanco["FK_CADERNO_codigo"].ToString())
                        ));
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conexao.Close();
            }
            return listaResumo;
        }

        //Método de busca de um resumo específico do usuário
        static public Resumo acessarResumo(int id_resumo)
        {
            Resumo resumo = new Resumo();
            try
            {
                conexao.Open(); //Abrindo conexão
                MySqlCommand pesquisaResumo = new MySqlCommand("SELECT * FROM RESUMO where codigo = @id", conexao);
                pesquisaResumo.Parameters.AddWithValue("@id", id_resumo);

                //Quando se executa uma pesquisa, tem como retorno as linhas de uma tabela que são guardadas em um leitor
                MySqlDataReader leitorBanco = pesquisaResumo.ExecuteReader();

                while (leitorBanco.Read()) //Enquanto for possível ler ele
                {
                    //Definindo os atributos que vão vir do banco
                     resumo = new Resumo(
                        int.Parse(leitorBanco["codigo"].ToString()),
                        Convert.ToDateTime(leitorBanco["data_resumo"].ToString()).ToString("dd/MM/yyyy"),
                        (string)leitorBanco["tipo"],
                        (string)leitorBanco["titulo"],
                        (string)leitorBanco["texto"],
                        int.Parse(leitorBanco["FK_CADERNO_codigo"].ToString()));
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conexao.Close();
            }
            return resumo;
        }

        //MÉTODOS 
        public bool existeResumo()
        {
            return true;
        }
    }
}
