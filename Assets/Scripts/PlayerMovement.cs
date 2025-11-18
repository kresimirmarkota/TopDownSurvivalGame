using NUnit.Framework;
using System.Collections.Generic;
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

    public TileMapWorldGenerator mapGenerator;
   
    void Start()
    {
        tileMap = GameObject.Find("Map").GetComponent<Tilemap>();
        allTilePositions = new List<Vector3Int>();
        transform.position = mapTransform.position;
        CollectTilePositions(tileMap);
        print(allTilePositions);
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
                }
                else
                {
                    Debug.Log("Tile not found");
                }
            }
        }
    }
      
    }

