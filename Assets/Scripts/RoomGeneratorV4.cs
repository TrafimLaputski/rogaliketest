using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class RoomGeneratorV4 : MonoBehaviour
{
    [Header("Spawn")] [SerializeField] private GameObject _player = null;

    [SerializeField] private List<GameObject> _enemy = new List<GameObject>();
    [SerializeField] private int _enemyInOneRoom = 4;

    [Header("Random settings")] [SerializeField]
    private int _seed = 0;

    [SerializeField] private bool _randomize = false;

    [Header("Map settings")] [SerializeField]
    private Tilemap _map = null;

    [SerializeField] private Tilemap _groundTiles = null;

    [SerializeField] private RuleTile _wall = null;
    [SerializeField] private List<Tile> _ground = new List<Tile>();

    [Header("Room settings")] [SerializeField]
    private int _roomCount = 5, _minRoomSize = 10, _maxRoomSize = 20, _offset = 5;

    /*#region Testing

    [SerializeField] private Tile _centerTest = null;

    #endregion*/

    private int _chunkCount = 3;

    private List<Vector2Int> _chunks = new List<Vector2Int>(); // Chunk center == room center
    private List<Vector2Int> _rooms = new List<Vector2Int>(); // Active chunks for room generation
    private List<Vector2Int> _roomsInfo = new List<Vector2Int>(); // latter i replace this List to Dictionary =)

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (_randomize)
        {
            _seed = Random.Range(0, int.MaxValue);
        }

        Random.seed = _seed;

        ChunkFormation();
    }

    private void ChunkFormation()
    {
        if (Mathf.Pow(_chunkCount, 2) < _roomCount)
        {
            while (Mathf.Pow(_chunkCount, 2) < _roomCount)
            {
                _chunkCount++;
            }
        }

        MapGeneration();
    }

    private void MapGeneration()
    {
        Vector2Int mapSize = RecalculateMapSize();
        Vector2Int centerChunk = new Vector2Int(mapSize.x / _chunkCount / 2, mapSize.y / _chunkCount / 2);
        int startX = centerChunk.x;

        for (int y = 0; y < mapSize.y; y++)
        {
            for (int x = 0; x < mapSize.x; x++)
            {
                _map.SetTile(new Vector3Int(x, y, 0), _wall);

                if (x == centerChunk.x && y == centerChunk.y)
                {
                    _chunks.Add(centerChunk);

                    centerChunk += new Vector2Int(_maxRoomSize + _offset * 2, 0);
                }
            }

            if (y == centerChunk.y)
            {
                centerChunk = new Vector2Int(startX, centerChunk.y + (_maxRoomSize + _offset * 2));
            }
        }

        RoomFormation();
    }

    private void RoomFormation()
    {
        ListClone(_chunks, _rooms);

        ShuffleList(_rooms);

        for (int i = 0; i < _roomCount; i++)
        {
            //_map.SetTile(new Vector3Int(_chunks[i].x, _chunks[i].y, 1), _centerTest);

            RoomGeneration(_rooms[i]);
        }

        SetSpawnPoint();
    }

    private Vector2Int RecalculateMapSize()
    {
        int size = (_maxRoomSize + (_offset * 2)) * _chunkCount;

        return new Vector2Int(size, size);
    }

    private void RoomGeneration(Vector2Int center)
    {
        Vector2Int roomSize = new Vector2Int(Random.Range(_minRoomSize, _maxRoomSize),
            Random.Range(_minRoomSize, _maxRoomSize));

        _roomsInfo.Add(roomSize);

        for (int y = 0; y < roomSize.y; y++)
        {
            for (int x = 0; x < roomSize.x; x++)
            {
                _map.SetTile(new Vector3Int(center.x + x - (roomSize.x / 2), center.y + y - (roomSize.y / 2), 0),
                    null);
                _groundTiles.SetTile(
                    new Vector3Int(center.x + x - (roomSize.x / 2), center.y + y - (roomSize.y / 2), 0),
                    _ground[Random.Range(0, _ground.Count)]
                );
            }
        }

        TunnelFormation();
    }

    private void SetSpawnPoint()
    {
        //First room in gen == spawn character

        try
        {
           _player.transform.position = (Vector3Int) _rooms[0];

            for (int i = 1; i < _roomCount; i++)
            {
                for (int j = 0; j < _enemyInOneRoom; j++)
                {
                    Instantiate(_enemy[Random.Range(0, _enemy.Count)]).transform.position =
                        (Vector3Int) new Vector2Int(
                            Random.Range(_rooms[i].x - (_roomsInfo[i].x / 2), _rooms[i].x + (_roomsInfo[i].x / 2)),
                            Random.Range(_rooms[i].y - (_roomsInfo[i].y / 2), _rooms[i].y + (_roomsInfo[i].y / 2))
                        );
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void TunnelFormation()
    {
        for (int n = 0; n < _chunks.Count; n += _chunkCount)
        {
            for (int i = 0; i < _chunkCount - 1; i++)
            {
                for (int j = 0; j < Vector2Int.Distance(_chunks[i], _chunks[i + 1]); j++) //x
                {
                    for (int k = 0; k < 2; k++)
                    {
                        _map.SetTile(
                            new Vector3Int(_chunks[i + n].x + j, _chunks[i + n].y + k, 0),
                            null);
                        _groundTiles.SetTile(
                            new Vector3Int(_chunks[i + n].x + j, _chunks[i + n].y + k, 0),
                            _ground[Random.Range(0, _ground.Count)]);
                    }
                }

                if (n < _chunks.Count - _chunkCount)
                {
                    for (int j = 0; j < Vector2Int.Distance(_chunks[i], _chunks[i + _chunkCount]); j++) //y
                    {
                        for (int k = 0; k < 2; k++)
                        {
                            _map.SetTile(
                                new Vector3Int(_chunks[i + n].x + k, _chunks[i + n].y + j, 0),
                                null);
                            _groundTiles.SetTile(
                                new Vector3Int(_chunks[i + n].x + k, _chunks[i + n].y + j, 0),
                                _ground[Random.Range(0, _ground.Count)]);
                        }
                    }
                }
            }
        }
    }

    private void ShuffleList<T>(List<T> list)
    {
        System.Random rand = new System.Random();

        for (int i = list.Count - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            (list[j], list[i]) = (list[i], list[j]);
        }
    }

    private void ShuffleList<T>(T[] array)
    {
        System.Random rand = new System.Random();

        for (int i = array.Length - 1; i >= 1; i--)
        {
            int j = rand.Next(i + 1);

            (array[j], array[i]) = (array[i], array[j]);
        }
    }

    private void ListClone<T>(List<T> origin, List<T> clone)
    {
        foreach (var i in origin)
        {
            clone.Add(i);
        }
    }
}