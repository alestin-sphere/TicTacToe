using System;
using UnityEngine;

public class BoardRules
{
    public static int CheckBoard(int[,] board, int size, int playerState, int piecesOnTheBoard)
    {
        // check board for tie = -1, ai win = 1, player win = 0
        if (piecesOnTheBoard == 9)
        {
            return -1;
        }

        if (board[0, 0] != 0)
        {
            // *|_|_
            // *|_|_
            // *| |
            if (board[0, 0] == board[0, 1] && board[0, 1] == board[0, 2])
            {
                Debug.Log("win 1");
                return board[0, 0] == playerState ? 0 : 1;
            }

            // *|*|*
            // _|_|_
            //  | |
            if (board[0, 0] == board[1, 0] && board[1, 0] == board[2, 0])
            {
                Debug.Log("win 4");
                return board[0, 0] == playerState ? 0 : 1;
            }
            
            // *|_|_
            // _|*|_
            //  | |*
            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2])
            { 
                Debug.Log("win 7");
                return board[0, 0] == playerState ? 0 : 1;
            }
        }

        if (board[2, 2] != 0)
        {
            // _|_|*
            // _|_|*
            //  | |*
            if (board[2, 0] == board[2, 1] && board[2, 1] == board[2, 2])
            {
                Debug.Log("win 3");
                return board[2, 0] == playerState ? 0 : 1;
            }

            // _|_|_
            // _|_|_
            // *|*|*
            if (board[0, 2] == board[1, 2] && board[1, 2] == board[2, 2])
            { 
                Debug.Log("win 6");
                return board[0, 2] == playerState ? 0 : 1;
            }
        }

        if (board[1, 1] != 0)
        {
            // _|*|_
            // _|*|_
            //  |*|
            if (board[1, 0] == board[1, 1] && board[1, 1] == board[1, 2])
            {
                Debug.Log("win 2");
                return board[1, 0] == playerState ? 0 : 1;
            }

            // _|_|_
            // *|*|*
            //  | |
            if (board[0, 1] == board[1, 1] && board[1, 1] == board[2, 1])
            {
                Debug.Log("win 5");
                return board[0, 1] == playerState ? 0 : 1;
            }

            // _|_|*
            // _|*|_
            // *| |
            if (board[2, 0] == board[1, 1] && board[1, 1] == board[0, 2])
            {
                Debug.Log("win 8");
                return board[0, 2] == playerState ? 0 : 1;
            }
        }

        return 10;
    }

    public static (int, int) MakeMove(int[,] board, int size, int aiState)
    {
        if (board[1, 1] == (int) TicTacToeState.none)
        {
            return (1, 1);
        }

        for (int x = 0; x < size; ++x)
        {
            for (int y = 0; y < size; ++y)
            {
                if (board[x, y] == (int) TicTacToeState.none)
                {
                    return (x, y);
                }
            }
        }

        return (0, 0);
    }
}
