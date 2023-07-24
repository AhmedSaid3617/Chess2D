using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {
    int _i, _j;

    ChessPiece _piece;
    GameObject _tile;

    public Tile(int i, int j, GameObject tile){
        _i = i;
        _j = j;
        _tile = tile;
    }

    public void addChessPiece(ChessPiece piece){
        _piece = piece;
    }

    public void makeBlack(){
        _tile.GetComponent<SpriteRenderer>().color = Color.black;
    }

    public void makeBlue(){
        _tile.GetComponent<SpriteRenderer>().color = Color.blue;
    }
}