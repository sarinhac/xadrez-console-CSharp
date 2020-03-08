using System;
using tabuleiro;

namespace xadrez_console
{
    class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + "  ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    
                    if(tab.Peca(i,j) == null)
                    {
                        Console.Write( "-  ");
                    }
                    else
                    {
                        imprimirPeca(tab.Peca(i, j));
                    }
                    
                }
                Console.WriteLine();
               
            }
                Console.WriteLine("   A  B  C  D  E  F  G  H");
        }

        public static void imprimirPeca(Peca peca)
        {
            if(peca.Cor == tabuleiro.Enuns.Cor.Branca)
            {
                Console.Write(peca + " ");
            }
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(peca + " ");
                Console.ForegroundColor = aux;
            }

        }
    }
}
