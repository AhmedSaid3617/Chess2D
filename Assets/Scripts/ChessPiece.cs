using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece{
    string _type, _team;
    char _initialPosition;
    int _i, _j;
    bool _kingCheck = false;
    int _numMoves = 0;
    GameObject _gameObject;

    public ChessPiece(string type, string team, char initialPosition, int i, int j){
        _type = type;
        _team = team;
        _initialPosition = initialPosition;
        _i = i;
        _j = j;
    }

    public GameObject gameObject{
        get {return _gameObject;}
        set {_gameObject = value;}
    }

    public int i{
        get{return _i;}
        set{_i = value;}
    }

    public int j{
        get{return _j;}
        set{_j = value;}
    }

    public string team{
        get {return _team;}
    }

    public string type{
        get {return _type;}
    }

    public bool kingCheck{
        get {return _kingCheck;}
        set {_kingCheck = value;}
    }

    public int numMoves{
        get {return _numMoves;}
        set {_numMoves = value;}
    }
    
    public string toString(){
        return _type + " " + _team + " " + _initialPosition;
    }

    public ChessPiece copy(){
        return new ChessPiece(_type, _team, _initialPosition, _i, _j);
    }

}