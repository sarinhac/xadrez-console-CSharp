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

            
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0, 0));
                tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(1, 3));
                tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(0, 2));

                tab.ColocarPeca(new Torre(tab, Cor.Branca), new Posicao(3, 5));
                tab.ColocarPeca(new Torre(tab, Cor.Branca), new Posicao(2, 6));
                tab.ColocarPeca(new Rei(tab, Cor.Branca), new Posicao(0, 5));

                Tela.imprimirTabuleiro(tab);
            
                Console.ReadLine();
            }
            catch (TabuleiroException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
