using System;
using UnityEditor.PackageManager;
using UnityEngine;

public class TileDetect : MonoBehaviour
{
    [SerializeField] float downTileRaycastDist = .1f;
    [SerializeField] LayerMask tileLayer;

    [SerializeField] TileSpawner tileSpawner;

    bool isMerged = false;



    bool isReleased = false;
    private void Awake()
    {
        tileSpawner = FindFirstObjectByType<TileSpawner>();
    }



    private void OnEnable()
    {
        tileSpawner.tileReleased += StartRaycast;
    }

    private void OnDisable()
    {
        tileSpawner.tileReleased -= StartRaycast;

    }

    private void Update()
    {
        //CheckRaycast();
    }//


    public void StartRaycast()
    {
      // CheckRaycast();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if (isMerged)
        if (isReleased) return;
        {
            print(collision.transform.name + " CARPTI " + transform.name);
            
            isReleased = true;
            if (collision.gameObject.CompareTag("Tile"))
            {

                CheckBelow(collision.transform);


            }
        }
    }
    void CheckBelow(Transform hitObject)
    {
        
        {
            print("Hitted " + hitObject.name + " in " + transform.name);
            TileData detectedTileData = hitObject.GetComponent<TileData>();
            if (detectedTileData)
            {
                print("invoke");

                if (TileDetectManager.Instance.OnTileDetectedBelow != null)
                {
                    TileDetectManager.Instance.OnTileDetectedBelow.Invoke(GetComponent<TileData>(), detectedTileData);
                }
                else
                    print("null");
            }

            float baseWidth = hitObject.localScale.x;
            float deltaX = transform.position.x - hitObject.position.x;
            float overlap = baseWidth - Mathf.Abs(deltaX);

            if (overlap <= 0f)
            {
                // hiçbir hizalama yok, doðrudan oyun biter
                Debug.Log("Missed completely!");
                return;
            }
            else
            {
                print("Cut if above certain threshold" + overlap + " hit: " + hitObject.name);
                print("CheckBelow called");
                // ... other code ...
                print("Checking TileCutter.instance");
                if (TileCutter.instance == null)
                {
                    print("INSTANCE NULL");
                }
                TileCutter.instance.CutTile(transform, hitObject);

                tileSpawner.tileReleased -= StartRaycast;
            }

        }
    }

    void CheckRaycast()
    {
        bool isHit = Physics.Raycast(transform.position, Vector3.down, out RaycastHit hitInfo, downTileRaycastDist, tileLayer);
        if (isHit)
        {
            print("Hitted " + hitInfo.transform.name + " in " + transform.name);
            TileData detectedTileData = hitInfo.transform.GetComponent<TileData>();
            if (detectedTileData)
            {
                print("invoke");

                if (TileDetectManager.Instance.OnTileDetectedBelow != null)
                {
                    TileDetectManager.Instance.OnTileDetectedBelow.Invoke(GetComponent<TileData>(), detectedTileData);
                }
                else
                    print("null");
            }

            float baseWidth = hitInfo.transform.localScale.x;
            float deltaX = transform.position.x - hitInfo.transform.position.x;
            float overlap = baseWidth - Mathf.Abs(deltaX);

            if (overlap <= 0f)
            {
                // hiçbir hizalama yok, doðrudan oyun biter
                Debug.Log("Missed completely!");
                return;
            }
            else
            {
                print("Cut if above certain threshold"+ overlap + " hit: "+hitInfo.transform.name);
                TileCutter.instance.CutTile(transform, hitInfo.transform);

                tileSpawner.tileReleased -= StartRaycast;
            }

        }
    }

    public void SetMerged(bool v) => isMerged = v;
}
