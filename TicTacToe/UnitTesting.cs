using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class UnitTesting
    {
        public static int errorCount = 0;
        public static void RunTest()
        {
            Console.WriteLine("Commencing Unit Testing...");

            GameSquareConstructorA1();
            GameSquareConstructorD1();
            GameSquareConstructorA0();
            GameSquareConstructorA4();
            GameSquareTwoArgConstructor();
            GameSquareGetSetColumn();
            GameSquareGetSetRow();
            GameSquareGetSetState();
            GameSquareSymbolToString();

            if (errorCount != 0)
            {
                Console.WriteLine("\nUnit testing failed. Error found: " + errorCount.ToString());
            }
            else
            {
                Console.WriteLine("\nUnit testing successful. No errors found.\n");
            }
        }

        #region GameSquare Tests
        public static void GameSquareConstructorA1() // valid column and row input, expected result: successful construction
        {
            try
            {
                IGameSquare testSquare = new GameSquare('A', 1);
            }
            catch (Exception e)
            {
                Console.WriteLine("GameSquare constructor A1 test failed");
                errorCount++;
            }
        }
        public static void GameSquareConstructorD1() // invalid column input, expected result: construction failed
        {
            try
            {
                IGameSquare testSquare = new GameSquare('D', 1);
                Console.WriteLine("GameSquare constructor D1 test failed");
                errorCount++;
            }
            catch (Exception e) { }
        }
        public static void GameSquareConstructorA0() // invalid row input (min-1), expected result: contruction failed
        {
            try
            {
                IGameSquare testSquare = new GameSquare('A', 0);
                Console.WriteLine("GameSquare constructor A0 test failed");
                errorCount++;
            }
            catch (Exception e) { }
        }
        public static void GameSquareConstructorA4() // invalid row input (max+1), expected result: contruction failed
        {
            try
            {
                IGameSquare testSquare = new GameSquare('A', 4);
                Console.WriteLine("GameSquare constructor A4 test failed");
                errorCount++;
            }
            catch (Exception e) { }
        }
        public static void GameSquareTwoArgConstructor() // valid input, expected result: successful construction
        {
            try
            {
                IGameSquare testSquare = new GameSquare('B', 3, GameSquareState.Circle);
                if (testSquare.GetState() != GameSquareState.Circle)
                {
                    Console.WriteLine("GameSquare 2 arg constructor, GetState() failed - incorrect state");
                    errorCount++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("GameSquare 2 arg constructor construction failed");
                errorCount++;
            }


        }
        public static void GameSquareGetSetColumn()
        {
            IGameSquare testSquare = new GameSquare('C', 2);
            if (!testSquare.SetColumn('A'))
            {
                Console.WriteLine("GameSquare SetColumn('A') failed");
                errorCount++;
            }
            if (testSquare.GetColumn() != 'A')
            {
                Console.WriteLine("GameSquare GetColumn() 'A' failed");
                errorCount++;
            }
            if (testSquare.SetColumn('D'))
            {
                Console.WriteLine("GameSquare SetColumn('D') executed, validation failed");
                errorCount++;
            }
        }
        public static void GameSquareGetSetRow()
        {
            IGameSquare testSquare = new GameSquare('C', 2);
            if (testSquare.GetRow() != 2)
            {
                Console.WriteLine("GameSquare GetRow() '2' failed");
                errorCount++;
            }
            if (!testSquare.SetRow(1))
            {
                Console.WriteLine("GameSquare SetRow(1) failed");
                errorCount++;
            }
            if (testSquare.GetRow() != 1)
            {
                Console.WriteLine("GameSquare GetRow() 1 failed");
                errorCount++;
            }
            if (testSquare.SetRow(4))
            {
                Console.WriteLine("GameSquare SetRow(4) executed, validation failed");
                errorCount++;
            }
            if (testSquare.SetRow(0))
            {
                Console.WriteLine("GameSquare SetRow(0) executed, validation failed");
                errorCount++;
            }
        }
        public static void GameSquareGetSetState()
        {
            IGameSquare testSquare = new GameSquare('B', 1);
            if (testSquare.GetState() != GameSquareState.Empty)
            {
                Console.WriteLine("GameSquare GetState() Empty failed");
                errorCount++;
            }
            try
            {
                testSquare.SetState(GameSquareState.Cross);
            }
            catch (Exception ex)
            {
                Console.WriteLine("GameSquare SetState - Cross failed");
                errorCount++;
            }
            if (testSquare.GetState() != GameSquareState.Cross)
            {
                Console.WriteLine("GameSquare GetState() Cross failed");
                errorCount++;
            }
        }
        public static void GameSquareSymbolToString()
        {
            IGameSquare testSquare = new GameSquare('B', 3, GameSquareState.Circle);
            if (testSquare.GameSquareState_ToString() != "o")
            {
                Console.WriteLine("GameSquare GameSquareState_ToString() circle test failed");
                errorCount++;
            }
            testSquare = new GameSquare('A', 1);
            if (testSquare.GameSquareState_ToString() != " ")
            {
                Console.WriteLine("GameSquare GameSquareState_ToString() empty test failed");
                errorCount++;
            }
        }
        #endregion

        #region Gameboard Tests
        // TODO
        #endregion

        #region GameAI Tests
        // TODO
        #endregion
    }
}
