using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {
    int _i, _j;

    bool _isSelectable;

    ChessPiece _piece;
    GameObject _gameObject;
    Color _color;

    public Tile(int i, int j, GameObject gameObject){
        _i = i;
        _j = j;
        _gameObject = gameObject;
        _piece = null;
        _isSelectable = false;
    }

    public ChessPiece piece{
        get {return _piece;}
        set {_piece = value;}
    }

    public GameObject gameObject{
        get{return _gameObject;}
    }

    public bool isSelectable{
        get{return _isSelectable;}
        set{_isSelectable = value;}
    }

    public int i{
        get{return _i;}
        set{_i = value;}
    }

    public int j{
        get{return _j;}
        set{_j = value;}
    }

    public Color color{
        get{return _color;}
        set{_color = value;}
    }

    public void removePiece(){
        _piece = null;
    }

    public void makeBlack(){
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        _color = Color.black;
    }

    public void makeBlue(){
        gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
    }
}