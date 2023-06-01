using MySql.Data.MySqlClient;
using System.Globalization;

namespace I_FOX_V1.Models
{
    public class Arquivo
    {
        //Atributos
        private int codigo, codCaderno, codResumo;
        private byte[] arrayBytes;
        private string tituloResumo;

        static MySqlConnection conexao = FabricaConexao.getConexao();

        //Construtores
        public Arquivo(byte[] arrayBytes, string tituloResumo, int codCaderno, int codResumo)
        {
            this.arrayBytes = arrayBytes;
            this.tituloResumo = tituloResumo;
            this.codCaderno = codCaderno;
            this.codResumo = codResumo;
        }

        //Trazer ele do banco
        public Arquivo(int codigo, byte[] arrayBytes, int codigoResumo)
        {
            this.codigo = codigo;
            this.arrayBytes = arrayBytes;
            this.codResumo = codigoResumo;
        }

        //getter e setters
        public int Codigo { get => codigo; set => codigo = value; }
        public byte[] ArrayBytes { get => arrayBytes; set => arrayBytes = value; }
        public string TituloResumo { get => tituloResumo; set => tituloResumo = value; }
        public int CodCaderno { get => codCaderno; set => codCaderno = value; }
        public int CodResumo { get => codResumo; set => codResumo = value; }

        //Método para cadastrar arquivos
        public string cadastrar()
        {
            string situacao_arquivo = "";

            //Pesquisando em que resumo vão estar aqueles arquivos
            try
            {
                MySqlCommand cadastrarArquivo = new MySqlCommand("INSERT INTO ARQUIVO(valor, FK_RESUMO_codigo) VALUES(@valor, @cod_resumo)", conexao);
                
                conexao.Open();
                cadastrarArquivo.Parameters.AddWithValue("@cod_resumo", CodResumo);
                cadastrarArquivo.Parameters.AddWithValue("@valor", arrayBytes);
                cadastrarArquivo.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                situacao_arquivo = "Erro! " + e;
            }
            finally
            {
                conexao.Close();
            }

            return situacao_arquivo;
        }

        //Método para listar arquivos
        static public List<Arquivo> listar(string id_resumo)
        {
            List<Arquivo> listaArquivo= new List<Arquivo>();
            try
            {
                conexao.Open(); //Abrindo conexão
                MySqlCommand pesquisa = new MySqlCommand("SELECT * FROM ARQUIVO where FK_RESUMO_codigo = @id", conexao);
                pesquisa.Parameters.AddWithValue("@id", id_resumo) ;

                //Quando se executa uma pesquisa, tem como retorno as linhas de uma tabela que são guardadas em um leitor
                MySqlDataReader leitorBanco = pesquisa.ExecuteReader();

                while (leitorBanco.Read()) //Enquanto for possível ler ele
                {
                    //Definindo os atributos que vão vir do banco
                    Arquivo arquivo = new Arquivo(
                        int.Parse(leitorBanco["codigo"].ToString()),
                         (byte[])leitorBanco["valor"],
                        int.Parse(leitorBanco["FK_RESUMO_codigo"].ToString()));

                    listaArquivo.Add(arquivo);
                }
            }
            catch (Exception e)
            {

            }
            finally
            {
                conexao.Close();
            }
            return listaArquivo;
        }

        static public string deletarArquivos(int id_arquivo)
        {
            //variável que vai armazenar se o cadastro foi ou não realizado.
            string situacao_deletar = "";

            try //tente efetuar a conexão, caso dê algum erro cair no catch
            {
                //abrir a conexão 
                conexao.Open();

                //criando o comando de delete
                MySqlCommand deletar = new MySqlCommand("DELETE FROM ARQUIVO WHERE FK_RESUMO_codigo = @codigo", conexao);

                //Passando os valores para os parâmetros para evitar INJEÇÃO DE SQL
                deletar.Parameters.AddWithValue("@codigo", id_arquivo);

                //Executando o comando
                deletar.ExecuteNonQuery(); //É um delete, logo não é necessário uma pesquisa (query)!
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

        //Método para deletar TODOS arquivos de determinado resumo
        static public string deletarAquivo(int id_arquivo)
        {
            //variável que vai armazenar se o cadastro foi ou não realizado.
            string situacao_deletar = "";

            try //tente efetuar a conexão, caso dê algum erro cair no catch
            {
                //abrir a conexão 
                conexao.Open();

                //criando o comando de delete
                MySqlCommand deletar = new MySqlCommand("DELETE FROM ARQUIVO WHERE codigo = @codigo", conexao);

                //Passando os valores para os parâmetros para evitar INJEÇÃO DE SQL
                deletar.Parameters.AddWithValue("@codigo", id_arquivo);

                //Executando o comando
                deletar.ExecuteNonQuery(); //É um delete, logo não é necessário uma pesquisa (query)!
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

        //buscar o caderno no qual aquele arquivo está ligado
        static public int buscarResumo(string tituloRes, int codCaderno)
        {
            int codigoResumo = 0;
            try
            {
                conexao.Open(); //Abrindo conexão

                //Comandos SQL
                MySqlCommand pesquisaResumo = new MySqlCommand("SELECT * FROM RESUMO where titulo = @titulo and FK_CADERNO_codigo = @cod_caderno", conexao);
                pesquisaResumo.Parameters.AddWithValue("@titulo", tituloRes);
                pesquisaResumo.Parameters.AddWithValue("@cod_caderno", codCaderno);

                //Quando se executa uma pesquisa, tem como retorno as linhas de uma tabela que são guardadas em um leitor
                MySqlDataReader leitorBanco = pesquisaResumo.ExecuteReader();

                while (leitorBanco.Read()) //Enquanto for possível ler ele
                {
                    //Definindo os atributos que vão vir do banco
                    codigoResumo = int.Parse(leitorBanco["codigo"].ToString());
                }
            }
            catch (Exception e)
            {
                string erro = "" + e;
            }
            finally
            {
                conexao.Close();
            }

            return codigoResumo;
        }
    }
}
