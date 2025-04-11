using System;
using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] Transform tileHeightTracker;
    [SerializeField] float yOffsetAmount=.5f;
    [SerializeField] Transform tileParent;

    public Action tileReleased;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            ReleaseTile();
        }
    }

    private void SpawnTile()
    {
        //TileFactory.Instance.SpawnTile(tileHeightTracker.position.y * Vector3.up, 2);
        //var spawnedTile = Instantiate(tilePrefab, tileHeightTracker.position.y * Vector3.up,Quaternion.identity);


        ////

        //Vector3 temp = tileHeightTracker.position;
        //temp.y += yOffsetAmount;
        //tileHeightTracker.position = temp;

    }
    void ReleaseTile()
    {
        Transform childTile = transform.GetChild(0);
        if (childTile == null)
        {
            Debug.LogError("Child is emptyy");
            return;
        }

        childTile.parent = tileParent;
        childTile.GetComponent<Rigidbody>().useGravity = true;

        tileReleased?.Invoke();
    }
}
