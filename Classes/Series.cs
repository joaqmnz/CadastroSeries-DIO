using System;

namespace CadastroSeries
{
    public class Series
    {
        private int id { get; set; }
        private Genero genero { get; set; }
        private string titulo { get; set; }
        private string descricao { get; set; }
        private int ano { get; set; }
        private string limiteData { get; set; }
        private string dataExcluido {get; set; }
        private string dataAtualizada {get; set; }
        private bool ativo { get; set; }

        public Series(int id, Genero gen, string tit, string desc, int ano, string data)
        {
            this.id = id;
            this.genero = gen;
            this.titulo = tit;
            this.descricao = desc;
            this.ano = ano;
            this.ativo = true;
            this.limiteData = data;
        }

        //Para toda vez que pedir "imprima serie" ele imprimir neste estilo
        public override string ToString()
        {
            string serie = "";
            serie += "ID: " + this.id + "\n";
            serie += "Título: " + this.titulo + "\n";
            serie += "Gênero: " + this.genero + "\n";
            serie += "Descrição: " + this.descricao + "\n";
            serie += "Ano: " + this.ano + "\n";
            serie += "Data limite: " + this.limiteData + "\n";
            serie += "Ativo: " + this.ativo + "\n";
            return serie;
        }

        public int retornarId()
        {
            return this.id;
        }

        public string retornarTitulo()
        {
            return this.titulo;
        }

        public void excluir()
        {
            this.ativo = false;
        }

        public void reativar()
        {
            this.ativo = true;
        }

        public string retornaDataLim()
        {
            return this.limiteData;
        }
        
        public string retornaDataExlcuido()
        {
            return this.dataExcluido;
        }

        public string retornaDataAtualizacao()
        {
            return this.dataAtualizada;
        }

        public bool excluido()
        {
            return this.ativo;
        }

        public void dataExclusao(string d)
        {
            this.dataExcluido = d;
        }

        public void dataAtualizacao(string d)
        {
            this.dataAtualizada = d;
        }
    }
}