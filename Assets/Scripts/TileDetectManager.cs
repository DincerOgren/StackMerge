using UnityEngine;
using System;
public class TileDetectManager : MonoBehaviour
{
    public static TileDetectManager Instance;

    public Action<TileData,TileData> OnTileDetectedBelow;


 
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {

        DontDestroyOnLoad(gameObject);
    }
}
