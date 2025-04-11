using System;
using UnityEngine;

public class TileMergeManager : MonoBehaviour
{

    public Action OnTileMerge;

    private void OnEnable()
    {
        TileDetectManager.Instance.OnTileDetectedBelow += TryMerge;
    }

    private void OnDisable()
    {
        TileDetectManager.Instance.OnTileDetectedBelow -= TryMerge;

    }


     void TryMerge(TileData currentTile, TileData belowTile)
     {
        print("Try Merge");
        // if (currentTile == null || belowTile == null) return;

        if (currentTile == null)
            print("Current empty");
        if (belowTile == null)
            print("below Mepty");
        if (currentTile.GetTileData() == belowTile.GetTileData())
        {

            Vector3 newPosition = Vector3.zero;
            newPosition.y = (belowTile.transform.position.y+belowTile.transform.localScale.y / 2);
            int newValue = currentTile.GetTileData() * 2;
            

            // Eskileri yok et
            Destroy(currentTile.gameObject);
            Destroy(belowTile.gameObject);



            //Yeni tile oluþtur
            GameObject newTileObj = TileFactory.Instance.SpawnTile(newPosition, newValue);
            newTileObj.GetComponent<Rigidbody>().useGravity = true;
            newTileObj.GetComponent<TileDetect>().SetMerged(true);
            //newTileObj.GetComponent<TileDetect>().StartRaycast();


            Debug.Log($"Merged {currentTile.GetTileData()} + {belowTile.GetTileData()} -> {newValue}");

            OnTileMerge?.Invoke();
        }
        else
            print("They Are Not The Same Data");
    }

}
