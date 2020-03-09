using System;
using tabuleiro;
using tabuleiro.Enuns;
using xadrez;
using tabuleiro.Exceptions;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
           
            try{

                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.Terminada)
                {
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tab);
                   
                    Console.WriteLine();
                    Console.Write("Digite a Posição de Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().toPosicao();

                    bool[,] posicoesPossiveis = partida.Tab.Peca(origem).MovimentosPossiveis();
                    Console.Clear();
                    Tela.ImprimirTabuleiro(partida.Tab, posicoesPossiveis);
                    
                    Console.WriteLine();
                    Console.Write("Digite a Posição de Destino: ");
                    Posicao destino = Tela.LerPosicaoXadrez().toPosicao();

                    partida.ExecutaMovimento(origem, destino);
                }
            
                Console.ReadLine();
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
