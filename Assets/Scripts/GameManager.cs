using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject tilePrefab;
    private ChessPiece[] chessPieces = new ChessPiece[32];
    

    void addInitialPieces(){
        string[] piecesTypes3 = {"rook", "knight", "bishop"};
        string[] piecesTypes2 = {"king", "queen"};
        
        for (int i=0; i<8; i++){
            chessPieces[i] = new ChessPiece("pawn", "white", (char)i, i, 2);
            chessPieces[i+8] = new ChessPiece("pawn", "black", (char)i, i, 6);
        }
        int a = 15;

        for (int i=0; i<3; i++){
            chessPieces[++a] = new ChessPiece(piecesTypes3[i], "white", 'l', i, 0);
            chessPieces[++a] = new ChessPiece(piecesTypes3[i], "black", 'l', i, 7);
            chessPieces[++a] = new ChessPiece(piecesTypes3[i], "white", 'r', 7-i, 0);
            chessPieces[++a] = new ChessPiece(piecesTypes3[i], "black", 'r', 7-i, 0);
        }

        chessPieces[++a] = new ChessPiece("queen", "white", 'n', 3, 0);
        chessPieces[++a] = new ChessPiece("queen", "black", 'n', 3, 7);
        chessPieces[++a] = new ChessPiece("king", "white", 'n', 4, 0);
        chessPieces[++a] = new ChessPiece("king", "black", 'n', 3, 7);


    }

    // Start is called before the first frame update
    void Start()
    {
        Tile tile;

        Tile[,] grid = new Tile[8, 8];

        for (int i = 0; i < 8; i++){
            for (int j = 0; j<8; j++){
                tile = new Tile(i, j, Instantiate(tilePrefab, new Vector3(i*2, j*2, 2), Quaternion.identity) );
                grid[i, j] = tile;

                if((i%2 == 0) && (j%2 == 0) || (j%2 == 1) && (i%2 == 1)){
                    grid[i, j].makeBlack();
                }
            }
        }

        addInitialPieces();

        for (int i=0; i<32; i++){
            Debug.Log(chessPieces[i].toString());
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
