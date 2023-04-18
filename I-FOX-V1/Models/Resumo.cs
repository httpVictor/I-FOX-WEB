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


        //MÉTODOS
        //CADASTRAR
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

        //Método para cadastrar arquivos (fotos e vídeos) no banco
        public string cadastrarArquivos(byte[] valor)
        {
            string situacao_arquivo = "";
            string codigoResumo = "";

            //Pesquisando em que resumo vão estar aqueles arquivos
            try
            {
                conexao.Open(); //Abrindo conexão
                MySqlCommand pesquisaResumo = new MySqlCommand("SELECT * FROM RESUMO where @titulo = titulo", conexao);
                MySqlCommand cadastrarArquivo = new MySqlCommand("INSERT INTO ARQUIVO(valor, FK_RESUMO_codigo) VALUES(@valor, @cod_resumo)", conexao);
                pesquisaResumo.Parameters.AddWithValue("@titulo", Titulo);

                //Quando se executa uma pesquisa, tem como retorno as linhas de uma tabela que são guardadas em um leitor
                MySqlDataReader leitorBanco = pesquisaResumo.ExecuteReader();

                while (leitorBanco.Read()) //Enquanto for possível ler ele
                {
                    //Definindo os atributos que vão vir do banco
                    codigoResumo = leitorBanco["codigo"].ToString();

                }

                if (codigoResumo != "")
                {
                    conexao.Close();
                    conexao.Open();
                    cadastrarArquivo.Parameters.AddWithValue("@cod_resumo", codigoResumo);
                    cadastrarArquivo.Parameters.AddWithValue("@valor", valor);
                    cadastrarArquivo.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {

            }
            finally
            {
                conexao.Close();
            }

            return situacao_arquivo;
        }

        //Método para cadastrar um card
        public string cadastrarCard(string cod_resumo)
        {
            string sit_cadastro = "";


            try //tente efetuar a conexão, caso dê algum erro cair no catch
            {
                //abrir a conexão 
                conexao.Open();

                //criando o comando de insert
                MySqlCommand inserir = new MySqlCommand("INSERT INTO CARD(frente, verso, FK_RESUMO_codigo) VALUES(@frente, @verso, @FK_Resumo)", conexao);


                //Passando os valores para os parâmetros para evitar INJEÇÃO DE SQL
                inserir.Parameters.AddWithValue("@tipo", Frente);
                inserir.Parameters.AddWithValue("@data_resumo", Verso);
                inserir.Parameters.AddWithValue("@titulo", cod_resumo);
                

                //Executando o comando
                inserir.ExecuteNonQuery(); //É um insert, logo não é necessário uma pesquisa (query)!
                sit_cadastro = "cadastrado";

            }
            catch (Exception e) //o e armazena o tipo de erro que aconteceu!
            {
                sit_cadastro = "Erro de conexão!" + e;
            }
            finally
            {
                conexao.Close(); //Fechando a conexão, dando certo, ou não!
            }

            return sit_cadastro;
        }


        //EDIÇÃO

        //Método para atualizar os dados do resumo 
        public string editarResumo()
        {
            return "Resumo editado com sucesso!";
        }



        //DELETAR
        //Método para deetar um resumo
        static public string deletarResumo(int id_resumo)
        {
            //Apagando primeiro arquivos presentes em tabelas que se ligam em resumos
            deletarAquivo(id_resumo);
            //deletarCards(id_resumo);

            //variável que vai armazenar se o cadastro foi ou não realizado.
            string situacao_deletar = "";

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

        //Método para deletar arquivos
        static public string deletarAquivo(int id_resumo)
        {
            //variável que vai armazenar se o cadastro foi ou não realizado.
            string situacao_deletar = "";

            try //tente efetuar a conexão, caso dê algum erro cair no catch
            {
                //abrir a conexão 
                conexao.Open();

                //criando o comando de insert
                MySqlCommand inserir = new MySqlCommand("DELETE FROM ARQUIVO WHERE FK_RESUMO_codigo = @codigo", conexao);

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
        //Método para deletar cards
        static public string deletarCards(int id_resumo)
        {
            //variável que vai armazenar se o cadastro foi ou não realizado.
            string situacao_deletar = "";

            try //tente efetuar a conexão, caso dê algum erro cair no catch
            {
                //abrir a conexão 
                conexao.Open();

                //criando o comando de insert
                MySqlCommand inserir = new MySqlCommand("DELETE FROM CARD WHERE FK_RESUMO_codigo = @codigo", conexao);

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
                        leitorBanco["data_resumo"].ToString(),
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
                        leitorBanco["data_resumo"].ToString(),
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


    }
}
