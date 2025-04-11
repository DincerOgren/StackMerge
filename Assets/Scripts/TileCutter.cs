using DG.Tweening;
using System;
using UnityEditor.ShaderGraph.Serialization;
using UnityEngine;

public class TileCutter : MonoBehaviour
{
    public static TileCutter instance;


    public Action onTileCutComplete;
    public GameObject tilePrefab; // orijinal prefab
    public float tileWidth = 1f;

    int directionCounter = 0;

    private void Awake()
    {

        instance = this;
    }


    public void CutTile(Transform currentTile, Transform belowTile)
    {
        if (directionCounter % 2 == 0)
        {
            float baseWidth = belowTile.localScale.x;
            float deltaX = currentTile.position.x - belowTile.position.x;
            float overlap = baseWidth - Mathf.Abs(deltaX);

           

            float direction = Mathf.Sign(deltaX); // saða mý sola mý taþtý

            // 1. Kalan tile'ý küçült ve ortala
            Vector3 newScale = currentTile.localScale;
            newScale.x = overlap;
            currentTile.localScale = newScale;

            Vector3 newPos = currentTile.position;
            newPos.x = belowTile.position.x + (deltaX / 2f);
            currentTile.position = newPos;

            // 2. Cut Piece oluþtur
            float cutSize = baseWidth - overlap;

            GameObject cutPiece = Instantiate(tilePrefab);
            Vector3 cutScale = currentTile.localScale;
            cutScale.x = cutSize;
            cutPiece.transform.localScale = cutScale;

            Vector3 cutPos = currentTile.position;
            cutPos.x += (overlap / 2f + cutSize / 2f) * direction;
            cutPiece.transform.position = cutPos;

            cutPiece.GetComponent<Rigidbody>().useGravity = true;
            cutPiece.GetComponent<Renderer>().material.color = Color.red;

            directionCounter++;
        }
        else
        {
            float baseDepth = belowTile.localScale.z; // Changed from x to z
            float deltaZ = currentTile.position.z - belowTile.position.z; // Changed from x to z
            float overlap = baseDepth - Mathf.Abs(deltaZ);

            float direction = Mathf.Sign(deltaZ); // forward or backward

            // 1. Kalan tile'ý küçült ve ortala
            Vector3 newScale = currentTile.localScale;
            newScale.z = overlap; // Changed from x to z
            currentTile.localScale = newScale;

            Vector3 newPos = currentTile.position;
            newPos.z = belowTile.position.z + (deltaZ / 2f); // Changed from x to z
            currentTile.position = newPos;

            // 2. Cut Piece oluþtur
            float cutSize = baseDepth - overlap;

            GameObject cutPiece = Instantiate(tilePrefab);
            Vector3 cutScale = currentTile.localScale;
            cutScale.z = cutSize; // Changed from x to z
            cutPiece.transform.localScale = cutScale;

            Vector3 cutPos = currentTile.position;
            cutPos.z += (overlap / 2f + cutSize / 2f) * direction; // Changed from x to z
            cutPiece.transform.position = cutPos;

            cutPiece.GetComponent<Renderer>().material.color = Color.red;

            cutPiece.GetComponent<Rigidbody>().useGravity = true;
            cutPiece.transform.rotation = Quaternion.Euler(new Vector3(5, 0, 0));
            directionCounter++;
        }

    

        

        // 3. Rigidbody ile düþür
         // opsiyonel

        onTileCutComplete?.Invoke();
    }
}
