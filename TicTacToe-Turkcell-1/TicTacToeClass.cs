using System;

namespace TicTacToe_Turkcell_1
{

    public interface IDBgateway
    {
        public void save(char[] board);
        public char[] load();
    }

    public class DBgateway : IDBgateway
    {
        public void save(char[] board)
        {

        }

        public char[] load()
        {
            return new char[1] {'X'} ;
        }
    }



    internal class TicTacToeClass

    {

        public  IDBgateway _gateway;

        public  TicTacToeClass (IDBgateway gateway)
        {
            _gateway = gateway; 
        }


        private int _moveCount;
        private char[] _board = new char[9];
        private char _whoseTurn;

        public TicTacToeClass()
        {
        }

        internal void newGame()
        {
            _moveCount = 0;
            for (int i = 0; i < 9; i++) { _board[i] = 'E'; }
            _whoseTurn = 'X';
        }

        internal int moveCount()
        {
            return _moveCount;
        }

        internal void nextMove(char movement, int index)
        {
            if (!(this.anyWinner()))
            if (_whoseTurn == movement)
            if (_board[index] == 'E')
            {
                _moveCount++;

                _board[index] = movement;

                if (movement == 'O') _whoseTurn = 'X'; else _whoseTurn = 'O'; 

            }
        }

        internal char whoseTurn()
        {
            return _whoseTurn;
        }

        internal bool anyWinner()
        {
            for (int i=0; i<3;i++)
            if (_board[3*i+0] != 'E')
                if (_board[3*i+0] == _board[3*i+1])
                    if (_board[3*i+1] == _board[3*i+2]) return true;

            for (int i = 0; i < 3; i++)
                if (_board[i+0] != 'E')
                if (_board[i+0] == _board[i+3])
                    if (_board[i+3] == _board[i+6]) return true;

            if (_board[0] != 'E')
                if (_board[0] == _board[4])
                    if (_board[4] == _board[8]) return true;

            if (_board[2] != 'E')
                if (_board[2] == _board[4])
                    if (_board[4] == _board[6]) return true;


            return false;
        }

        internal char board(int index)
        {
            return _board[index];
        }

        internal void nextBestMove()
        {

            for (int i = 0; i < 9; i++)
            {
                if (_board[i] == 'E')
                {
                    _board[i] = this.whoseTurn();
                    if (this.anyWinner()) break; else _board[i] = 'E';
                }
            }

            if (_board[0] == _board[1]) if (_board[0]=='X') if (_board[2] == 'E') { this.nextMove(this.whoseTurn(), 2); return; }


            if (_board[4] == 'E') { this.nextMove(this.whoseTurn(), 4); return; }

            if (_board[1] == 'E') { this.nextMove(this.whoseTurn(), 1); return; }
            if (_board[3] == 'E') { this.nextMove(this.whoseTurn(), 3); return; }
            if (_board[5] == 'E') { this.nextMove(this.whoseTurn(), 5); return; }
            if (_board[7] == 'E') { this.nextMove(this.whoseTurn(), 7); return; }

        }

        internal void SaveTheBoard()
        {
            _gateway.save(_board);
        }

        internal void LoadTheBoard()
        {
            _board = _gateway.load();
        }

        internal char[] getTheBoard()
        {
            return _board;
        }
    }
}