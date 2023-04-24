using MySql.Data.MySqlClient;
using System.Globalization;

namespace I_FOX_V1.Models
{
    public class Arquivo
    {
        //Atributos
        private int codigo;
        private byte[] arrayBytes;
        private string resumo;

        static MySqlConnection conexao = FabricaConexao.getConexao();

        //construtor
        public Arquivo(byte[] arrayBytes, string resumo)
        {
            this.arrayBytes = arrayBytes;
            this.resumo = resumo;
        }

        public Arquivo(int codigo, byte[] arrayBytes, string resumo)
        {
            this.codigo = codigo;
            this.arrayBytes = arrayBytes;
            this.resumo = resumo;
        }

        //getter e setters
        public int Codigo { get => codigo; set => codigo = value; }
        public byte[] ArrayBytes { get => arrayBytes; set => arrayBytes = value; }
        public string Resumo { get => resumo; set => resumo = value; }
        
        //métodos
        public string cadastrar(byte[] valor, string titulo, string cod_caderno)
        {
            string situacao_arquivo = "";
            string codigoResumo = "";

            //Pesquisando em que resumo vão estar aqueles arquivos
            try
            {
                conexao.Open(); //Abrindo conexão
                MySqlCommand pesquisaResumo = new MySqlCommand("SELECT * FROM RESUMO where @titulo = titulo and FK_CADERNO_codigo = @cod_caderno", conexao);
                MySqlCommand cadastrarArquivo = new MySqlCommand("INSERT INTO ARQUIVO(valor, FK_RESUMO_codigo) VALUES(@valor, @cod_resumo)", conexao);
                pesquisaResumo.Parameters.AddWithValue("@titulo", titulo);
                pesquisaResumo.Parameters.AddWithValue("@cod_caderno", cod_caderno);

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
                         leitorBanco["FK_RESUMO_codigo"].ToString());

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

    }
}
