using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject tilePrefab;
    private ChessPiece[] chessPieces = new ChessPiece[32];
    public GameObject[] piecesPrefabs = new GameObject[12];
    private Tile[,] grid;
    public Vector2 detectionBoxSize = new Vector2(0.2f, 0.2f);
    public Camera mainCamera;
    private Vector3 mousePosition;
    private Vector2 mouseWorldPosition;
    private Collider2D selectedTileColl;
    private Tile selectedTile;
    private string[] STATES = {"white-select", "white-play", "black-select", "black-play"};
    private int currentState;

    void addInitialPieces(){
        string[] piecesTypes3 = {"rook", "knight", "bishop"};
        string[] piecesTypes2 = {"king", "queen"};
        
        for (int i=0; i<8; i++){
            chessPieces[i] = new ChessPiece("pawn", "white", (char)i, i, 1);
            chessPieces[i].gameObject = Instantiate(piecesPrefabs[0], new Vector3(-10, -10, 1), Quaternion.identity);
            chessPieces[i+8] = new ChessPiece("pawn", "black", (char)i, i, 6);
            chessPieces[i+8].gameObject = Instantiate(piecesPrefabs[6], new Vector3(-10, -10, 1), Quaternion.identity);
        }
        int a = 15;

        for (int i=0; i<3; i++){
            chessPieces[++a] = new ChessPiece(piecesTypes3[i], "white", 'l', i, 0);
            chessPieces[a].gameObject = Instantiate(piecesPrefabs[i+1], new Vector3(-10, -10, 1), Quaternion.identity);
            chessPieces[++a] = new ChessPiece(piecesTypes3[i], "black", 'l', i, 7);
            chessPieces[a].gameObject = Instantiate(piecesPrefabs[i+7], new Vector3(-10, -10, 1), Quaternion.identity);
            chessPieces[++a] = new ChessPiece(piecesTypes3[i], "white", 'r', 7-i, 0);
            chessPieces[a].gameObject = Instantiate(piecesPrefabs[i+1], new Vector3(-10, -10, 1), Quaternion.identity);
            chessPieces[++a] = new ChessPiece(piecesTypes3[i], "black", 'r', 7-i, 7);
            chessPieces[a].gameObject = Instantiate(piecesPrefabs[i+7], new Vector3(-10, -10, 1), Quaternion.identity);
        }

        chessPieces[++a] = new ChessPiece("queen", "white", 'n', 3, 0);
        chessPieces[a].gameObject = Instantiate(piecesPrefabs[4], new Vector3(-10, -10, 1), Quaternion.identity);
        chessPieces[++a] = new ChessPiece("queen", "black", 'n', 3, 7);
        chessPieces[a].gameObject = Instantiate(piecesPrefabs[10], new Vector3(-10, -10, 1), Quaternion.identity);
        chessPieces[++a] = new ChessPiece("king", "white", 'n', 4, 0);
        chessPieces[a].gameObject = Instantiate(piecesPrefabs[5], new Vector3(-10, -10, 1), Quaternion.identity);
        chessPieces[++a] = new ChessPiece("king", "black", 'n', 4, 7);
        chessPieces[a].gameObject = Instantiate(piecesPrefabs[11], new Vector3(-10, -10, 1), Quaternion.identity);
    }

    void initialRender(Tile[,] grid){
        for (int i=0; i<8; i++){
            for (int j=0; j<8; j++){
                Tile tile = grid[i,j];
                ChessPiece piece = tile.piece;
                if (piece != null){
                    piece.gameObject.transform.position = tile.gameObject.transform.position;
                }
            }
        }
    }

    void sideSelect(string side){
        for (int i = 0; i < 8; i++){
            for (int j = 0; j<8; j++){
                if(grid[i,j].piece != null){
                    if(grid[i,j].piece.team == side){
                        grid[i, j].isSelectable = true;
                        Debug.Log(i);
                    } 
                }
                
            }
        }
    }

    void findSelectedTile(){
        mousePosition = Input.mousePosition;
        Debug.Log(mousePosition.x);
        mousePosition.z = mainCamera.nearClipPlane;
        mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        selectedTileColl = Physics2D.OverlapBox(mouseWorldPosition, detectionBoxSize, 0);
        for (int i = 0; i < 8; i++){
            for (int j = 0; j<8; j++){
                if(grid[i, j].gameObject == selectedTileColl.gameObject){
                    selectedTile = grid[i, j];
                    break;
                }
            }
        }

        if(selectedTile.isSelectable){
            selectedTile.gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Tile tile;
        // Creates grid of tiles.
        grid = new Tile[8, 8];

        // Addes tiles to the grid.        
        for (int i = 0; i < 8; i++){
            for (int j = 0; j<8; j++){
                tile = new Tile(i, j, Instantiate(tilePrefab, new Vector3(i*2, j*2, 2), Quaternion.identity) );
                grid[i, j] = tile;

                if((i%2 == 0) && (j%2 == 0) || (j%2 == 1) && (i%2 == 1)){
                    grid[i, j].makeBlack();
                }
            }
        }

        // Creates ChessPiece objects.
        addInitialPieces();

        for(int k=0; k<chessPieces.Length; k++){
            int i = chessPieces[k].i;
            int j = chessPieces[k].j;
            grid[i, j].piece = chessPieces[k];
        }

        initialRender(grid);

        currentState = 0;
        
        sideSelect("black");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            findSelectedTile();
        }
    }
}
