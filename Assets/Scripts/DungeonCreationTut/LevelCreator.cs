using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// The purpose of this class is SOLELY to create the level layout.
/// </summary>
public class LevelCreator : MonoBehaviour
{

    public GameObject wall;
    public GameObject floor;
    public GameObject player;
    public GameObject boll;
    public GameObject enemy;
    public GameObject roomWall;
    public GameObject enemyRoom;
    public GameObject enemyRoom2;
    bool flagRoom1 = true;

    public LevelCreationData _levelCreationData;

    private HashSet<Vector2Int> _dungeonTiles;
    private HashSet<Vector2Int> _dungeonTilesWalls;
    private int[,] _maze; //

    private void Start()
    {
        _dungeonTiles = DrunkWalkerManager.CreateMap(_levelCreationData);
        _dungeonTilesWalls = DrunkWalkerManager.CreateMap(_levelCreationData);
        //////
        _maze = MazeGenerator.combina(); //

        int x_room = 0;
        int y_room = 0;
        bool room_def = false;

        for (int i = 0; i <= _maze.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= _maze.GetUpperBound(1); j++)
            {
                if (_maze[i, j] < 2) {
                    if (_maze[i, j] == 0)
                    {
                        Vector2Int newPosition = new Vector2Int(j, i);
                        _dungeonTiles.Add(newPosition);
                    }
                    else
                    {
                        Vector2Int newPosition = new Vector2Int(j, i);
                        _dungeonTilesWalls.Add(newPosition);
                    }
                }
                else if(_maze[i, j] == 3)
                {
                    x_room = i;
                    y_room = j;
                    if(flagRoom1 == true)
                    {
                        DrawRoom(x_room, y_room, enemyRoom);
                        flagRoom1 = false;
                    }
                    else
                    {
                        DrawRoom(x_room, y_room, enemyRoom2);
                    }


                }
            }
        }
        //////
        if (_levelCreationData._tileSize > 1)
        {
            _dungeonTiles = ReturnListOfScaledTiles(_dungeonTiles);
            _dungeonTilesWalls = ReturnListOfScaledTiles(_dungeonTilesWalls);
        }

        //if(true)
        //    DrawRoom(x_room, y_room, enemyRoom);

        DrawMaze(_dungeonTiles, floor);
        DrawMaze(_dungeonTilesWalls, wall);
        SpawnPlayer(_dungeonTiles, player);
    }

    public void SpawnPlayer(IEnumerable<Vector2Int> tiles, GameObject obj)
    {
        foreach (Vector2Int tileLocation in tiles)
        {
            Vector3 position = new Vector3(tileLocation.x, 0, tileLocation.y);
            Instantiate(obj, position, Quaternion.identity);
            return;
        }
    }
    public void DrawRoom(int x, int y, GameObject obj)
    {
        Vector3 position = new Vector3(y, 0, x);
        Instantiate(obj, position, Quaternion.identity);
    }


    public void DrawMaze(IEnumerable<Vector2Int> tiles, GameObject obj)
    {
        foreach (Vector2Int tileLocation in tiles)
        {
            Vector3 position = new Vector3(tileLocation.x, 0, tileLocation.y);
            Instantiate(obj, position, Quaternion.identity);
        }
    }

    private HashSet<Vector2Int> ReturnListOfScaledTiles(IEnumerable<Vector2Int> tiles)
    {
        HashSet<Vector2Int> scaledTiles = new HashSet<Vector2Int>();

        foreach (Vector2Int tileLocation in tiles)
        {
            Vector2Int startPosition = tileLocation * _levelCreationData._tileSize;
            Vector2Int newPosition;

            for (int i = 0; i < _levelCreationData._tileSize; i++)
            {
                for (int j = 0; j < _levelCreationData._tileSize; j++)
                {
                    newPosition = new Vector2Int(i, j) + new Vector2Int(startPosition.x, startPosition.y);
                    scaledTiles.Add(newPosition);
                }
            }
        }

        return scaledTiles;
    }
}



























