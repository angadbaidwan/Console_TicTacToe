using System.Data;
using System.Diagnostics.Contracts;

namespace TicTacToe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //UnitTesting.RunTest();

            // Greet
            Console.WriteLine(" Welcome to Console TicTacToe!");
            bool playAgain = false;
            do
            {
                // Symbol selection prompt
                string playerSymbolSelection;
                char playerSymbolChar;
                string validSymbolSelectionOptions = "xo";
                do
                {
                    Console.WriteLine(" Please enter which symbol you would like to play as ('o' or 'x'): ");
                    playerSymbolSelection = Console.ReadLine();
                    playerSymbolChar = playerSymbolSelection.Trim().ToLower()[0];
                    if (!validSymbolSelectionOptions.Contains(playerSymbolChar))
                    {
                        Console.WriteLine(" Sorry! Invalid input, please try again.");
                    }
                } while (!validSymbolSelectionOptions.Contains(playerSymbolChar));
                // Assign symbols to use and AI player
                GameSquareState playerSymbol;
                IGameAI cpuPlayer; ;
                if (playerSymbolChar.Equals('o'))
                {
                    playerSymbol = GameSquareState.Circle;
                    cpuPlayer = new GameAI(GameSquareState.Cross);
                }
                else
                {
                    playerSymbol = GameSquareState.Cross;
                    cpuPlayer = new GameAI(GameSquareState.Circle);
                }

                // Create gameboard
                IGameboard gameboard = new Gameboard();
                int winner = 0; // default result: draw
                string playerMoveInput = "";
                IGameSquare playerMove;
                IGameSquare cpuMove;
                //Play game until draw or win
                do
                {
                    gameboard.PrintGameboard();
                    Console.WriteLine("\n Please enter your move by entering column number followed by row number (i.e. A1, C2, B3 etc.).");
                    playerMoveInput = Console.ReadLine().Trim().ToUpper();
                    try
                    {
                        playerMove = new GameSquare(playerMoveInput[0], (int)Char.GetNumericValue(playerMoveInput[1]), playerSymbol);
                        if (!gameboard.UpdateGameboard((GameSquare)playerMove))
                            throw new Exception(" Invalid player move.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("\n Invalid move, please try again.\n");
                        continue;
                    }
                    // Check player win
                    if (gameboard.CheckWinner((GameSquare)playerMove))
                    {
                        winner = 1;
                        break;
                    }
                    if (!gameboard.IsBoardFull())
                    {
                        cpuMove = cpuPlayer.MakeRandomMove(gameboard.GetLegalMoveSelection());
                        gameboard.UpdateGameboard((GameSquare)cpuMove);
                        // Check cpu win
                        if (gameboard.CheckWinner((GameSquare)cpuMove))
                        {
                            winner = 2;
                            break;
                        }
                    }
                } while (!gameboard.IsBoardFull());

                gameboard.PrintGameboard();
                // Declare result
                switch (winner)
                {
                    case 0:
                        Console.WriteLine("\n The outcome is a draw.\n");
                        break;
                    case 1:
                        Console.WriteLine("\n Congratulations! You won!\n");
                        break;
                    case 2:
                        Console.WriteLine("\n Sorry, You lost.\n");
                        break;
                }
                // Play again prompt
                Console.WriteLine("\n Would you to play again? (Y/N)");
                char playAgainRespone = Console.ReadLine().Trim().ToUpper()[0];
                if (playAgainRespone == 'Y')
                    playAgain = true;
                else
                    playAgain = false;

            } while (playAgain);
        }
    }
}