using MySql.Data.MySqlClient;

namespace I_FOX_V1.Models
{
    public class Caderno
    {
        private string descricao, titulo, icone;
        private int codigo;
        private string usuario;

        //Conexão com o banco de dados
        static MySqlConnection conexao = FabricaConexao.getConexao();

        public Caderno() { 

        }
       
        public Caderno(string descricao, string titulo, string usuario, string icone)
        {
            this.descricao = descricao;
            this.titulo = titulo;
            this.usuario = usuario;
            this.icone = icone;
        }

        //construtor
        public Caderno(string descricao, string titulo, int codigo, string icone)
        {
            this.descricao = descricao;
            this.titulo = titulo;
            this.codigo = codigo;
            this.icone = icone;
        }

        //getters e setters
        public string Descricao { get => descricao; set => descricao = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Icone { get => icone; set => icone = value; }
        public int Codigo { get => codigo; set => codigo = value; }

        //métodos
        //Cadastrar o caderno
        public string cadastrarCaderno()
        {
            string situacaoCad = "";
            try
            {
                //abrindo a conexão
                conexao.Open();
                //Comando de inserção no banco
                MySqlCommand inserirCaderno = new MySqlCommand(
                    "INSERT INTO CADERNO(titulo, descricao, icone, FK_USUARIO_nome) VALUES(@titulo,@descricao, @icone, @FK_USUARIO_nome)", conexao);
                //Definindo os parâmetros
                inserirCaderno.Parameters.AddWithValue("@descricao", Descricao);
                inserirCaderno.Parameters.AddWithValue("@titulo", Titulo);
                inserirCaderno.Parameters.AddWithValue("@icone", Icone);
                inserirCaderno.Parameters.AddWithValue("@FK_USUARIO_nome", usuario);
                //Executando o comando
                inserirCaderno.ExecuteNonQuery();
                
                situacaoCad = "Caderno cadastrado com sucesso";
            }
            catch(Exception e)
            {
                //caso dê algum erro no processo retornar qual foi o erro
                situacaoCad = "Erro de conexão!" + e;
                
            }
            finally
            {
                conexao.Close();
            }

            return situacaoCad;
        }
        
       //Deletar caderno
        static public string deletarCaderno(int id)
        {
            string sitDel = "";
            try
            {
                conexao.Open();
                //criando o comando e definindo seu parâmetro
                MySqlCommand deletarCaderno = new MySqlCommand("DELETE FROM CADERNO WHERE codigo = @cod", conexao);
                deletarCaderno.Parameters.AddWithValue("@cod", id);
                //executando o comando
                deletarCaderno.ExecuteNonQuery();
                
                sitDel = "Caderno deletado com sucesso!";

            }
            catch (Exception e)
            {
                sitDel = "Erro! " + e;
            }
            finally
            {
                conexao.Close();
            }
            return sitDel;
        }

        static public Caderno CadernoSelecionado(int id)
        {
            Caderno caderno = new Caderno();
            try
            {
                conexao.Open();
                MySqlCommand pesquisaCaderno = new MySqlCommand("SELECT * FROM CADERNO where codigo = @id", conexao);
                pesquisaCaderno.Parameters.AddWithValue("@id", id);
                //Lista de cadernos vindas do banco
                MySqlDataReader leitorBanco = pesquisaCaderno.ExecuteReader();

                while (leitorBanco.Read())
                {
                    caderno = new Caderno(
                        (string)leitorBanco["descricao"],
                        (string)leitorBanco["titulo"],
                        int.Parse(leitorBanco["codigo"].ToString()),
                        (string)leitorBanco["icone"].ToString()
                        );
                }
                return caderno;
            }
            catch (Exception e)
            {
              
            }
            finally
            {
                conexao.Close();
            }
            return caderno;
        }
        static public List<Caderno> listarCaderno(string nome)
        {
            List<Caderno> listaCaderno = new List<Caderno>();
            try
            {
                conexao.Open();
                MySqlCommand pesquisaCaderno = new MySqlCommand("SELECT * FROM CADERNO where FK_USUARIO_nome = @nome", conexao);
                pesquisaCaderno.Parameters.AddWithValue("@nome", nome);
                //Lista de cadernos vindas do banco
                MySqlDataReader leitorBanco = pesquisaCaderno.ExecuteReader();

                while (leitorBanco.Read())
                {
                    listaCaderno.Add(new Caderno(
                        (string) leitorBanco["descricao"],
                        (string) leitorBanco["titulo"],
                        int.Parse(leitorBanco["codigo"].ToString()),
                        (string)leitorBanco["icone"].ToString()
                        ));
                }
                conexao.Close();
                return listaCaderno;
            }
            catch (Exception e)
            {
                
            }
            return listaCaderno;
        }

        //Editar propriedades do caderno
        static public string editarDescricao(string titulo)
        {
            string estadoEdicao ="";

            try
            {
                conexao.Open();
                MySqlCommand editarTitulo = new MySqlCommand("UPDATE CADERNO set titulo = @titulo", conexao);
                editarTitulo.Parameters.AddWithValue("@titulo", titulo);
                
                editarTitulo.ExecuteNonQuery();
                estadoEdicao = "Editado!";
            }
            catch (Exception e)
            {
                estadoEdicao = "Erro de conexão" + e;
            }
            finally
            {
                conexao.Close();
            }

            return estadoEdicao;
        }

        //Editar propriedades do caderno
        static public string editarNome(string descricao)
        {
            string estadoEdicao = "";
            try
            {
                conexao.Open();
                MySqlCommand editarTitulo = new MySqlCommand("UPDATE CADERNO set titulo = @titulo", conexao);
                editarTitulo.Parameters.AddWithValue("@titulo", descricao);

                editarTitulo.ExecuteNonQuery();
                estadoEdicao = "Editado!";
            }
            catch (Exception e)
            {
                estadoEdicao = "Erro de conexão" + e;
            }
            finally
            {
                conexao.Close();
            }

            return estadoEdicao;
        }
    }
}
