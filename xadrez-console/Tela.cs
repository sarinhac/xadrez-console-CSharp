using System;
using System.Collections.Generic;
using tabuleiro;
using tabuleiro.Enuns;
using xadrez;

namespace xadrez_console
{
    class Tela
    {
       public static void ImprimirPartida(PartidaDeXadrez partida)
        {
            ImprimirTabuleiro(partida.Tab);
            Console.WriteLine();
            Console.WriteLine();
            ImprimirPecasCapturadas(partida);
            Console.WriteLine($"Turno: {partida.Turno}");
            Console.WriteLine($"Aguardando jogada: {partida.JogadorAtual}");
            if (partida.Xeque)
            {
                Console.WriteLine("# XEQUE # ");
            }
        }

        public static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
        {
            Console.WriteLine("Peças Capturadas:");
            Console.Write("Brancas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ImprimirConjunto(partida.PecasCapturadas(Cor.Preta));
        }

        public static void ImprimirConjunto(HashSet<Peca> conjuntoPeca)
        {
            Console.Write("[");
            foreach(Peca p in conjuntoPeca)
            {
                Console.Write(p + " ");
            }
            Console.WriteLine("]");
        }
    
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                
                Console.Write(8 - i + "  ");
               
                for (int j = 0; j < tab.Colunas; j++)
                {
                 if((i % 2 == 0 && j % 2 !=0) || (i%2!=0 && j % 2 == 0))
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }

                    ImprimirPeca(tab.Peca(i, j));
                    Console.ResetColor();
                }
                Console.WriteLine();
                
            }
            Console.Write("   ");
            Console.WriteLine(" A  B  C  D  E  F  G  H");
            
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkRed;

        
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + "  ");

                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.BackgroundColor = ConsoleColor.Gray;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }
                    ImprimirPeca(tab.Peca(i, j));
                    Console.ResetColor();
                }
                Console.WriteLine();

            }
            Console.Write("   ");
            Console.WriteLine(" A  B  C  D  E  F  G  H");
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + " ");
            
            return new PosicaoXadrez(coluna, linha);
        } 

        public static void ImprimirPeca(Peca peca)
        {
            Console.Write(" ");
            if (peca == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (peca.Cor == tabuleiro.Enuns.Cor.Branca)
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(peca);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }

        }
    }
}
