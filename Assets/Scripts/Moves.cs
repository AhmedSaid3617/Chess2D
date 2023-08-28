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

    private bool isFreePawn(int i, int j, string team, Tile[,] grid){
        if (outOfRange(i, j))
        {
            return true;
        }
        else if (grid[i, j].piece != null)
        {
            return true;
        }

        return false;
    }

    public void addTile(Tile tile, Tile[,] grid, List<(int, int)> moves, int new_i, int new_j){
        // Adds tile i, j to moves if available, otherwise adds (-1, -1) to moves.
        if (isBlocked(new_i, new_j, tile.piece.team, grid))
        {
            moves.Add((-1, -1));
        }
        else if (grid[new_i, new_j].piece != null)
        {
            if (grid[new_i, new_j].piece.team != tile.piece.team){
                moves.Add((new_i, new_j));
                moves.Add((-1, -1));
            }
            
        }
        else {
            moves.Add((new_i, new_j));
        }
    }

    private (int, int) rotateVector((int, int) vector){
        (int, int) result;
        vector.Item1 *= -1;
        result = (vector.Item2, vector.Item1);
        return result;
    }

    public List<(int, int)> pawnMoves(Tile tile, Tile[,] grid, int i, int j)
    {
        string team = tile.piece.team;
        int direction = (team == "white") ? 1 : -1;
        List<(int, int)> moves = new List<(int, int)>();

        // Forward movement.
        if (!isFreePawn(i, j+direction*1, team, grid)){
            moves.Add((i, j+direction*1));
            if(team == "white" && j==1 || team == "black" && j==6){
                if (!isFreePawn(i, j+direction*2, team, grid)){
                    moves.Add((i, j+direction*2));
                }
            }
            
        }
        // Left attack.
        if (!outOfRange(i-1, j+direction*1)){
            if (grid[i-1, j+direction*1].piece != null){
                if(grid[i-1, j+direction*1].piece.team != team){
                    moves.Add((i-1, j+direction*1));
                }
            }
        }
        

        // Right attack.
        if (!outOfRange(i+1, j+direction*1)){
            if (grid[i+1, j+direction*1].piece != null){
                if(grid[i+1, j+direction*1].piece.team != team){
                    moves.Add((i+1, j+direction*1));
                }
            }
        }
        

        return moves;
    }

    public List<(int, int)> rookMoves(Tile tile, Tile[,] grid, int i, int j)
    {
        List<(int, int)> moves = new List<(int, int)>();

        for (int k = j+1; k < 8; k++)
        {
            addTile(tile, grid, moves, new_i:i, new_j:k);
            if (moves[moves.Count-1] == (-1, -1)){
                moves.RemoveAt(moves.Count-1);
                break;
            }
        }

        for (int k = i+1; k < 8; k++)
        {
            addTile(tile, grid, moves, new_i:k, new_j:j);
            if (moves[moves.Count-1] == (-1, -1)){
                moves.RemoveAt(moves.Count-1);
                break;
            }
        }

        for (int k = j-1; k >=0; k--)
        {
            addTile(tile, grid, moves, new_i:i, new_j:k);
            if (moves[moves.Count-1] == (-1, -1)){
                moves.RemoveAt(moves.Count-1);
                break;
            }
        }

        for (int k = i-1; k >=0; k--)
        {
            addTile(tile, grid, moves, new_i:k, new_j:j);
            if (moves[moves.Count-1] == (-1, -1)){
                moves.RemoveAt(moves.Count-1);
                break;
            }
        }

        return moves;
    }

    public List<(int, int)> bishopMoves(Tile tile, Tile[,] grid, int i, int j){
        List<(int, int)> moves = new List<(int, int)>();

        for (int k=i+1, l=j+1; k<8 && l<8; k++, l++){
            addTile(tile, grid, moves, new_i:k, new_j:l);
            if (moves[moves.Count-1] == (-1, -1)){
                moves.RemoveAt(moves.Count-1);
                break;
            }
        }

        for (int k=i+1, l=j-1; k<8 && l>=0; k++, l--){
            addTile(tile, grid, moves, new_i:k, new_j:l);
            if (moves[moves.Count-1] == (-1, -1)){
                moves.RemoveAt(moves.Count-1);
                break;
            }
        }

        for (int k=i-1, l=j-1; k>=0 && l>=0; k--, l--){
            addTile(tile, grid, moves, new_i:k, new_j:l);
            if (moves[moves.Count-1] == (-1, -1)){
                moves.RemoveAt(moves.Count-1);
                break;
            }
        }

        for (int k=i-1, l=j+1; k>=0 && l<8; k--, l++){
            addTile(tile, grid, moves, new_i:k, new_j:l);
            if (moves[moves.Count-1] == (-1, -1)){
                moves.RemoveAt(moves.Count-1);
                break;
            }
        }

        return moves;
    }

    public List<(int, int)> kingMoves(Tile tile, Tile[,] grid, int i, int j){
        List<(int, int)> moves = new List<(int, int)>();

        // Sides
        for (int k = j-1; k<=j+1; k++){
            moves.Add((i-1, k));
            moves.Add((i+1, k));
        }

        moves.Add((i, j+1));
        moves.Add((i, j-1));

        return moves;
    }

    public List<(int, int)> knightMoves(Tile tile, Tile[,] grid, int i, int j){
        List<(int, int)> moves = new List<(int, int)>();

        (int, int) vector1 = (1, 2);
        (int, int) vector2 = (2, 1);

        for (int k=0; k<4; k++){
            moves.Add((i+vector1.Item1, j+vector1.Item2));
            moves.Add((i+vector2.Item1, j+vector2.Item2));

            vector1 = rotateVector(vector1);
            vector2 = rotateVector(vector2);
        }


        return moves;
    }

}
