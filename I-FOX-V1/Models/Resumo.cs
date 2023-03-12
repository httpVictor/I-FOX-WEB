namespace I_FOX_V1.Models
{
    public class Resumo
    {

        //atributos
        private int codigo, data_resumo;
        private string tipo, titulo, texto, frente, verso;
        private int caderno;

        //construtor
        public Resumo(int codigo, int data_resumo, string tipo, string titulo, string texto, string frente, string verso, int caderno)
        {
            this.codigo = codigo;
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
        public int Data_resumo { get => data_resumo; set => data_resumo = value; }
        public string Tipo { get => tipo; set => tipo = value; }
        public string Titulo { get => titulo; set => titulo = value; }
        public string Texto { get => texto; set => texto = value; }
        public string Frente { get => frente; set => frente = value; }
        public string Verso { get => verso; set => verso = value; }
        public int Caderno { get => codigo; set => codigo = value; }


        //métodos

        public string cadastrarResumo()
        {
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
