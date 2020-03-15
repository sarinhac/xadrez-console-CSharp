﻿using System.Collections.Generic;
using tabuleiro;
using tabuleiro.Enuns;
using tabuleiro.Exceptions;


namespace xadrez
{
    class PartidaDeXadrez
    {
        private HashSet<Peca> _pecas;
        private HashSet<Peca> _capturadas;
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public Tabuleiro Tab { get; private set; }
        public bool Terminada { get; private set; }
        public bool Xeque { get; private set; }


        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            this._pecas = new HashSet<Peca>();
            this._capturadas = new HashSet<Peca>(); 
            ColocarPecas();     
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            p.IncrementarQtdMovimento();
            Peca pecaCapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
            if(pecaCapturada != null)
            {
                this._capturadas.Add(pecaCapturada);
            }

            //#jogadaEspecial Roque Pequeno

            if(p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemDaTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoDaTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca Torre = Tab.RetirarPeca(origemDaTorre);
                Torre.IncrementarQtdMovimento();
                Tab.ColocarPeca(Torre, destinoDaTorre);
            }

            //#jogadaEspecial Roque Grande

            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemDaTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoDaTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca Torre = Tab.RetirarPeca(origemDaTorre);
                Torre.IncrementarQtdMovimento();
                Tab.ColocarPeca(Torre, destinoDaTorre);
            }

            return pecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementarQtdMovimento();
            if(pecaCapturada != null)
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                this._capturadas.Remove(pecaCapturada);
            }
            Tab.ColocarPeca(p, origem);

            //#jogadaEspecial Roque Pequeno

            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemDaTorre = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoDaTorre = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca Torre = Tab.RetirarPeca(destinoDaTorre);
                Torre.DecrementarQtdMovimento();
                Tab.ColocarPeca(Torre, origemDaTorre);
            }
            
            //#jogadaEspecial Roque Grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemDaTorre = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoDaTorre = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca Torre = Tab.RetirarPeca(destinoDaTorre);
                Torre.DecrementarQtdMovimento();
                Tab.ColocarPeca(Torre, origemDaTorre);
            }


        }

       public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em XEQUE!");
            }

            Peca p = Tab.Peca(destino);

            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (TesteXequeMate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }

           
        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if(Tab.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça nesta posição!");
            }
            if(JogadorAtual != Tab.Peca(pos).Cor)
            {
                throw new TabuleiroException("A peça escolhida não é sua!");
            }
            if (!Tab.Peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Esta peça não pode ser movida, pois está bloqueada!");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("Esta peça não pode fazer esse movimento!");
            }
        }

        private void MudaJogador()
        {
            if(JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
               JogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            
            foreach(Peca p in this._capturadas)
            {
                if(p.Cor == cor)
                {
                    aux.Add(p);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();

            foreach (Peca p in this._pecas)
            {
                if (p.Cor == cor)
                {
                    aux.Add(p);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca Rei (Cor cor)
        {
            foreach (Peca p in PecasEmJogo(cor))
            {
                if(p is Rei)
                {
                    return p;
                }
            }

            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca r = Rei(cor);
            if (r == null)
            {
                throw new TabuleiroException("Sem um Rei você não pode jogar!");
            }
            foreach(Peca p in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = p.MovimentosPossiveis();
                if(mat[r.Posicao.Linha, r.Posicao.Coluna])
                {
                    return true;
                }
            }

            return false;
        }

        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for(int i = 0; i< Tab.Linhas; i++)
                {
                    for(int j = 0; j< Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }

                        }
                    }
                }
            }
            return true;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca novaPeca)
        {
            Tab.ColocarPeca(novaPeca, new PosicaoXadrez(coluna, linha).toPosicao());
            this._pecas.Add(novaPeca);
        }
        
        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 2, new Peao(Tab, Cor.Branca));
            ColocarNovaPeca('b', 2, new Peao(Tab, Cor.Branca));
            ColocarNovaPeca('c', 2, new Peao(Tab, Cor.Branca));
            ColocarNovaPeca('d', 2, new Peao(Tab, Cor.Branca));
            ColocarNovaPeca('e', 2, new Peao(Tab, Cor.Branca));
            ColocarNovaPeca('f', 2, new Peao(Tab, Cor.Branca));
            ColocarNovaPeca('g', 2, new Peao(Tab, Cor.Branca));
            ColocarNovaPeca('h', 2, new Peao(Tab, Cor.Branca));

            ColocarNovaPeca('a', 1, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(Tab, Cor.Branca));
            ColocarNovaPeca('c', 1, new Bispo(Tab, Cor.Branca));
            ColocarNovaPeca('d', 1, new Rainha(Tab, Cor.Branca));
            ColocarNovaPeca('e', 1, new Rei(Tab, Cor.Branca, this));
            ColocarNovaPeca('f', 1, new Bispo(Tab, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(Tab, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(Tab, Cor.Branca));


            ColocarNovaPeca('a', 7, new Peao(Tab, Cor.Preta));
            ColocarNovaPeca('b', 7, new Peao(Tab, Cor.Preta));
            ColocarNovaPeca('c', 7, new Peao(Tab, Cor.Preta));
            ColocarNovaPeca('d', 7, new Peao(Tab, Cor.Preta));
            ColocarNovaPeca('e', 7, new Peao(Tab, Cor.Preta));
            ColocarNovaPeca('f', 7, new Peao(Tab, Cor.Preta));
            ColocarNovaPeca('g', 7, new Peao(Tab, Cor.Preta));
            ColocarNovaPeca('h', 7, new Peao(Tab, Cor.Preta));


            ColocarNovaPeca('a', 8, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(Tab, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(Tab, Cor.Preta));
            ColocarNovaPeca('d', 8, new Rainha(Tab, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(Tab, Cor.Preta, this));
            ColocarNovaPeca('f', 8, new Bispo(Tab, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(Tab, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(Tab, Cor.Preta));

        }
    }
}
