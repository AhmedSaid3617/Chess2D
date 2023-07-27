using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece{
    string _type, _team;
    char _initialPosition;
    int _i, _j;
    GameObject _gameObject;

    public ChessPiece(string type, string team, char initialPosition, int i, int j){
        _type = type;
        _team = team;
        _initialPosition = initialPosition;
        _i = i;
        _j = j;
    }

    public string toString(){
        return _type + " " + _team + " " + _initialPosition;
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
}