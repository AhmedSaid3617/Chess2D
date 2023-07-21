using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject myPiece;
    public GameObject tilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        /*Tile myTile = new Tile(0, 0, Instantiate(tilePrefab, new Vector3(10, 10, 2), Quaternion.identity) );
        myTile.makeBlack();
        */
        
        Tile[,] grid = new Tile[8, 8];

        for (int i = 0; i < 8; i++){
            for (int j = 0; j<8; j++){
                grid[i, j] = new Tile(i, j, Instantiate(tilePrefab, new Vector3(i*2, j*2, 2), Quaternion.identity) );
                if((i%2 == 0) && (j%2 != 0) || (j%2 == 0) && (i%2 != 0)){
                    grid[i, j].makeBlack();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
