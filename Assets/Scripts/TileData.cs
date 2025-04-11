using UnityEngine;

public class TileData : MonoBehaviour
{
    [SerializeField] TileDataSO tileData;

    [SerializeField] int tileNum = 2;
    private void Start()
    {
       // tileData.Initialize(tileNum);
    }






    public int GetTileData() => tileNum;

    public void SetTileData(int tileNum)
    {
        this.tileNum = tileNum;
       // tileData.Initialize(tileNum);
    }


}
