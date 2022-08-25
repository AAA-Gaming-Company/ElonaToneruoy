using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyManager : MonoBehaviour
{
    public int enemiesToSpawn = 4;
    public Tilemap wallTilemap;
    public Tilemap floorTilemap;
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

        int x = Random.Range(tilemapBounds.position.x, tilemapBounds.position.x + tilemapBounds.size.x);
        int y = Random.Range(tilemapBounds.position.y, tilemapBounds.position.y + tilemapBounds.size.y);
        int z = tilemapBounds.position.z;

        Vector3Int spawnPosition = new Vector3Int(x, y, z);

        if (wallTilemap.GetTile(spawnPosition) == null && floorTilemap.GetTile(spawnPosition))
        {
            return new Vector3(x + .5f, y + .5f, z);
        } else
        {
            return GetRandomSpawnPosition();
        }
    }
}
