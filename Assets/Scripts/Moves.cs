using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves
{
    public Moves()
    {

    }
    public List<(int, int)> pawnMoves(Tile tile, Tile[,] grid, int i, int j)
    {
        List<(int, int)> moves = new List<(int, int)>();

        moves.Add((i, j + 1));
        if (i - 1 > 0 && j + 1 < 8)
        {
            if (grid[i - 1, j + 1].piece != null)
            {
                moves.Add((i - 1, j + 1));
            }
        }

        if (i + 1 < 8 && j + 1 < 8)
        {
            if (grid[i + 1, j + 1].piece != null)
            {
                moves.Add((i + 1, j + 1));
            }
        }

        if (j == 1)
        {
            moves.Add((i, j + 2));
        }



        return moves;
    }
}
