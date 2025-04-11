using DG.Tweening;
using DG.Tweening.Core;
using System;
using UnityEngine;
using UnityEngine.WSA;

public class TileSpawnMovement : MonoBehaviour
{
    [SerializeField] TileMergeManager tileMergeManager;


    [SerializeField] GameObject tilePrefab;
    [SerializeField] float _moveTarget = 1f;
    [SerializeField] float _moveDuration = 1f;
    [SerializeField] Transform tileParent;

    TileSpawner _tileSpawner;
    Tween baseLoop = null;
    Vector3 _startPos;


    float tileYSize;
    int tileCount;

    public int directionCounter = 0;

    public int deleteLater = 2;

    private void Awake()
    {
        _tileSpawner = GetComponent<TileSpawner>();
    }

    private void OnEnable()
    {
        TileCutter.instance.onTileCutComplete += OnTileRelease;
        //_tileSpawner.tileReleased += OnTileRelease;
        tileMergeManager.OnTileMerge += CalculateNewSpawnPos;
    }

    private void OnDisable()
    {

        //_tileSpawner.tileReleased -= OnTileRelease;
        tileMergeManager.OnTileMerge -= CalculateNewSpawnPos;
    }
    private void Start()
    {
        tileYSize = tilePrefab.transform.localScale.y;
        CalculateStartPos();
        SpawnNewTile();
        MoveObject(true);
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            MoveObject(true);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {

        }
        // MoveObject();
    }

    void CalculateStartPos()
    {
        // if even go with x 
        if (directionCounter % 2 == 0)
        {
            _startPos = new Vector3(_moveTarget, transform.position.y, 0);
            transform.position = _startPos;

        }

        // if odd go with z
        else
        {
            _startPos = new Vector3(0, transform.position.y, _moveTarget);
            transform.position = _startPos;

        }



    }

    void OnTileRelease()
    {
        KillMoveTween();
    }

    void KillMoveTween()
    {
        if (baseLoop == null)
        {
            print("Base loop is empty");
            return;
        }

        baseLoop.Kill();
        print("Loop killed");
        baseLoop = null;

        StartMovementAfterDrop();

    }
    private void SpawnNewTile()
    {
        Transform topTile = GetTopTile();

        TileFactory.Instance.SpawnTile(transform.position, deleteLater, transform);
    }

    private void SpawnAfterRelease()
    {
        Transform topTile = GetTopTile();
        //spawnPos.y = transform.position.y;



        //TileFactory.Instance.SpawnTile(transform.position, deleteLater, topTile.localScale, transform);
        GameObject spawnedTile = TileFactory.Instance.SpawnTile(transform.position, deleteLater, topTile.localScale, transform);

        Vector3 spawnPos = spawnedTile.transform.position;
        if (directionCounter % 2 == 0)
        {
            print("A");
            spawnPos.z = topTile.transform.position.z;
            print(spawnPos);
        }
        else
        {
            print("B");
            spawnPos.x = topTile.transform.position.x;
            print(spawnPos);
        }
        spawnedTile.transform.position = spawnPos;
    }

    void StartMovementAfterDrop()
    {

        IncreaseYOffset();
        CalculateStartPos();
        SpawnAfterRelease();
        MoveObject(true);
    }
    void IncreaseYOffset()
    {
        Vector3 temp = transform.position;
        temp.y += tileYSize;
        tileCount++;
        transform.position = temp;
    }

    void CalculateNewSpawnPos()
    {
        Vector3 temp = transform.position;
        temp.y -= tileYSize;
        tileCount--;
        transform.position = temp;


        CalculateStartPos();
        RestartBaseLoop();

    }

    void RestartBaseLoop()
    {
        if (baseLoop == null)
        {
            print("Base loop is empty");
            return;
        }

        baseLoop.Kill();
        print("Loop killed");
        baseLoop = null;

        MoveObject(!false);
    }

    private void MoveObject(bool increaseCounter)
    {
        print("Starting new loop");
        if (directionCounter % 2 == 0)
        {
            baseLoop = transform.DOMove(new Vector3(-_moveTarget, transform.position.y, 0), _moveDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.Linear);
            if (increaseCounter)
                directionCounter++;
            deleteLater += 2;

        }
        else
        {
            baseLoop = transform.DOMove(new Vector3(0, transform.position.y, -_moveTarget), _moveDuration)
           .SetLoops(-1, LoopType.Yoyo)
           .SetEase(Ease.Linear);
            if (increaseCounter)
                directionCounter++;
            deleteLater += 2;
        }



        //baseLoop = transform.DOMoveX(_xTarget, _moveDuration)
        //         .SetLoops(-1, LoopType.Yoyo)
        //         .SetEase(Ease.InOutSine);
    }

    private Transform GetTopTile()
    {
        Transform topTile = null;
        foreach (Transform child in tileParent)
        {
            topTile = child;
            if (child.transform.position.y > topTile.transform.position.y)
            {
                topTile = child;
            }

        }

        return topTile;
    }

}
