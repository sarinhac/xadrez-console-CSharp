﻿using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + "  ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                   ImprimirPeca(tab.Peca(i, j));
                      
                    
                }
                Console.WriteLine();
               
            }
                Console.WriteLine("   A  B  C  D  E  F  G  H");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkGray;

            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + "  ");
                
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }
                    ImprimirPeca(tab.Peca(i, j));
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();

            }
            Console.WriteLine("   A  B  C  D  E  F  G  H");
            Console.BackgroundColor = fundoOriginal;
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
            if(peca == null)
            {
                Console.Write("-  ");
            }
            else
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
                Console.Write(" ");
            }

        }
    }
}