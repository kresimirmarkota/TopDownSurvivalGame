using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapWorldGenerator : MonoBehaviour
{
    public TileBase grassTile, WaterTile, groundTile, forestTile;
    public Tilemap tilemap;
    private int[,] squareMap;


    public delegate void OnMapGenerated();
    public event OnMapGenerated MapGenerated;

    public int width = 50;
    public int height = 50;
    public float noiseScale = 0.1f;
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
       
       makeSquareMap();

       // CollectTilePositions(tilemap);
        
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
    void makeSquareMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Dobij Perlin noise vrijednost za trenutne koordinate
                float noiseValue = Mathf.PerlinNoise(x * noiseScale, y * noiseScale);

                Vector3Int tilePosition = new Vector3Int(x, y, 0);

                // Odaberi tile na osnovu Perlin noise vrijednosti
                if (noiseValue > 0.3f)
                {
                    tilemap.SetTile(tilePosition, grassTile);
                }
                else
                {
                    tilemap.SetTile(tilePosition, groundTile);
                }
            }
        }
        MapGenerated?.Invoke();
    }
}
   

