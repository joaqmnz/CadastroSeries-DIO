using System;

namespace CadastroSeries
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcao = opcaoUsuario();

            while(opcao != "X")
            {
                switch(opcao)
                {
                    case "1": { cadastrarSerie(); break; }
                    case "2": { removerSerie(); break; }
                    case "3": { listar(); break; }
                    case "4": { atualizarSerie(); break; }
                    case "5": { reativarSerie(); break; }
                    case "L": { Console.Clear(); break; }
                    default: { Console.WriteLine("Opção inválida do menu!"); break; }
                }
                opcao = opcaoUsuario();
            }
        }

        public static void listar()
        {
            string opcao = opcaoUsuarioListar();
            while(opcao != "X")
            {
                switch(opcao)
                {
                    case "1": { listarSeries(1); break; }
                    case "2": { listarEspecifica(); break; }
                    case "3": { listarSeries(3); break; }
                    case "4": { listarSeries(4); break; }
                    case "5": { listarSeries(5); break;}
                    case "L": { Console.Clear(); break; }
                    default: { Console.WriteLine("Opção inválida do menu!"); break; }
                }
                opcao = opcaoUsuarioListar();
            }
        }

        public static void cadastrarSerie()
        {
            Console.WriteLine("Bem-vindo(a) ao cadastro de séries!");
            Console.WriteLine("Escolha uma categoria");
 
            cadastro(1, 0);
        }

        public static void atualizarSerie()
        {
            if(repositorio.ProximoId() == 0)
            {
                Console.WriteLine("Não há séries cadastradas!");
                return;
            }

            Console.WriteLine("Bem-vindo(a) à Atualização de Séries!");
            Console.WriteLine("Por favor, informe o ID da série para substituir: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine();

            bool existe = repositorio.ExisteNaLista(id);

            if(existe == false)
            {
                Console.WriteLine("Série não encontrada!");
                return;
            }

            cadastro(2, id);
        }

        public static void removerSerie()
        {
            if(repositorio.ProximoId() == 0)
            {
                Console.WriteLine("Não há séries cadastradas!");
                return;
            }

            Console.WriteLine("Bem-vindo(a) à remoção de Séries!");
            Console.Write("Por favor, digite o ID da Série: ");
            int id = int.Parse(Console.ReadLine());

            bool existe = repositorio.ExisteNaLista(id);

            if(existe == false)
            {
                Console.WriteLine("Série não encontrada!");
                return;
            }

            string data = "";
            registraData(ref data);
            repositorio.RegistraDataExcluido(id, data);
            repositorio.Remover(id);

            Console.WriteLine();
            Console.WriteLine("Remoção realizada com sucesso!");
        }

        public static void listarSeries(int x)
        {
            if(repositorio.ProximoId() == 0)
            {
                Console.WriteLine("Não há séries cadastradas!");
                return;
            }

            var list = repositorio.Listar();
            int cont = 0;
            foreach(var serie in list)
            {
                if(x == 1)
                {
                    if(serie.excluido() == false)
                    {
                        cont++;
                        continue;
                    }
                    Console.WriteLine("ID: {0} - {1} | Data limite: {2}", serie.retornarId(), serie.retornarTitulo(), serie.retornaDataLim());

                } else if(x == 3)
                {
                    if(serie.excluido() == false)
                        continue;

                    Console.WriteLine("{0} - {1}", serie.retornarTitulo(), serie.retornaDataLim());

                } else if(x == 4)
                {
                    if(serie.excluido() == false || serie.retornaDataAtualizacao() == null)
                    {
                        cont++;
                        continue;
                    }
                    Console.WriteLine("{0} - {1}", serie.retornarTitulo(), serie.retornaDataAtualizacao());

                } else
                {
                    if(serie.excluido() == true)
                    {
                        cont++;
                        continue;
                    }
                    Console.WriteLine("{0} | Excluída em: {1}", serie.retornarTitulo(), serie.retornaDataExlcuido());
                }
            }
            if(cont == repositorio.ProximoId() && x == 1)
            {
                Console.WriteLine("Não há séries!");
                return;

            } else if(cont == repositorio.ProximoId() && x == 4)
            {
                Console.WriteLine("Nenhuma série foi atualizada!");
                return;

            } else if(cont == repositorio.ProximoId() && x == 5)
            {
                Console.WriteLine("Não existem séries excluídas!");
                return;
            }
        }

        public static void listarEspecifica()
        {
            if(repositorio.ProximoId() == 0)
            {
                Console.WriteLine("Não há séries cadastradas!");
                return;
            }

            Console.WriteLine("Bem-vindo(a) à Listagem Específica!");
            Console.WriteLine("Por favor, informe o ID da série:");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine();

            bool existe = repositorio.ExisteNaLista(id);

            if(existe == false)
            {
                Console.WriteLine("Série não encontrada!");
                return;
            }

            var serie = repositorio.RetornaPorId(id);

            Console.WriteLine();
            Console.WriteLine(serie);
        }

        public static void cadastro(int x, int id)
        {
            Console.WriteLine();
            foreach(int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }
            byte gen = byte.Parse(Console.ReadLine());

            Console.Write("Digite o título da série: ");
            string tit = Console.ReadLine();

            Console.Write("Digite a descrição da série: ");
            string desc = Console.ReadLine();

            Console.Write("Digite o ano em que a série foi lançada: ");
            int ano = int.Parse(Console.ReadLine());

            //Quando for digitar a data deve inserir as barras
            Console.WriteLine("Digite a data limite da série");
            Console.WriteLine("OBS.: deve ser no formato DD/MM/AA, inclusive com as barras!");
            string data = Console.ReadLine();

            Series serie = new Series(repositorio.ProximoId(), (Genero)gen, tit, desc, ano, data);

            if(x == 1)
            {
                repositorio.Adicionar(serie);
                Console.WriteLine();
                Console.WriteLine("Cadastro realizado com sucesso!");
                return;
            }

            repositorio.Atualizar(id, serie);
            string data2 = "";
            registraData(ref data2);
            repositorio.RegistraDataAtualizacao(id, data2);
            Console.WriteLine();
            Console.WriteLine("Atualização realizada com sucesso!");
        }

        public static void reativarSerie()
        {
            if(repositorio.ProximoId() == 0)
            {
                Console.WriteLine("Não há séries cadastradas!");
                return;
            }

            var list = repositorio.Listar();
            int cont = 0;
            foreach(var serie in list)
            {
                if(serie.excluido() == true)
                {
                    cont++;
                    continue;
                }
            }

            if(cont == repositorio.ProximoId())
            {
                Console.WriteLine("Não existem séries para reativar!");
                return;
            }

            Console.WriteLine("Bem-vindo(a) à Reativação de Séries!");
            Console.WriteLine("Por favor, digite o ID da série: ");
            int id = int.Parse(Console.ReadLine());
            Console.WriteLine();

            bool existe = repositorio.ExisteNaLista(id);
            if(existe == false)
            {
                Console.WriteLine("Série não encontrada!");
                return;
            }

            repositorio.Reativar(id);
            Console.WriteLine("Reativação realizada com sucesso!");
        }

        public static void registraData(ref string s)
        {
            var data = DateTime.Now;
            string data1 = "" + data.Day + data.Month + data.Year;
            Console.WriteLine(data1);
            if(data1.Length < 8)
                data1 = "0" + data1;
            string d2 = data1.Insert(2, "/");
            s = d2.Insert(5, "/");
            Console.WriteLine(s);
        }

        public static string opcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("Cadastro de Séries da LunasFilm!!");
            Console.WriteLine("Escolha uma opção");
            Console.WriteLine();
            Console.WriteLine("1 - Cadastrar nova série");
            Console.WriteLine("2 - Remover série");
            Console.WriteLine("3 - Listagem");
            Console.WriteLine("4 - Atualizar série");
            Console.WriteLine("5 - Reativar série");
            Console.WriteLine("L - Limpar tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();
            string opcao = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcao;
        }

        public static string opcaoUsuarioListar()
        {
            Console.WriteLine();
            Console.WriteLine("Bem-vindo(a) ao menu de Listagem!");
            Console.WriteLine("Por favor, escolha uma opção");
            Console.WriteLine("1 - Listar séries");
            Console.WriteLine("2 - Listar série expecícica");
            Console.WriteLine("3 - Listar datas limite das séries");
            Console.WriteLine("4 - Listar data de atualização das séries");
            Console.WriteLine("5 - Listar séries excluídas");
            Console.WriteLine("L - Limpar tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();
            string opcao = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcao;
        }
    }
}
