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
        public Arquivo(byte[] arrayBytes, string tituloResumo, int codCaderno)
        {
            this.arrayBytes = arrayBytes;
            this.tituloResumo = tituloResumo;
            CodCaderno = codCaderno;    
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

        //MÉTODOS
        public string cadastrar()
        {
            string situacao_arquivo = "";
            string codigoResumo = "";

            //Pesquisando em que resumo vão estar aqueles arquivos
            try
            {
                conexao.Open(); //Abrindo conexão

                //Comandos SQL
                MySqlCommand pesquisaResumo = new MySqlCommand("SELECT * FROM RESUMO where titulo = @titulo and FK_CADERNO_codigo = @cod_caderno", conexao);
                pesquisaResumo.Parameters.AddWithValue("@titulo", TituloResumo);
                pesquisaResumo.Parameters.AddWithValue("@cod_caderno", CodCaderno);

                //Quando se executa uma pesquisa, tem como retorno as linhas de uma tabela que são guardadas em um leitor
                MySqlDataReader leitorBanco = pesquisaResumo.ExecuteReader();

                while (leitorBanco.Read()) //Enquanto for possível ler ele
                {
                    //Definindo os atributos que vão vir do banco
                    codigoResumo = leitorBanco["codigo"].ToString();
                }

                MySqlCommand cadastrarArquivo = new MySqlCommand("INSERT INTO ARQUIVO(valor, FK_RESUMO_codigo) VALUES(@valor, @cod_resumo)", conexao);
                if (codigoResumo != "")
                {
                    conexao.Close();
                    conexao.Open();
                    cadastrarArquivo.Parameters.AddWithValue("@cod_resumo", codigoResumo);
                    cadastrarArquivo.Parameters.AddWithValue("@valor", arrayBytes);
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


    }
}
