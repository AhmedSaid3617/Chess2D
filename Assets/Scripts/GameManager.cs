using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
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
    private bool whitePlay;
    private Moves movesObject = new Moves();
    private List<(int, int)> availableMoves;
    private ChessPiece chosenPiece;
    private List<ChessPiece> whiteDead = new List<ChessPiece>();
    private List<ChessPiece> blackDead = new List<ChessPiece>();
    private List<ChessPiece> whiteLiving = new List<ChessPiece>();
    private List<ChessPiece> blackLiving = new List<ChessPiece>();
    private List<(int, int)> whiteMoves = new List<(int, int)>();
    private List<(int, int)> blackMoves = new List<(int, int)>();
    private ChessPiece whiteKing;
    private ChessPiece blackKing;
    private bool gameOver = false;


    void addInitialPieces()
    {
        string[] piecesTypes3 = { "rook", "knight", "bishop" };

        for (int i = 0; i < 8; i++)
        {
            chessPieces[i] = new ChessPiece("pawn", "white", (char)i, i, 1);
            chessPieces[i].gameObject = Instantiate(piecesPrefabs[0], new Vector3(-10, -10, 1), Quaternion.identity);
            chessPieces[i + 8] = new ChessPiece("pawn", "black", (char)i, i, 6);
            chessPieces[i + 8].gameObject = Instantiate(piecesPrefabs[6], new Vector3(-10, -10, 1), Quaternion.identity);
        }
        int a = 15;

        for (int i = 0; i < 3; i++)
        {
            chessPieces[++a] = new ChessPiece(piecesTypes3[i], "white", 'l', i, 0);
            chessPieces[a].gameObject = Instantiate(piecesPrefabs[i + 1], new Vector3(-10, -10, 1), Quaternion.identity);
            chessPieces[++a] = new ChessPiece(piecesTypes3[i], "black", 'l', i, 7);
            chessPieces[a].gameObject = Instantiate(piecesPrefabs[i + 7], new Vector3(-10, -10, 1), Quaternion.identity);
            chessPieces[++a] = new ChessPiece(piecesTypes3[i], "white", 'r', 7 - i, 0);
            chessPieces[a].gameObject = Instantiate(piecesPrefabs[i + 1], new Vector3(-10, -10, 1), Quaternion.identity);
            chessPieces[++a] = new ChessPiece(piecesTypes3[i], "black", 'r', 7 - i, 7);
            chessPieces[a].gameObject = Instantiate(piecesPrefabs[i + 7], new Vector3(-10, -10, 1), Quaternion.identity);
        }

        chessPieces[++a] = new ChessPiece("queen", "white", 'n', 3, 0);
        chessPieces[a].gameObject = Instantiate(piecesPrefabs[4], new Vector3(-10, -10, 1), Quaternion.identity);

        chessPieces[++a] = new ChessPiece("queen", "black", 'n', 3, 7);
        chessPieces[a].gameObject = Instantiate(piecesPrefabs[10], new Vector3(-10, -10, 1), Quaternion.identity);

        chessPieces[++a] = new ChessPiece("king", "white", 'n', 4, 0);
        chessPieces[a].gameObject = Instantiate(piecesPrefabs[5], new Vector3(-10, -10, 1), Quaternion.identity);
        whiteKing = chessPieces[a];

        chessPieces[++a] = new ChessPiece("king", "black", 'n', 4, 7);
        chessPieces[a].gameObject = Instantiate(piecesPrefabs[11], new Vector3(-10, -10, 1), Quaternion.identity);
        blackKing = chessPieces[a];

        foreach (ChessPiece piece in chessPieces)
        {
            if (piece.team == "white")
            {
                whiteLiving.Add(piece);
            }
            else
            {
                blackLiving.Add(piece);
            }
        }
    }

    void quickRender(Tile[,] grid)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Tile tile = grid[i, j];
                ChessPiece piece = tile.piece;
                if (piece != null)
                {
                    piece.gameObject.transform.position = tile.gameObject.transform.position;
                }
            }
        }
    }

    void sideSelect(string side)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (grid[i, j].piece != null)
                {
                    if (grid[i, j].piece.team == side)
                    {
                        grid[i, j].isSelectable = true;
                    }
                }

            }
        }
    }

    Tile findSelectedTile()
    {
        Tile selectedTile;
        mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane;
        mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePosition);
        selectedTileColl = Physics2D.OverlapBox(mouseWorldPosition, detectionBoxSize, 0);

        if (selectedTileColl != null)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (grid[i, j].gameObject == selectedTileColl.gameObject)
                    {
                        selectedTile = grid[i, j];
                        return selectedTile;
                    }
                }
            }
        }
        return null;
    }

    void enforceState(bool state)
    {
        if (state)
        {
            sideSelect("white");
        }

        else
        {
            sideSelect("black");
        }
    }

    List<(int, int)> findMoves(Tile tile, Tile[,] grid, int recursionLevel)
    {
        List<(int, int)> moves = new List<(int, int)>();
        int i = tile.i;
        int j = tile.j;

        if (tile.piece.type == "pawn")
        {
            moves = movesObject.pawnMoves(tile, grid, i, j);
        }

        else if (tile.piece.type == "rook")
        {
            moves = movesObject.rookMoves(tile, grid, i, j);
        }

        else if (tile.piece.type == "bishop")
        {
            moves = movesObject.bishopMoves(tile, grid, i, j);
        }

        else if (tile.piece.type == "queen")
        {
            moves = movesObject.rookMoves(tile, grid, i, j);
            moves.AddRange(movesObject.bishopMoves(tile, grid, i, j));
        }

        else if (tile.piece.type == "king")
        {
            moves = movesObject.kingMoves(tile, grid, i, j);
        }

        else if (tile.piece.type == "knight")
        {
            moves = movesObject.knightMoves(tile, grid, i, j);
        }

        for (int k = moves.Count - 1; k >= 0; k--)
        {

            if (movesObject.isBlocked(moves[k].Item1, moves[k].Item2, tile.piece.team, grid))
            {
                moves.RemoveAt(k);
            }
        }
        if (recursionLevel == 0)
        {
            for (int k = moves.Count - 1; k >= 0; k--)
            {
                if (isKingThreatning(moves[k], grid, tile, recursionLevel))
                {
                    moves.RemoveAt(k);
                }
            }
        }


        return moves;
    }

    void allowMoves(List<(int, int)> moves, Tile[,] grid)
    {
        Tile tile;
        for (int k = 0; k < moves.Count; k++)
        {
            tile = grid[moves[k].Item1, moves[k].Item2];
            tile.lightUP();
            if (tile.piece != null)
            {
                tile.lightRed();
            }
        }
    }

    void resetGridColors()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (grid[i, j].piece != null)
                {
                    if (grid[i, j].piece.kingCheck)
                    {
                        grid[i, j].reset();
                        grid[i, j].lightMagenta();
                        continue;
                    }
                }

                grid[i, j].reset();
            }
        }
    }

    void killPiece(ChessPiece piece)
    {
        if (piece.team == "white")
        {
            whiteLiving.Remove(piece);
            whiteDead.Add(piece);
        }
        else
        {
            blackLiving.Remove(piece);
            blackDead.Add(piece);
        }
        Destroy(piece.gameObject);
    }

    void movePiece(ChessPiece piece, Tile[,] grid, Tile tile)
    {
        if (tile.piece != null)
        {
            killPiece(tile.piece);
        }
        tile.piece = piece;
        grid[piece.i, piece.j].removePiece();
        piece.i = tile.i;
        piece.j = tile.j;
    }

    bool isKingThreatning((int, int) move, Tile[,] grid, Tile origin, int recursionLevel)
    {
        Tile[,] new_grid = new Tile[8, 8];
        string team = origin.piece.team;
        List<(int, int)> enemy_moves = new List<(int, int)>();

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                new_grid[i, j] = grid[i, j].copy();
            }
        }

        movePiece(new_grid[origin.i, origin.j].piece, new_grid, new_grid[move.Item1, move.Item2]);

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (new_grid[i, j].piece != null)
                {
                    if (new_grid[i, j].piece.team != team)
                    {
                        enemy_moves = findMoves(new_grid[i, j], new_grid, recursionLevel + 1);
                        foreach ((int, int) m in enemy_moves)
                        {
                            if (new_grid[m.Item1, m.Item2].piece != null)
                            {
                                if (new_grid[m.Item1, m.Item2].piece.type == "king")
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
        }

        return false;

    }

    void updateMoves()
    {

        whiteMoves.Clear();
        foreach (ChessPiece piece in whiteLiving)
        {
            whiteMoves.AddRange(findMoves(grid[piece.i, piece.j], grid, 0));
            foreach ((int, int) m in whiteMoves)
            {
                Debug.Log("White: " + m);
            }
        }

        blackMoves.Clear();
        foreach (ChessPiece piece in blackLiving)
        {
            blackMoves.AddRange(findMoves(grid[piece.i, piece.j], grid, 0));
            foreach ((int, int) m in blackMoves)
            {
                Debug.Log("Black: " + m);
            }
        }
    }

    void endGame()
    {
        gameOver = true;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                grid[i, j].isSelectable = false;
                grid[i, j].isAllowed = false;
            }
        }
    }

    void kingCheck(bool white)
    {
        if (white)
        {
            (int, int) kingTile = (whiteKing.i, whiteKing.j);
            foreach ((int, int) m in blackMoves)
            {
                if (m == kingTile)
                {
                    grid[kingTile.Item1, kingTile.Item2].lightMagenta();
                    grid[kingTile.Item1, kingTile.Item2].piece.kingCheck = true;
                }
            }
        }
        else
        {
            (int, int) kingTile = (blackKing.i, blackKing.j);
            foreach ((int, int) m in whiteMoves)
            {
                if (m == kingTile)
                {
                    grid[kingTile.Item1, kingTile.Item2].lightMagenta();
                    grid[kingTile.Item1, kingTile.Item2].piece.kingCheck = true;
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Tile tile;
        // Creates grid of tiles.
        grid = new Tile[8, 8];

        // Addes tiles to the grid.        
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                tile = new Tile(i, j);
                tile.gameObject = Instantiate(tilePrefab, new Vector3(i * 2, j * 2, 2), Quaternion.identity);
                grid[i, j] = tile;

                if ((i % 2 == 0) && (j % 2 == 0) || (j % 2 == 1) && (i % 2 == 1))
                {
                    grid[i, j].makeBlack();
                }
            }
        }

        // Creates ChessPiece objects.
        addInitialPieces();

        for (int k = 0; k < chessPieces.Length; k++)
        {
            int i = chessPieces[k].i;
            int j = chessPieces[k].j;
            grid[i, j].piece = chessPieces[k];
        }

        quickRender(grid);

        whitePlay = true;

        enforceState(whitePlay);

        updateMoves();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver)
        {
            if (Input.GetMouseButtonDown(0))
            {
                enforceState(whitePlay);

                selectedTile = findSelectedTile();
                if (selectedTile != null)
                {

                    if (selectedTile.isSelectable)
                    {
                        resetGridColors();
                        enforceState(whitePlay);
                        chosenPiece = selectedTile.piece;
                        availableMoves = findMoves(selectedTile, grid, 0);
                        allowMoves(availableMoves, grid);
                    }

                    if (selectedTile.isAllowed)
                    {
                        movePiece(chosenPiece, grid, selectedTile);
                        quickRender(grid);
                        whitePlay ^= true;
                        whiteKing.kingCheck = false;
                        blackKing.kingCheck = false;
                        resetGridColors();
                        updateMoves();
                        kingCheck(whitePlay);
                    }
                }
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                resetGridColors();
            }

        }
        if (Input.GetKey(KeyCode.N))
        {
            endGame();
        }

    }
}
