using NUnit.Framework;
using NSubstitute;

namespace TicTacToe_Turkcell_1
{
    public class Tests
    {

        TicTacToeClass game;

        [SetUp]
        public void Setup()
        {

            //A G
            game = new TicTacToeClass();

            //A W

            game.newGame();

        }

        [Test]
        public void startAnewGame()
        {

            //A T

            int actualMoveCount = game.moveCount();
            Assert.AreEqual(0, actualMoveCount);
            Assert.That('X', Is.EqualTo(game.whoseTurn()));

        }

        [Test]
        public void firstMoveIncreaseMoveCounter()
        {

            game.nextMove('X', 0);


            //A T

            Assert.AreEqual(1, game.moveCount());


        }

        [TestCase('X', 0)]
        [TestCase('X', 1)]
        [TestCase('X', 2)]
        [TestCase('X', 3)]
        [TestCase('X', 4)]
        [TestCase('X', 5)]
        [TestCase('X', 6)]
        [TestCase('X', 7)]
        [TestCase('X', 8)]
        public void checkBoardforFirstMovement(char input, int coordinate)
        {

            game.nextMove(input, coordinate);


            //A T

            Assert.AreEqual(input, game.board(coordinate));


        }

        public void doubleinput()
        {
            game.nextMove('X', 0);
            game.nextMove('O', 0);
        }


        [Test]
        public void doubleInputTheSamePlace_movement()
        {

            doubleinput(); 

            Assert.AreEqual('X', game.board(0));

        }

        [Test]
        public void doubleInputTheSamePlace_moveCount()
        {

            doubleinput();

            Assert.AreEqual(1, game.moveCount());

        }


        [Test]
        public void sameMovementNotAllowed()
        {

            game.nextMove('X', 0);
            game.nextMove('X', 1);

            Assert.AreEqual(1, game.moveCount());

        }


        [Test]
        public void anyWinner()
        {
            Assert.AreEqual(false, game.anyWinner());
        }

        [Test]
        public void secondMoveTests()
        {

            game.nextMove('X', 0);
            game.nextMove('O', 1);
            game.nextMove('X', 2);

            Assert.AreEqual(3, game.moveCount());

        }

        public void setOfNextMove(char[] movement, int[] coordinate)
        {
            for (int i = 0; i < movement.Length; i++)
            {
                game.nextMove(movement[i], coordinate[i]);

            }
        }

        [TestCase(new char[] { 'X', 'O', 'X', 'O', 'X' }, new int[] { 0, 4, 1, 6, 2 })]
        [TestCase(new char[] { 'X', 'O', 'X', 'O', 'X' }, new int[] { 3, 1, 4, 6, 5 })]
        [TestCase(new char[] { 'X', 'O', 'X', 'O', 'X' }, new int[] { 6, 1, 7, 5, 8 })]
        [TestCase(new char[] { 'X', 'O', 'X', 'O', 'X', 'O' }, new int[] { 1, 0, 2, 3, 4, 6 })]
        [TestCase(new char[] { 'X', 'O', 'X', 'O', 'X' }, new int[] { 1, 0, 4, 3, 7 })]
        [TestCase(new char[] { 'X', 'O', 'X', 'O', 'X', 'O' }, new int[] { 1, 2, 3, 5, 7, 8 })]
        [TestCase(new char[] { 'X', 'O', 'X', 'O', 'X' }, new int[] { 0, 2, 4, 5, 8 })]
        [TestCase(new char[] { 'X', 'O', 'X', 'O', 'X', 'O' }, new int[] { 0, 2, 3, 4, 5, 6 })]
        public void anyWinneronTheBoard(char [] movement, int[] coordinate)
        {

            setOfNextMove(movement, coordinate);

            Assert.AreEqual(true, game.anyWinner());

        }

        [TestCase(new char[] { 'X', 'O', 'X', 'O', 'X' }, new int[] { 0, 2, 4, 5, 7 })]
        [TestCase(new char[] { 'X', 'O', 'X', 'O', 'X', 'O' }, new int[] { 0, 2, 3, 4, 5, 8 })]
        [TestCase(new char[] { 'X' }, new int[] { 0, 2, 3, 4, 5, 8 })]
        public void noWinneronTheBoard(char[] movement, int[] coordinate)
        {

            setOfNextMove(movement, coordinate);

            Assert.AreEqual(false, game.anyWinner());

        }


        [TestCase(new char[] { 'X', 'O', 'X', 'O', 'X', 'O' }, new int[] { 0, 2, 3, 4, 5, 6 }, 'X')]
        [TestCase(new char[] { 'X', 'O' }, new int[] { 0, 2 }, 'X')]
        [TestCase(new char[] { 'X', 'O', 'X' }, new int[] { 0, 2, 3 }, 'O')]
        [TestCase(new char[] { 'X' }, new int[] { 0 }, 'O')]
        public void myTurnwithNoWinner(char[] movement, int[] coordinate, char myTurn)
        {

            setOfNextMove(movement, coordinate);

            Assert.AreEqual(myTurn, game.whoseTurn());

        }


        [TestCase('X', new char[] { 'X', 'O', 'X', 'O' }, new int[] { 0, 3, 1, 4 }, 2)]
        [TestCase('O', new char[] { 'X', 'O', 'X', 'O', 'X' }, new int[] { 0, 3, 1, 4, 6 }, 5)]
        public void makeTheNextBestMoveToWin(char winner, char[] movement, int[] coordinate, int winningPosition)
        {

            // Arrange (TDD)   Given (BDD)
            setOfNextMove(movement, coordinate);

            // Act (TDD)   When (BDD)
            game.nextBestMove();

            //Assert (TDD)   Then (BDD) 
            Assert.AreEqual(winner, game.board(winningPosition));

        }

        [TestCase('O', new char[] { 'X' }, new int[] { 0 }, 4)]
        [TestCase('O', new char[] { 'X' }, new int[] { 4 }, 1)]
        public void makeTheSecondMove(char winner, char[] movement, int[] coordinate, int winningPosition)
        {

            // Arrange (TDD)   Given (BDD)
            setOfNextMove(movement, coordinate);

            // Act (TDD)   When (BDD)
            game.nextBestMove();

            //Assert (TDD)   Then (BDD) 
            Assert.AreEqual(winner, game.board(winningPosition));

        }


        [TestCase('O', new char[] { 'X', 'O', 'X', }, new int[] { 0, 4, 1 }, 2)]
        public void makeTheDefensiveMove(char winner, char[] movement, int[] coordinate, int winningPosition)
        {

            // Arrange (TDD)   Given (BDD)
            setOfNextMove(movement, coordinate);

            // Act (TDD)   When (BDD)
            game.nextBestMove();

            //Assert (TDD)   Then (BDD) 
            Assert.AreEqual(winner, game.board(winningPosition));

        }



        [TestCase('E', new char[] { 'X', 'O', 'X', 'O', 'X', 'O' }, new int[] { 0, 8, 1, 4, 2, 7 }, 7)]
        [TestCase('E', new char[] { 'X', 'O', 'X', 'O', 'X', 'O', 'X', 'O' }, new int[] { 0, 3, 8, 4, 1, 7, 2, 5 }, 5)]
        public void noMoreMovesIfanyWinner(char emptyCell, char[] movement, int[] coordinate, int nextMove)
        {
            setOfNextMove(movement, coordinate);

            Assert.AreEqual(emptyCell, game.board(nextMove));
        }

        [Test]
        public void loadingTheBoard()
        {
            var gateway = Substitute.For<IDBgateway>();

            TicTacToeClass newgame = new TicTacToeClass(gateway);

            newgame.nextMove('X', 0);

            newgame.SaveTheBoard();

            newgame.nextMove('O', 1);

            char[] expectedBoard = new char[9] { 'X', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E' };

            gateway.load().Returns(expectedBoard); 

            newgame.LoadTheBoard();

            Assert.AreEqual(expectedBoard, newgame.getTheBoard());
        }

        [Test]
        public void savingTheBoard()
        {
            var gateway = Substitute.For<IDBgateway>();

            TicTacToeClass newgame = new TicTacToeClass(gateway);

            newgame.newGame();

            //newgame.nextMove('X', 0);

            newgame.SaveTheBoard();

            char[] expectedBoard = new char[9] { 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E', 'E' };

            gateway.Received().save(expectedBoard);

            //Assert.AreEqual(expectedBoard, newgame.getTheBoard());

        }


    }
}