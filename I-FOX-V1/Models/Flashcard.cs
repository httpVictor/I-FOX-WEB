using MySql.Data.MySqlClient;
using System;

namespace I_FOX_V1.Models
{
    public class Flashcard
    {
        //Atributos
        private List<string> listaFrente, listaVerso;
        private List<int> codigoCard;
        private int codCaderno;
        private string tituloResumo;

        static MySqlConnection conexao = FabricaConexao.getConexao();


        //Construtor vazio
        public Flashcard()
        {

        }
        //Construtor
        public Flashcard(List<int> codigoCard, List<string> listaFrente, List<string> listaVerso, int codResumo, string tituloResumo)
        {
            this.codigoCard = codigoCard;
            this.listaFrente = listaFrente;
            this.listaVerso = listaVerso;
            this.codCaderno = codResumo;
            this.tituloResumo = tituloResumo;
        }

        //Getters e setters
        public List<string> ListaFrente { get => listaFrente; set => listaFrente = value; }
        public List<string> ListaVerso { get => listaVerso; set => listaVerso = value; }
        public int CodResumo { get => codCaderno; set => codCaderno = value; }
        public List<int> CodigoCard { get => codigoCard; set => codigoCard = value; }
        public string TituloResumo { get => tituloResumo; set => tituloResumo = value; }

        public string cadastrar()
        {
            string sitCadastro = "";

            try //tente efetuar a conexão, caso dê algum erro cair no catch
            {
                conexao.Open(); //Abrindo conexão

                //Procurando o código daquele resumo!
                MySqlCommand pesquisaResumo = new MySqlCommand("SELECT * FROM RESUMO where titulo = @titulo and FK_CADERNO_codigo = @cod_caderno", conexao);
                pesquisaResumo.Parameters.AddWithValue("@titulo", TituloResumo);
                pesquisaResumo.Parameters.AddWithValue("@cod_caderno", CodResumo);

                //Quando se executa uma pesquisa, tem como retorno as linhas de uma tabela que são guardadas em um leitor
                MySqlDataReader leitorBanco = pesquisaResumo.ExecuteReader();

                while (leitorBanco.Read()) //Enquanto for possível ler ele
                {
                    //Definindo os atributos que vão vir do banco
                    CodResumo = int.Parse(leitorBanco["codigo"].ToString());
                }

                conexao.Close();

                //Salvando cartão por cartão
                for (int i = 0; i < ListaFrente.Count; i++)
                {
                    //abrir a conexão 
                    conexao.Open();

                    //criando o comando de insert
                    MySqlCommand inserir = new MySqlCommand("INSERT INTO CARD(frente, verso, FK_RESUMO_codigo) VALUES(@frente, @verso, @FK_Resumo)", conexao);


                    //Passando os valores para os parâmetros para evitar INJEÇÃO DE SQL
                    inserir.Parameters.AddWithValue("@frente", ListaFrente[i]);
                    inserir.Parameters.AddWithValue("@verso", ListaVerso[i]);
                    inserir.Parameters.AddWithValue("@FK_Resumo", CodResumo);


                    //Executando o comando
                    inserir.ExecuteNonQuery(); //É um insert, logo não é necessário uma pesquisa (query)!
                    conexao.Close();
                }

                sitCadastro = "Cadastrado";

            }
            catch (Exception e) //o e armazena o tipo de erro que aconteceu!
            {
                sitCadastro = "Erro de conexão!" + e;
            }
            finally
            {
                conexao.Close(); //Fechando a conexão, dando certo, ou não!
            }

            return sitCadastro;
        }

        static public string deletar(int id_resumo)
        {
            //variável que vai armazenar se o cadastro foi ou não realizado.
            string situacao_deletar = "";

            try //tente efetuar a conexão, caso dê algum erro cair no catch
            {
                //abrir a conexão 
                conexao.Open();

                //criando o comando de delete
                MySqlCommand deletar = new MySqlCommand("DELETE FROM CARD WHERE FK_RESUMO_codigo = @codigo", conexao);

                //Passando os valores para os parâmetros para evitar INJEÇÃO DE SQL
                deletar.Parameters.AddWithValue("@codigo", id_resumo);

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
        static public string deletarCartao(int id_cartao)
        {
            //variável que vai armazenar se o cadastro foi ou não realizado.
            string situacao_deletar = "";

            try //tente efetuar a conexão, caso dê algum erro cair no catch
            {
                //abrir a conexão 
                conexao.Open();

                //criando o comando de delete
                MySqlCommand deletar = new MySqlCommand("DELETE FROM CARD WHERE codigo = @codigo", conexao);

                //Passando os valores para os parâmetros para evitar INJEÇÃO DE SQL
                deletar.Parameters.AddWithValue("@codigo", id_cartao);

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

        public static string editar(List<string> frente, List<string> verso, List<int> codigo)
        {
            string sit_update = "";
            try
            {
                //editando cartão por cartão...
               
                for (int i = 0; i < frente.Count ; i++)
                {
                    conexao.Open();
                    //CRIANDO COMANDO DE INSERIR USUÁRIOS NO BANCO DE DADOS
                    MySqlCommand inserir = new MySqlCommand("UPDATE CARD SET frente = @frente, verso = @verso WHERE codigo = @codigo", conexao);
                    inserir.Parameters.AddWithValue("@frente", frente[i]);
                    inserir.Parameters.AddWithValue("@verso", verso[i]);
                    inserir.Parameters.AddWithValue("@codigo", codigo[i]);

                    inserir.ExecuteNonQuery();
                    sit_update = "Atualizado com sucesso";
                    conexao.Close();
                }
                
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

        static public List<Flashcard> listar(int cod_resumo)
        {
            List<Flashcard> cards = new List<Flashcard>();

            try
            {
                conexao.Open();
                //Criando as listas de frente, verso e código
                List<string> frentes = new List<string>();
                List<string> versos = new List<string>();
                List<int> codigos = new List<int>();
                int codigoRes = 0;

                //Pesquisando no banco
                MySqlCommand pesquisa = new MySqlCommand("SELECT * FROM CARD where FK_RESUMO_codigo = @codigo", conexao);
                pesquisa.Parameters.AddWithValue("@codigo", cod_resumo);
                MySqlDataReader leitorBanco= pesquisa.ExecuteReader();

                //Add as listas de frente e verso dos cards
                while (leitorBanco.Read())
                {
                    frentes.Add((string) leitorBanco["frente"]);
                    versos.Add((string) leitorBanco["verso"]);
                    codigos.Add(int.Parse(leitorBanco["codigo"].ToString()));
                    codigoRes = int.Parse(leitorBanco["FK_RESUMO_codigo"].ToString());
                }

                Flashcard flash = new Flashcard(codigos, frentes, versos, cod_resumo, null);
                cards.Add(flash);
            }
            catch (Exception e)
            {
                string erro = "Erro de conexão" + e;
            }
            finally
            {
                conexao.Close();
            }


            return cards;
        }
    }
}
