using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Klingon
{
    internal class Program
    {
        static string[] palavrasArquivoTexto;
        static string letrasTipoFoo = "slfwek";
        static List<char> alfabetoKinglon { get; set; } = new List<char>("kbwrqdnfxjmlvhtcgzps".ToCharArray());
        static List<char> alfabeto { get; set; } = new List<char>("abcdefghijklmnopqrstuvwxyz".ToCharArray());

        static void Main(string[] args)
        {

            Console.Write("Digite o caminho do arquivo .txt com texto da língua Klingon: ");
            string path = Console.ReadLine();
            Console.WriteLine();

            if (File.Exists(path))
            {
                string[] data = File.ReadAllLines(path);
                foreach (string line in data)
                {
                    palavrasArquivoTexto = line.Split(' ');
                }

                ImprimePreposicoes();
                ImprimeVerbos();
                ImprimeNumeroBonito();
                ImprimeListaOrdenada();
            }
            else
            {
                Console.WriteLine("Arquivo não encontrado!");                
            }
            Console.ReadKey();
        }

        /// <summary>
        /// Método que verifica se uma letra da palavra é do tipo foo
        /// </summary>
        /// <param name="palavra">String a ser analisada</param>
        /// <param name="posicao">Posição da letra a ser analisada</param>
        /// <returns>Retorna um valor booleano que diz se encontrou na posição específicada uma letra do tipo foo</returns>
        static bool VerificaLetrasFoo (string palavra, int posicao)
        {
            foreach (char letra in letrasTipoFoo)
            {
                if (palavra[posicao].Equals(letra))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Método que define preposição e imprime sua quantidade.
        /// </summary>
        static void ImprimePreposicoes ()
        {
            int contadorPreposicao = 0;

            foreach (string palavra in palavrasArquivoTexto)
            {
                if (palavra.Length == 3 && palavra.IndexOf("d") == -1 && !VerificaLetrasFoo(palavra, 2))
                {
                    contadorPreposicao++;
                }
            }

            Console.WriteLine("O arquivo específicado contém " + contadorPreposicao + " preposições.");
        }

        /// <summary>
        /// Método que define verbo e que imprime sua quantidade.
        /// </summary>
        static void ImprimeVerbos ()
        {
            int contadorVerbos = 0;
            int verboPrimeiraPessoa = 0;

            foreach (string palavra in palavrasArquivoTexto)
            {
                int tamanhoPalavra = palavra.Length;
                if (tamanhoPalavra >= 8 && VerificaLetrasFoo(palavra, tamanhoPalavra-1))
                {
                    contadorVerbos++;

                    if (!VerificaLetrasFoo(palavra, 0))
                    {
                        verboPrimeiraPessoa++;
                    }
                }
            }

            Console.WriteLine("O arquivo específicado contém " + contadorVerbos + " verbos.");
            Console.WriteLine("Desses " + contadorVerbos + " verbos, " + verboPrimeiraPessoa + " são em primeira pessoa.");
        }

        /// <summary>
        /// Método que ordena as palavras do tipo foo e as imprime.
        /// </summary>
        static void ImprimeListaOrdenada ()
        {
            List<string> listaConvertida = new List<string>();
            Console.WriteLine();

            foreach (string palavra in palavrasArquivoTexto)
            {
                string palavraConvertida = ConverteAlfabeto(palavra);

                if (!listaConvertida.Contains(palavraConvertida))
                    listaConvertida.Add(palavraConvertida);
            }

            listaConvertida = listaConvertida.OrderBy(x => x).ToList();

            List<string> listaRetroceder = new List<string>();

            foreach (string palavra in listaConvertida)
            {
                string palavraconvertida = ConverteAlfabetoKinglon(palavra);
                listaRetroceder.Add(palavraconvertida);
                Console.Write(palavraconvertida + " ");
            }
        }

        /// <summary>
        /// Método que converte uma palavra do tipo foo para nosso alfabeto.
        /// </summary>
        /// <param name="palavra">Palavra a ser convertida.</param>
        /// <returns>Retorna a palavra convertida.</returns>
        static string ConverteAlfabeto (string palavra)
        {
            string retorno = "";

            foreach (char letra in palavra)
            {
                retorno += alfabeto[alfabetoKinglon.IndexOf(letra)];
            }

            return retorno;
        }

        /// <summary>
        /// Método que converte uma palavra do nosso alfabeto para o tipo foo.
        /// </summary>
        /// <param name="palavra">Palavra a ser convertida.</param>
        /// <returns>Retorna a palavra convertida.</returns>
        static string ConverteAlfabetoKinglon (string palavra)
        {
            string retorno = "";

            foreach (char letra in palavra)
            {
                retorno += alfabetoKinglon[alfabeto.IndexOf(letra)];
            }

            return retorno;
        }

        /// <summary>
        /// Método que converte a palavra do tipo foo em número.
        /// </summary>
        /// <param name="palavra">Palavra a ser convertida.</param>
        /// <returns>Retorna o número representado pela palavra.</returns>
        static double ConvertePalavraNumero (string palavra)
        {
            double retorno = 0;

            for (int x = 0; x < palavra.Length; x++)
            {
                retorno += alfabetoKinglon.IndexOf(palavra[x]) * (Math.Pow(20, x));
            }

            return retorno;
        }

        /// <summary>
        /// Método que define número bonito distinto e o imprime.
        /// </summary>
        static void ImprimeNumeroBonito ()
        {
            List<double> lista = new List<double>();

            foreach (string palavra in palavrasArquivoTexto)
            {
                double resultado = ConvertePalavraNumero(palavra);

                if (!lista.Contains(resultado) && resultado >= 440566 && resultado % 3 == 0)
                {
                    lista.Add(resultado);
                }
            }

            Console.WriteLine("O arquivo específicado contém " + lista.Count + " números bonitos distintos.");

        }
    }
}
