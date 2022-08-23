using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    public int enemiesToSpawn = 4;
    public Tilemap wallTilemap;
    public GameObject enemyPrefab;

    void Start()
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Vector3 position = GetRandomSpawnPosition();
            Instantiate(enemyPrefab, position, Quaternion.identity, transform);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        BoundsInt tilemapBounds = wallTilemap.cellBounds;
        int x = Random.Range(tilemapBounds.position.x, tilemapBounds.size.x);
        int y = Random.Range(tilemapBounds.position.y, tilemapBounds.size.y);
        int z = tilemapBounds.position.z;

        if (wallTilemap.GetTile(new Vector3Int(x, y, z)) == null)
        {
            return new Vector3(x, y, z);
        } else
        {
            return GetRandomSpawnPosition();
        }
    }
}
