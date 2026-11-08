using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private List<Vector2Int> path = new List<Vector2Int>();

    [Header("Path Settings")]
    [SerializeField] private Vector2Int start = new Vector2Int(0, 0);
    [SerializeField] private Vector2Int goal = new Vector2Int(4, 4);

    [Header("Grid Generation Settings")]
    [SerializeField] private int gridWidth = 5;
    [SerializeField] private int gridHeight = 5;
    [SerializeField, Range(0f, 1f)] private float obstacleProbability = 0.2f;

    private int[,] grid;
    private Vector2Int next;
    private Vector2Int current;

    private Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int(1, 0),
        new Vector2Int(-1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(0, -1)
    };

    private void Start()
    {
        //generate grid and find path
        GenerateRandomGrid(gridWidth, gridHeight, obstacleProbability);

        //check if start or goal are on obstacles
        if (IsInBounds(start)) grid[start.y, start.x] = 0;
        if (IsInBounds(goal)) grid[goal.y, goal.x] = 0;

        FindPath(start, goal);
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            RunPathfinding();
        }
    }

    //generate random grid
    public void GenerateRandomGrid(int width, int height, float probability)
    {
        gridWidth = width;
        gridHeight = height;
        grid = new int[height, width];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                grid[y, x] = Random.value < probability ? 1 : 0;
            }
        }

        Debug.Log("Random grid generated.");
    }

    //add obstacle and recalculate path
    public void AddObstacle(Vector2Int position)
    {
        if (IsInBounds(position))
        {
            grid[position.y, position.x] = 1;
            FindPath(start, goal); //recalculate dynamically
            Debug.Log($"Obstacle added at: {position}");
        }
    }

    //update start and goal
    public void SetStartAndGoal(Vector2Int newStart, Vector2Int newGoal)
    {
        if (IsInBounds(newStart) && IsInBounds(newGoal))
        {
            start = newStart;
            goal = newGoal;
            grid[start.y, start.x] = 0; //clear start
            grid[goal.y, goal.x] = 0;   //clear goal
            FindPath(start, goal);
        }
    }

    private void OnDrawGizmos()
    {
        if (grid == null) return;

        float cellSize = 1f;

        //draw grid
        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                Vector3 cellPosition = new Vector3(x * cellSize, 0, y * cellSize);
                Gizmos.color = grid[y, x] == 1 ? Color.black : Color.white;
                Gizmos.DrawCube(cellPosition, new Vector3(cellSize, 0.1f, cellSize));
            }
        }

        //draw path
        foreach (var step in path)
        {
            Vector3 cellPosition = new Vector3(step.x * cellSize, 0, step.y * cellSize);
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(cellPosition, new Vector3(cellSize, 0.1f, cellSize));
        }

        //draw start and goal
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector3(start.x * cellSize, 0, start.y * cellSize), new Vector3(cellSize, 0.1f, cellSize));

        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(goal.x * cellSize, 0, goal.y * cellSize), new Vector3(cellSize, 0.1f, cellSize));
    }

    private bool IsInBounds(Vector2Int point)
    {
        return point.x >= 0 && point.x < grid.GetLength(1) && point.y >= 0 && point.y < grid.GetLength(0);
    }
    private void RunPathfinding()
    {
        GenerateRandomGrid(gridWidth, gridHeight, obstacleProbability);

        if (IsInBounds(start)) grid[start.y, start.x] = 0;
        if (IsInBounds(goal)) grid[goal.y, goal.x] = 0;

        FindPath(start, goal);
    }

    private void FindPath(Vector2Int start, Vector2Int goal)
    {
        path.Clear(); //clear previous path before recalculating

        Queue<Vector2Int> frontier = new Queue<Vector2Int>();
        frontier.Enqueue(start);

        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        cameFrom[start] = start;

        while (frontier.Count > 0)
        {
            current = frontier.Dequeue();

            if (current == goal)
            {
                break;
            }

            foreach (Vector2Int direction in directions)
            {
                next = current + direction;

                if (IsInBounds(next) && grid[next.y, next.x] == 0 && !cameFrom.ContainsKey(next))
                {
                    frontier.Enqueue(next);
                    cameFrom[next] = current;
                }
            }
        }

        if (!cameFrom.ContainsKey(goal))
        {
            Debug.Log("Path not found.");
            return;
        }

        //trace from path
        Vector2Int step = goal;
        while (step != start)
        {
            path.Add(step);
            step = cameFrom[step];
        }
        path.Add(start);
        path.Reverse();
    }
}