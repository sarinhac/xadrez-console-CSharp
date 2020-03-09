﻿using tabuleiro.Enuns;

namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdMovimento { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set; }

        public Peca (Cor cor, Tabuleiro tabuleiro)
        {
            Posicao = null;
            Cor = cor;
            Tabuleiro = tabuleiro;
            QtdMovimento = 0;
        }

        public void IncrementarQtdMovimento()
        {
            QtdMovimento++;
        }

        public abstract bool[,] MovimentosPossiveis();
        
    }
}
