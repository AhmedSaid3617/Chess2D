using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves
{
    public Moves()
    {

    }

    public bool outOfRange(int i, int j){
        if (i>7 || i<0 || j>7 || j<0){
            return true;
        }
        return false;
    }

    public List<(int, int)> pawnMoves(Tile tile, Tile[,] grid, int i, int j)
    {
        List<(int, int)> moves = new List<(int, int)>();

        if (tile.piece.team == "white")
        {
            if(grid[i, j+1].piece == null){
                moves.Add((i, j + 1));
            }
    
            if (i - 1 >= 0 && j + 1 < 8)
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
        }

        else {
            if(grid[i, j-1].piece == null){
                moves.Add((i, j - 1));
            }

            if (i - 1 >= 0 && j - 1 < 8)
            {
                if (grid[i - 1, j - 1].piece != null)
                {
                    moves.Add((i - 1, j - 1));
                }
            }

            if (i + 1 < 8 && j - 1 < 8)
            {
                if (grid[i + 1, j - 1].piece != null)
                {
                    moves.Add((i + 1, j - 1));
                }
            }

            if (j == 6)
            {
                moves.Add((i, j - 2));
            }

        }

        return moves;
    }

}
