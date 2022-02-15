using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Spawner : MonoBehaviour
{
    private const int wide = 10;
    private const int heigh = 20;
    private Transform[,] grid = new Transform[wide, heigh];
    [SerializeField] private List<GameObject> blocks;

    // Start is called before the first frame update
    private void Start()
    {
        Time.timeScale = 1.5f;
        spawnRandomBlock();
    }

    public void saveTakenPlaceAndSpawnNewBlock(Transform[] children)
    {
        foreach(var child in children)
        {
            var postion = child.position;
            var xIndex = Mathf.RoundToInt(postion.x) - 1;
            var yIndex = Mathf.RoundToInt(postion.y) - 1;
            grid[xIndex, yIndex] = child;
        }
        destroyIfColumnIsFull();
        spawnRandomBlock();
    }

    private void spawnRandomBlock()
    {
        var randomBlockIndex = Random.Range(0, blocks.Count - 1);
        var randomXlocation = System.Convert.ToSingle(Random.Range(3, wide - 3));
        Instantiate(blocks[randomBlockIndex], new Vector3(randomXlocation, 17), Quaternion.identity);
    }

    private void destroyIfColumnIsFull()
    {
        for(int column = 0; column < heigh; column++)
        {
            if(ifAllRowsAreFull(column))
            {
                for(int i = 0; i < wide; i++)
                {
                    Destroy(grid[i, column].gameObject);
                    grid[i, column] = null;
                }
                moveDownBlocksFromUpperColumn(column);
                Time.timeScale += 0.1f;
            }
        }
    }

    private void moveDownBlocksFromUpperColumn(int column)
    {
        for(int upperColumn = column + 1; upperColumn < heigh; upperColumn++)
        {
            //int upperColumn = column + 1;
            for(int row = 0; row < wide; row++)
            {
                if(grid[row, upperColumn] != null)
                {
                    grid[row, upperColumn].position -= new Vector3(0, 1);
                    grid[row, (upperColumn - 1)] = grid[row, upperColumn];
                    grid[row, upperColumn] = null;
                }
            }
        }
    }

    private bool ifAllRowsAreFull(int coulumnIndex)
    {
        for(int row = 0; row < wide; row++)
        {
            if(grid[row, coulumnIndex] == null)
            {
                return false;
            }
        }
        return true;
    }

    private bool IsNotValid(Transform child)
    {
        return !IsValid(child.position);
    }

    public bool IsValid(Vector2 vector)
    {
        return ifIsInWide(vector) && ifIsInHeigh(vector) && isNotTaken(vector);
    }

    private bool isNotTaken(Vector2 vector)
    {
        var xIndex = Mathf.RoundToInt(vector.x) - 1;
        var yIndex = Mathf.RoundToInt(vector.y) - 1;
        if(grid[xIndex, yIndex] != null)
        {
            return false;
        }
        return true;
    }

    private bool ifIsInWide(Vector2 vector)
    {
        return 0 < vector.x && vector.x <= wide;
    }

    private bool ifIsInHeigh(Vector2 vector)
    {
        return 0 < vector.y && vector.y <= heigh;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}