using MySql.Data.MySqlClient;
using Org.BouncyCastle.Crypto.Digests;
using System;
using System.Numerics;

namespace I_FOX_V1.Models
{
    public class Usuario
    {
        //atributos
        private string nome, email, senha;
        private string data_nasc;
        private int codigo;

        // Criar conexão com o banco de dados
        MySqlConnection conexao = FabricaConexao.getConexao();

        public Usuario()
        {
        }

        //CONSTRUTOR
        public Usuario(string nome, string email, string senha, string data_nasc)
        {
            this.nome = nome;
            this.email = email;
            this.senha = senha;
            this.data_nasc = data_nasc;
        }

        //Construtor para logar
        public Usuario(string nome, string senha)
        {
            this.nome = nome;
            this.senha = senha;
        }

        //getters e setters
        public string Nome { get => nome; set => nome = value; }
        public string Email { get => email; set => email = value; }
        public string Senha { get => senha; set => senha = value; }
        public string Data_nasc { get => data_nasc; set => data_nasc = value; }
        public int Codigo { get => codigo;}

        //MÉTODOS
        //CADASTRANDO USUÁRIO

        public string cadastrarUsuario()
        {
            string cadastro = "";
            try
            {
                if (!existeUsuario(Nome))
                {
                    conexao.Open();

                    //CRIANDO COMANDO DE INSERIR USUÁRIOS NO BANCO DE DADOS
                    MySqlCommand inserir = new MySqlCommand("INSERT INTO USUARIO VALUES(@nome, @email, @senha, @avatar, @data_nasc)", conexao);

                    inserir.Parameters.AddWithValue("@nome", Nome);
                    inserir.Parameters.AddWithValue("@senha", Senha);
                    inserir.Parameters.AddWithValue("@email", Email);
                    inserir.Parameters.AddWithValue("@data_nasc", Data_nasc);
                    inserir.Parameters.AddWithValue("@avatar", 0);

                    inserir.ExecuteNonQuery();
                    cadastro = "cadastrado";
                    conexao.Close();

                    //Puxar o id do usuário


                }
                else
                {
                    cadastro = "Esse usuário já existe! Escolha outro nome de usuário";
                }
            }
            catch (Exception e)
            {
                cadastro = "Erro de conexão!" + e;
            }
            finally
            {
      
            }

            return cadastro;
        }

        //MÉTODO PARA VERIFICAR O LOGIN DO USUÁRIO
        //Seguindo a lógica de -> Exite no banco? Se existe a senha bate?
        public string logarUsuario()
        {
            //Variável que vai devolver o estado do login
            string situacao = "";

            try
            {
                conexao.Open();

                //CRIANDO COMANDO DE INSERIR USUÁRIOS NO BANCO DE DADOS
                MySqlCommand buscarUsuario = new MySqlCommand("SELECT * FROM USUARIO", conexao);
                MySqlDataReader listaUsuario = buscarUsuario.ExecuteReader();

                while (listaUsuario.Read())
                {
                    Usuario usuario = new Usuario((string )listaUsuario["nome"], (string) listaUsuario["senha"]);
                    //CONFERINDO SE AQUELE USUÁRIO EXISTE NO BANCO
                    if (usuario.Nome == Nome)
                    {
                        //A SENHA É A SENHA CADASTRADA PELO USUÁRIO?
                        if (usuario.Senha == Senha)
                        {
                            situacao = "logado";
                            
                            //Pegar o id do banco
                            break;
                        }
                        else
                        {
                            situacao = "Senha incorreta!";
                           
                            break;
                        }
                    }
                    else
                    {
                        situacao = "Usuário não cadastrado!";
                        
                    }
                }
                          
            }
            catch (Exception e)
            {
                situacao = "Erro de conexão!" + e;
            }

            conexao.Close();
            return situacao;
        }

        public bool existeUsuario(string nome)
        {
            //Variável que vai se já existe ou não um usuário com aquele nome
            bool situacao = false;

            try
            {
                conexao.Open();

                //CRIANDO COMANDO DE INSERIR USUÁRIOS NO BANCO DE DADOS
                MySqlCommand buscarUsuario = new MySqlCommand("SELECT * FROM usuario", conexao);
                MySqlDataReader listaUsuario = buscarUsuario.ExecuteReader();

                while (listaUsuario.Read())
                {
                    string nomeBanco = (string) listaUsuario["nome"];
                    //CONFERINDO SE AQUELE USUÁRIO EXISTE NO BANCO
                    if (nomeBanco == nome)
                    {
                        situacao = true;
                    }
                    else
                    {
                        situacao = false;
                    }
                }
            }
            catch (Exception e)
            {
                
            }
            finally
            {
                conexao.Close();
            }

            return situacao;
        }
    }
}
