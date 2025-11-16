using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapWorldGenerator : MonoBehaviour
{
    public TileBase grassTile, WaterTile, groundTile, forestTile;
    private Tilemap tilemap;
    private int[,] squareMap;

    public int width = 50;
    public int height = 50;
    public float noiseScale = 0.1f;
    void Start()
    {
        tilemap = GetComponent<Tilemap>();
       
        
       makeSquareMap();
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
    }
}
   

