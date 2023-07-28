using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves
{
    public Moves(){

    }
    public List<(int, int)> pawnMoves(Tile tile, Tile[,] grid, int i, int j)
    {
        List<(int, int)> moves = new List<(int, int)>();

        if (j == 1)
        {
            moves.Add((i, j + 2));
        }
        if (grid[i - 1, j + 1].piece != null)
        {
            moves.Add((i, j + 2));
        }
        if (grid[i + 1, j + 1].piece != null)
        {
            moves.Add((i, j + 2));
        }

        return moves;
    }
}
