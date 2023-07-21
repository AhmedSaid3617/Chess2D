using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {
    int _i, _j;

    GameObject _piece;
    GameObject _tile;

    public Tile(int i, int j, GameObject tile){
        _i = i;
        _j = j;
        _tile = tile;
    }

    public void makeBlack(){
        _tile.GetComponent<SpriteRenderer>().color = Color.black;
    }
}