using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece{
    string _type, _team;
    char _initialPosition;
    int _i, _j;
    GameObject gameObject;

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

    public GameObject GameObject{
        get {return gameObject;}
        set {gameObject = value;}

    }
}