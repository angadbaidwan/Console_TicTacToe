using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public enum GameSquareState
    {
        Empty,
        Circle,
        Cross
    }

    public interface IGameSquare
    {
        public char GetColumn();
        public int GetColumnIndex();
        public bool SetColumn(char columnIn); // set column value, return true if valid input
        public int GetRow();
        public int GetRowIndex();
        public bool SetRow(int rowIn); // set column value, return true if valid input
        public GameSquareState GetState();
        public void SetState(GameSquareState stateIn);
        public string GameSquareState_ToString(); // return game square state as a symbol (lower case) in form of a string
    }

    public class GameSquare : IGameSquare
    {
        private char column; // x coordinate
        private int row; // y coordinate
        private GameSquareState squareState;

        public GameSquare(char columnIn, int rowIn)
        {
            string errorMessage = "";
            if (SetColumn(columnIn) == false)
            {
                errorMessage = errorMessage + "Invalid Column: " + columnIn + "\n";
            }
            if (SetRow(rowIn) == false)
            {
                errorMessage = errorMessage + "Invalid Row: " + rowIn + "\n";
            }
            this.squareState = GameSquareState.Empty;

            if (errorMessage != "")
            {
                throw new Exception("GameSquare construction failed\n" + errorMessage);
            }
        }
        public GameSquare(char columnIn, int rowIn, GameSquareState stateIn) : this(columnIn, rowIn)
        {
            this.squareState = stateIn;
        }
        public char GetColumn()
        {
            return this.column;
        }
        public int GetColumnIndex()
        {
            int columnNum = 0;
            switch (this.GetColumn())
            {
                case 'A':
                    columnNum = 0;
                    break;
                case 'B':
                    columnNum = 1;
                    break;
                case 'C':
                    columnNum = 2;
                    break;
                default:
                    throw new Exception("Invalid GameSquare column value, GetColumnIndex() match not found");
                    break;
            }
            return columnNum;
        }
        public bool SetColumn(char columnIn)
        {
            columnIn = Char.ToUpper(columnIn);
            if ("ABC".Contains(columnIn))
            {
                this.column = columnIn;
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetRow()
        {
            return this.row;
        }
        public int GetRowIndex()
        {
            int rowNum = 0;
            switch (this.GetRow())
            {
                case 3:
                    rowNum = 0;
                    break;
                case 2:
                    rowNum = 1;
                    break;
                case 1:
                    rowNum = 2;
                    break;
                default:
                    throw new Exception("Invalid GameSquare row value, GetRowIndex() match not found");
                    break;
            }
            return rowNum;
        }
        public bool SetRow(int rowIn)
        {
            if (rowIn >= 1 && rowIn <= 3)
            {
                this.row = rowIn;
                return true;
            }
            return false;
        }
        public GameSquareState GetState()
        {
            return this.squareState;
        }
        public void SetState(GameSquareState statein)
        {
            this.squareState = statein;
        }
        public string GameSquareState_ToString() // return game square state as a symbol in form of a string
        {
            GameSquareState state = GetState();
            string returnSymbol;

            switch (state)
            {
                case GameSquareState.Empty:
                    returnSymbol = " ";
                    break;
                case GameSquareState.Circle:
                    returnSymbol = "o";
                    break;
                case GameSquareState.Cross:
                    returnSymbol = "x";
                    break;
                default:
                    returnSymbol = " ";
                    break;
            }
            return returnSymbol;
        }
    }

    public interface IGameboard
    {
        public void PrintGameboard(); // print current state of the gameboard
        public bool ValidateMove(GameSquare newMove); // validate if new move's game square is still empty and move is legal
        public bool UpdateGameboard(GameSquare newMove); //  return true if board successfully updated (check if empty square)
        public List<GameSquare> GetLegalMoveSelection(); // return set of legal moves available (empty spaces on board)
        public bool CheckWinner(GameSquare lastMove); // check winner
        public bool IsBoardFull(); // return true if board full
    }

    public class Gameboard : IGameboard
    {
        private IGameSquare[,] gameGrid = new GameSquare[3, 3];

        public Gameboard()
        {
            int rowIn = 3;
            for (int i = 0; i < 3; i++) // row increment
            {
                for (int j = 0; j < 3; j++) // column increment
                {
                    switch (j)
                    {
                        case 0:
                            gameGrid[i, j] = new GameSquare('A', rowIn);
                            break;
                        case 1:
                            gameGrid[i, j] = new GameSquare('B', rowIn);
                            break;
                        case 2:
                            gameGrid[i, j] = new GameSquare('C', rowIn);
                            break;
                        default:
                            break;
                    }
                }
                rowIn = --rowIn;
            }
        }
        public void PrintGameboard() // print TicTacToe board
        {
            int rowNum = 3;
            Console.Write('\n');
            for (int i = 0; i < 3; i++) // row increment
            {
                for (int j = 0; j < 3; j++) // column increment
                {
                    if (j == 0) Console.Write(" " + rowNum-- + "  ");
                    Console.Write(gameGrid[i, j].GameSquareState_ToString() + " ");
                    if (j != 2) Console.Write("| ");
                }
                if (i != 2) Console.Write("\n   ---+---+---\n");
            }
            Console.Write("\n\n    A   B   C \n");
        }
        public bool ValidateMove(GameSquare newMove)
        {
            int rowIndex = newMove.GetRowIndex();
            int columnIndex = newMove.GetColumnIndex();
            if (gameGrid[rowIndex, columnIndex].GetState() == GameSquareState.Empty)
            {
                return true;
            }
            return false;
        }
        public bool UpdateGameboard(GameSquare newMove)
        {
            if (!ValidateMove(newMove)) return false;
            int rowIndex = newMove.GetRowIndex();
            int columnIndex = newMove.GetColumnIndex();
            gameGrid[rowIndex, columnIndex] = newMove;
            return true;
        }
        public List<GameSquare> GetLegalMoveSelection()
        {
            List<GameSquare> availableMoves = new List<GameSquare>();
            for (int i = 0; i < 3; i++) // row increment
            {
                for (int j = 0; j < 3; j++) // column increment
                {
                    if (this.gameGrid[i, j].GetState() == GameSquareState.Empty)
                    {
                        availableMoves.Add(new GameSquare(this.gameGrid[i, j].GetColumn(), this.gameGrid[i, j].GetRow()));
                    }
                }
            }
            return availableMoves;
        }
        public bool CheckWinner(GameSquare lastMove) // check row, column, diagonal, anti-diagonal for last move to check for win
        {
            GameSquareState moveState = lastMove.GetState();
            bool win; ;

            // check row
            win = true;
            int rowIndex = lastMove.GetRowIndex();
            for (int i = 0; i < 3; i++)
            {
                if (gameGrid[rowIndex, i].GetState() != moveState)
                {
                    win = false;
                }
            }
            if (win) return true;

            // check column
            win = true;
            int columnIndex = lastMove.GetColumnIndex();
            for (int i = 0; i < 3; i++)
            {
                if (gameGrid[i, columnIndex].GetState() != moveState)
                {
                    win = false;
                }
            }
            if (win) return true;

            // check \ diagonal
            if (columnIndex == rowIndex) // only need to check if both index max for 3x3 grid, i.e. last move is one of the points on the potential winning diagonal
            {
                win = true;
                for (int i = 0; i < 3; i++)
                {
                    if (gameGrid[i, i].GetState() != moveState)
                    {
                        win = false;
                    }
                }
                if (win) return true;
            }

            // check / diagonal
            if ((columnIndex + rowIndex) == 2) // only need to check if index sum == 2 for 3x3 grid, i.e. last move is one of the points on the potential winning diagonal
            {
                win = true;
                int j = 2;
                for (int i = 0; i < 3; i++)
                {
                    if (gameGrid[i, j--].GetState() != moveState)
                    {
                        win = false;
                    }
                }
                if (win) return true;
            }
            return win;
        }
        public bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++) // row increment
            {
                for (int j = 0; j < 3; j++) // column increment
                {
                    if (this.gameGrid[i, j].GetState() == GameSquareState.Empty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
    public interface IGameAI
    {
        public GameSquareState GetSymbol();
        public GameSquare MakeRandomMove(List<GameSquare> validMoves); // Return random valid move
        //public GameSquare MakeBestMove(List<GameSquare> validMoves); // Possible future feature to let user pick difficulty and have AI play optimally (i.e. always wins or draws... no fun tho)
    }
    public class GameAI : IGameAI
    {
        private GameSquareState cpuSymbol;

        public GameAI(GameSquareState stateIn)
        {
            if(stateIn == GameSquareState.Empty)
            {
                throw new Exception("Error, invalid symbol assignment to game AI: Empty");
            }
            this.cpuSymbol = stateIn;
        }
        public GameSquareState GetSymbol()
        {
            return cpuSymbol;
        }
        public GameSquare MakeRandomMove(List<GameSquare> validMoves)
        {
            int validMoveCount = validMoves.Count;
            if (validMoveCount == 0)
            {
                throw new Exception("Error! No valid moves provided to GameAI.");
            }
            Random r = new Random();
            int rInt = r.Next(0, validMoveCount - 1);
            validMoves[rInt].SetState(this.GetSymbol());
            return validMoves[rInt];
        }
    }
}
