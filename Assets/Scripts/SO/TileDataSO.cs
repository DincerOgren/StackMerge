using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "Scriptable Objects/TileData")]
public class TileDataSO : ScriptableObject
{
    public int tileNum;

    public void Initialize(int tileNum)
    {
        this.tileNum = tileNum;
    }
}
