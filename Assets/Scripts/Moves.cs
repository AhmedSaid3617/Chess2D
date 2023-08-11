using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moves
{
    public Moves()
    {

    }

    public bool outOfRange(int i, int j)
    {
        if (i > 7 || i < 0 || j > 7 || j < 0)
        {
            return true;
        }
        return false;
    }

    public bool isBlocked(int i, int j, string team, Tile[,] grid)
    {
        if (outOfRange(i, j))
        {
            return true;
        }
        else if (grid[i, j].piece != null)
        {
            if (grid[i, j].piece.team == team)
            {
                return true;
            }
        }

        return false;
    }

    public List<(int, int)> pawnMoves(Tile tile, Tile[,] grid, int i, int j)
    {
        List<(int, int)> moves = new List<(int, int)>();

        if (tile.piece.team == "white")
        {
            if (grid[i, j + 1].piece == null)
            {
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

        else
        {
            if (grid[i, j - 1].piece == null)
            {
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

    public List<(int, int)> rookMoves(Tile tile, Tile[,] grid, int i, int j)
    {
        List<(int, int)> moves = new List<(int, int)>();

        for (int k = i; k < 7 - i; k++)
        {
            if (isBlocked(k, j, tile.piece.team, grid)){
                break;
            }
            else if(grid[k, j].piece.team != tile.piece.team){
                moves.Add((k, j));
                break;
            }
            moves.Add((k, j));
        }

        for (int k = j; k < 7 - i; k++)
        {
            if (isBlocked(i, k, tile.piece.team, grid)){
                break;
            }
            else if(grid[i, k].piece.team != tile.piece.team){
                moves.Add((i, k));
                break;
            }
            moves.Add((i, k));
        }

        return moves;
    }

}
