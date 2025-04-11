using UnityEngine;

public class TileFactory : MonoBehaviour
{
    public static TileFactory Instance;
    [SerializeField] GameObject baseTilePrefab;
    [SerializeField] Transform tileParent;

    // (Optional) If you plan to have different visuals for different values later
    [SerializeField] Material[] tileMaterials; // Index 0 => value 2, Index 1 => 4, etc.

    private void Awake()
    {
        Instance = this;

    }

    public GameObject SpawnTile(Vector3 position, int value,Transform parent=null)
    {

        GameObject tile = Instantiate(baseTilePrefab, position, Quaternion.identity);
        tile.GetComponent<TileData>().SetTileData(value);

        if (parent != null)
        {
            tile.transform.parent = parent;
        }
        else
            tile.transform.parent = tileParent;

        if ( value == 4)
        {
            tile.GetComponent<Renderer>().material.color = Color.blue;
        }
        // Example: Change material based on value
        //int index = Mathf.RoundToInt(Mathf.Log(value, 2)) - 1; // 2 => 0, 4 => 1, 8 => 2, etc.
        //if (tileMaterials != null && index >= 0 && index < tileMaterials.Length)
        //{
        //    tile.GetComponent<Renderer>().material = tileMaterials[index];
        //}
        tile.name = $"Tile_{value}";
        return tile;
    }
    
    public GameObject SpawnTile(Vector3 position, int value,Vector3 scale,Transform parent=null)
    {

        GameObject tile = Instantiate(baseTilePrefab, position, Quaternion.identity);
        tile.GetComponent<TileData>().SetTileData(value);
        tile.transform.localScale = scale;

        if (parent != null)
        {
            tile.transform.parent = parent;
        }
        else
            tile.transform.parent = tileParent;

        if ( value == 4)
        {
            tile.GetComponent<Renderer>().material.color = Color.blue;
        }
        // Example: Change material based on value
        //int index = Mathf.RoundToInt(Mathf.Log(value, 2)) - 1; // 2 => 0, 4 => 1, 8 => 2, etc.
        //if (tileMaterials != null && index >= 0 && index < tileMaterials.Length)
        //{
        //    tile.GetComponent<Renderer>().material = tileMaterials[index];
        //}
        tile.name = $"Tile_{value}";
        return tile;
    }
}
