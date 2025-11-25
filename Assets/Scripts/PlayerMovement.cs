using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Transform mapTransform;

    private Tilemap tileMap;
    private TileBase tileBase;
    private Bounds bounds;
    List<Vector3Int> allTilePositions;
    List<TileBase> tileBases;
    Vector3 cellCenter;

    private Vector3 targetPosition;
    private bool moving = false;

    public TileMapWorldGenerator mapGenerator;

    void Start()
    {
        tileMap = GameObject.Find("Map").GetComponent<Tilemap>();
        allTilePositions = new List<Vector3Int>();
        transform.SetParent(tileMap.transform);

      
        transform.position = new Vector3(2, 2, 0);
    }


    void OnEnable()
    {
        mapGenerator.MapGenerated += OnMapReady;
    }
    void OnDisable()
    {
        mapGenerator.MapGenerated -= OnMapReady;
    }

    void OnMapReady()
    {
        tileMap = mapGenerator.tilemap;
        CollectTilePositions(tileMap);
       
    }
    private void Update()
    {
        playerMovement();
       
    }

    void CollectTilePositions(Tilemap tilemap)
    {
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int currentPos = new Vector3Int(x, y, 0);
                TileBase tile = tilemap.GetTile(currentPos);

                if (tile != null)
                {
                    Debug.Log("Tile found at position: " + currentPos + " Tile: " + tile.name);
                    // tileBases.Add(tile);
                    allTilePositions.Add(currentPos);
                }
                else
                {
                    Debug.Log("Tile not found");
                }
            }
        }
    }
    void playerMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int cell = tileMap.WorldToCell(mouseWorldPos);
            cellCenter = tileMap.GetCellCenterWorld(cell);

            // Pomiče objekat na centar pločice
           
            
            Debug.Log("mouse to worldpos "+mouseWorldPos);
            Debug.Log("cell "+cell);
            Debug.Log("Cellpos "+ cellCenter);
        }

        transform.position = Vector3.MoveTowards(transform.position, cellCenter, 0.1f);
    }
}

