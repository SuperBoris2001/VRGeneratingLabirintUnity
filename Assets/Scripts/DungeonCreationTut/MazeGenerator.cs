using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    private const int dim = 41;
    private const int step = 8;

    private const int boll = 2; // room cell
    private const int start_room = 3; // start point of room x,y
    private const int wall = 1;
    private const int space = 0;

    private const int minWidth = 3;
    private const int minHeight = 3;

    public static Vector3Int roomCoords
    {
        get; private set;
    }

    public static void fillBool(bool[,] cells)
    {
        for (int i = 0; i <= cells.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= cells.GetUpperBound(1); j++)
            {
                cells[i, j] = false;
            }
        }
    }
    public static int[,] gridRoomsGen()
    {
        int[,] rooms = new int[dim, dim];

        for (int i = 0; i < dim; i++)
        {
            for (int j = 0; j < dim; j++)
            {
                rooms[i, j] = wall;
            }
        }

        int ready = 0;
        //Random generator = new Random();
        int cellsCount = (int)(Mathf.Pow((float)(dim / step), 2.0f));
        int maxRooms = cellsCount * 4 / 5;
        bool[,] cells = new bool[dim / step, dim / step];
        fillBool(cells);

        while (ready < maxRooms)
        {
            int x = Random.Range(0, dim - 2); //36
            int y = Random.Range(0, dim - 2); //2
            int cellX = x / step; //4
            int cellY = y / step; //0

            if (cells[cellY, cellX])
            {
                continue;
            }

            int roomWidth = minWidth + Random.Range(0, step - 1 - minWidth); //5
            int roomHeight = minHeight + Random.Range(0, step - 1 - minHeight); //5
            int startY = y - y % step + 1, startX = x - x % step + 1; //1, 33
            int endY = step * (y + 1), endX = step * (x + 1); //8, 40

            startY = startY + Random.Range(0, (endY - roomHeight) % step);
            startX = startX + Random.Range(0, (endX - roomWidth) % step);

            for (int i = startY; i < startY + roomHeight; i++)
            {
                for (int j = startX; j < startX + roomWidth; j++)
                {
                    rooms[i, j] = space;
                }
            }

            rooms[startX, startY] = boll;

            if (ready == 0)
                roomCoords = new Vector3Int(startX + roomWidth / 2, startY + roomHeight / 2, 0);

            cells[cellY, cellX] = true;
            ready++;
        }

        return rooms;
    }

    public static float placementThreshold;    // chance of empty space

    public static void MazeDataGenerator()
    {
        placementThreshold = .1f;
    }

    public static int[,] FromDimensions(int sizeRows = dim, int sizeCols = dim)
    {
        int[,] maze = new int[sizeRows, sizeCols];

        int rMax = maze.GetUpperBound(0);
        int cMax = maze.GetUpperBound(1);
        //Random generator = new Random();

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                // outside wall
                if (i == 0 || j == 0 || i == rMax || j == cMax)
                {
                    maze[i, j] = wall;
                }

                // every other inside space
                else if (i % 2 == space && j % 2 == space)
                {
                    //if (Random.value > placementThreshold)
                    //{
                    maze[i, j] = wall;

                    // in addition to this spot, randomly place adjacent
                    int a = Random.value < .5 ? 0 : (Random.value < .5 ? -1 : 1);
                    int b = a != 0 ? 0 : (Random.value < .5 ? -1 : 1);
                    maze[i + a, j + b] = wall;
                    //}
                }
            }
        }

        int[,] maze2 = new int[rMax + 1, cMax + 1];
        int row = rMax;

        for (int i = 0; i <= rMax; i++)
        {
            for (int j = 0; j <= cMax; j++)
            {
                maze2[i, j] = maze[row, j];
            }
            row--;
        }

        return maze2;
    }



    public static int[,] combina()
    {
        //int[,] rooms = gridRoomsGen();
        int[,] maze = FromDimensions();

        int x = Random.Range(4, 15);
        int y = Random.Range(4, 30);

        for(int i = 1; i < 7; i++)
        {
            for (int j = 1; j < 7; j++)
                maze[x + i, y + j] = boll;
        }
        maze[x, y] = start_room;

        x = Random.Range(18, 30);
        y = Random.Range(4, 30);
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
                maze[x + i, y + j] = boll;
        }
        maze[x, y] = start_room;

        //for (int i = 0; i < dim; i++)
        //{
        //    for (int j = 0; j < dim; j++)
        //    {
        //        if (rooms[i, j] == space)
        //        {
        //            maze[i, j] = space;
        //        }
        //        if (rooms[i,j] == boll)
        //        {
        //            maze[i, j] = boll;
        //        }
        //    }
        //}

        return maze;

        //int rMax = maze.GetUpperBound(0);
        //int cMax = maze.GetUpperBound(1);
        //int[,] maze2 = new int[rMax + 1, cMax + 1];
        //int row = rMax;

        //for (int i = 0; i <= rMax; i++)
        //{
        //    for (int j = 0; j <= cMax; j++)
        //    {
        //        maze2[i, j] = maze[row, j];
        //    }
        //    row--;
        //}

        //return maze2;
    }
}
