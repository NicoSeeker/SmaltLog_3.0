using System.Data;
using System.Diagnostics;
using System.Text;

namespace SmaLog_3._0_Meu_Deus
{
    internal class Program
    {
        public class smalte
        {
            public string _Nome, _Marca;
            public int _Quantidade;

            public smalte(string Nome, string Marca, int Quantidade)
            {
                _Nome = Nome;
                _Marca = Marca;
                _Quantidade = Quantidade;
            }
        }
        public static void Main(string[] args)
        {
        ini:
            Abobora();
            var ListaComSmal = SmalteRead();

            Console.WriteLine("____________________________SmaltLog 3.0. Oh meu Deus, Oque eu fiz?__________________________________");
            Console.WriteLine("Digite: /a para adicionar novo item, /m para busca por marca, /i para informações e /p para imprimir\n");

            string interation = Console.ReadLine();

            if (interation[0] != '/')
            {
                if (ListaComSmal.Exists(X => X._Nome == interation))
                {
                    smalte smalteTarget = ListaComSmal.FirstOrDefault(X => X._Nome == interation);
                    int indexsmalte = ListaComSmal.FindIndex(X => X._Nome == interation);
                    InfoSmalte(smalteTarget);

                    Console.WriteLine("\n Digite /i para incrementar, /s para subtrair ou /r para remover. \n ");
                    string anotherinterection = Console.ReadLine();
                    if (anotherinterection != String.Empty)
                    { 
                         if (anotherinterection[0] == '/') 
                         {
                            switch (anotherinterection.ToLower())
                            {
                                case "/i":
                                    smalteTarget._Quantidade += 1;
                                    Console.WriteLine("\nAgora existem {0} smalts", smalteTarget._Quantidade);
                                    Console.ReadKey();
                                    break;

                                case "/s":
                                    smalteTarget._Quantidade -= 1;
                                    Console.WriteLine("\nAgora existem {0} smalts", smalteTarget._Quantidade);
                                    Console.ReadKey();
                                    break;

                                case "/r":
                                    ListaComSmal.Remove(smalteTarget);
                                    Console.WriteLine("\nItem removido da Lista");
                                    Console.ReadKey();
                                    break;


                            }
                        };
                    }   
                    SmalteWrite(ListaComSmal);
                    goto ini;
                }
                else
                {
                    Console.WriteLine("Smalte não encontrado na Data Base");
                    goto ini;
                }
            }
            else
            {
              switch (interation.Substring(0,2))
                {
                    case "/a":
                        Console.Clear();
                        Console.WriteLine("________________ ADD Smalt________________\n");
                        SmaltADD();

                        break;

                    case "/m":
                        
                        string marcaS = interation.Substring(3);

                        var buscacor = ListaComSmal.Where(X => X._Marca == marcaS);

                        if (buscacor == null)
                        {
                            Console.WriteLine($"Não foi possivel encontrar essa marca: {marcaS}");
                        }
                        else
                        {
                            foreach (smalte item in buscacor)
                            {
                                Console.WriteLine("__________________________");
                                Console.WriteLine("Nome: {0}", item._Nome);
                                Console.WriteLine("Marc: {0}", item._Marca);
                                Console.WriteLine("Quan: {0}", item._Quantidade);
                            }
                        }
                        Console.ReadLine();
                        break;

                    case "/i":
                        int Totalsmalt = 0;
                        int TotalsmaltDiferrent = 0;

                        foreach(smalte item in ListaComSmal)
                        {
                            Totalsmalt += item._Quantidade;
                        }
                        TotalsmaltDiferrent = ListaComSmal.Count;

                        //Console.Clear();
                        Console.WriteLine("\nExistem {0} smalts diferentes, com um total de {1}",TotalsmaltDiferrent,Totalsmalt);
                        Console.ReadKey();
                        break;

                    case "/p":
                        Imprimir(ListaComSmal);
                        break;
                }
                Console.Clear();
                goto ini;
            }

          void SmaltADD()
            {
                string smaltaddNome, smaltaddmarca;
              
                Console.Write("Nome do smalte: ");
                smaltaddNome = Console.ReadLine();
                Console.Write("Marca do smalte: ");
                smaltaddmarca =Console.ReadLine();

                if (smaltaddmarca != String.Empty || smaltaddmarca != String.Empty)
                {
                    if (ListaComSmal.Exists(X => X._Nome == smaltaddNome))
                    {
                        Console.WriteLine("Já existe um Smalte com esse nome.");
                        Console.ReadLine();
                    }
                    else
                    {
                        ListaComSmal.Add(new smalte(smaltaddNome, smaltaddmarca, 1));
                        SmalteWrite(ListaComSmal);
                    }
                }
            }

        }
       
        public static void InfoSmalte(smalte target)
        {
            Console.WriteLine("\n_______________________________");
            Console.WriteLine("Nome  : {0}", target._Nome);
            Console.WriteLine("Marca : {0}", target._Marca);
            Console.WriteLine("Quant : {0}", target._Quantidade);

        } 
        public static List<smalte> SmalteRead()
        {
            string listtxtPath = Environment.CurrentDirectory + "//SmalLog.txt";

            if (!File.Exists(listtxtPath)) throw new Exception("Erro 69 : Lista não encontrada");

            string[] smalttoken = File.ReadAllText(listtxtPath).Replace("\r\n", "").Split(';');
           
            List<smalte> returnList = new List<smalte>();
            
            foreach (string item in smalttoken)
            {
                if (item != String.Empty)
                {
                    string[] individualvalor = item.Split(':');

                    string NomeSmalt = individualvalor[0];
                    string MaraSmalt = individualvalor[1];
                    int QuantidSmalt = Convert.ToInt32(individualvalor[2]);

                    returnList.Add(new smalte(NomeSmalt, MaraSmalt, QuantidSmalt));
                }
            }
            return returnList;  
        }
        public static void SmalteWrite(List<smalte> listofesmalte)
        {
            StringBuilder textsmalte = new StringBuilder();
            string notherlisttxtPath = Environment.CurrentDirectory + "//SmalLog.txt";

            foreach(smalte item in listofesmalte)
            {
                textsmalte.AppendLine(item._Nome + ":" + item._Marca + ":" + item._Quantidade + ';');
            }

            File.WriteAllText(notherlisttxtPath, textsmalte.ToString());
        } 
        public static void Abobora()
        {
            if (!File.Exists(Environment.CurrentDirectory + "//SmalLog.txt"))
            {
                string DummySmalt = "Test:ruinSeekers:0";
                File.WriteAllText(Environment.CurrentDirectory + "//SmalLog.txt", DummySmalt);
            }
        } 
        public static void Imprimir(List<smalte> target)
        {
            List<string> marca = new List<string>();
            StringBuilder stringB = new StringBuilder();
            DateTime date = DateTime.Now;

            stringB.AppendLine("--------- Test de Impressão do SmaLog 3.0, oh meu Deus oque eu fiz? ---------\n");

            foreach (smalte item in target)
            {
                if (marca.IndexOf(item._Marca) < 0)
                {
                    if (item._Marca != "ruinSeekers") marca.Add(item._Marca);
                }
            }

            foreach (string item in marca)
            {
               IEnumerable<smalte> smaltesepa = target.Where(X => X._Marca == item);

               foreach(smalte smalte in smaltesepa)
                {
                    stringB.AppendLine($"{("Nome: " + smalte._Nome),-45}{("Marca: " + smalte._Marca),-20}{("Quant.: " + smalte._Quantidade),-8}");
                }
                stringB.AppendLine("------------------------------------------------------------------------------");
            }
            
            stringB.AppendLine(date.ToString());

            System.IO.File.WriteAllText(Environment.CurrentDirectory + "//impresssaoSmaLog_3.0.txt", stringB.ToString());
            Process.Start("notepad.exe",Environment.CurrentDirectory + "//impresssaoSmaLog_3.0.txt");

        }

    }


}
