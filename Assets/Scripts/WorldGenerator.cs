using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField]
    GameObject grassPrefab,waterPrefab,dirtPrefab,woodsPrefab;
    float tileSize = 1.0f; // veličina jednog "tijela" ili ćelije mreže
    float scale = 0.5f;
    float waterScale = 0.01f;
    int[,] generatedMap; // deklaracija klasičnog dvodimenzionalnog niza
    float offsetX = 10f;
    float offsetY = 20f;
    void Start()
    {
        GenerateMapWithPerlinNoise();
    }

    void GenerateMapWithPerlinNoise()
    {
        generatedMap = new int[60, 60]; 


        for (int i = 0; i < generatedMap.GetLength(0); i++)  
        {
            for (int j = 0; j < generatedMap.GetLength(1); j++)  
            {
                float noiseValue = Mathf.PerlinNoise(i * scale, j * scale);
                float waterNoise = Mathf.PerlinNoise(i * waterScale + offsetX, j * waterScale + offsetY); // dodatni šum za regiju vode

                if (waterNoise > 0.7f) // fokus na manje područje koje može sadržavati vodu
                {
                    if (noiseValue < 0.3f)
                        generatedMap[i, j] = 3;   // 0 ili neki int za vodu
                    else if (noiseValue < 0.4f)
                        generatedMap[i, j] = 4;    // 1 za dirt
                    else
                        generatedMap[i, j] = 1;   // 2 za grass
                }
                else
                {
                    // Ostala područja bez vode, više dirt i grass
                    if (noiseValue < 0.4f)
                        generatedMap[i, j] = 2;
                    else
                        generatedMap[i, j] = 1;
                }
            }
        }

        for (int i = 0; i < generatedMap.GetLength(0); i++)
        {
            for (int j = 0; j < generatedMap.GetLength(1); j++)
            {
                if (generatedMap[i, j] == 1)
                {
                    Vector3 position = new Vector3(i * tileSize, j * tileSize, 0f);
                    Instantiate(grassPrefab, position, Quaternion.identity, transform);
                }
                if (generatedMap[i, j] == 2)
                {
                    Vector3 position = new Vector3(i * tileSize, j * tileSize, 0f);
                    Instantiate(woodsPrefab, position, Quaternion.identity, transform);
                }
                if (generatedMap[i, j] == 3)
                {
                    Vector3 position = new Vector3(i * tileSize, j * tileSize, 0f);
                    Instantiate(waterPrefab, position, Quaternion.identity, transform);
                }
                if (generatedMap[i, j] == 4)
                {
                    Vector3 position = new Vector3(i * tileSize, j * tileSize, 0f);
                    Instantiate(dirtPrefab, position, Quaternion.identity, transform);
                }

            }
        }
    }

}
