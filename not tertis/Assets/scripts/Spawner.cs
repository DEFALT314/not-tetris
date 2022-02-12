using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private static int wide = 10;
    private static int heigh = 20;
    private static bool[,] grid = new bool[wide, heigh];
    [SerializeField] private List<GameObject> blocks;

    // Start is called before the first frame update
    private void Start()
    {
        spawnRandomBlock();
    }

    private void spawnRandomBlock()
    {
        var randomBlockIndex = Random.Range(0, blocks.Count - 1);
        var randomXlocation = System.Convert.ToSingle(Random.Range(3, 37));
        Instantiate(blocks[randomBlockIndex], new Vector3(randomXlocation, 18), Quaternion.identity);
    }

    public static void safeTakenPlace(Transform[] children)
    {
        foreach(var child in children)
        {
            var postion = child.position;
            var xIndex = Mathf.RoundToInt(postion.x) - 1;
            var yIndex = Mathf.RoundToInt(postion.y) - 1;
            grid[xIndex, yIndex] = true;
        }
    }

    public static bool AreChildrenValid(Transform[] chidren)
    {
        foreach(var child in chidren)
        {
            if(IsNotValid(child))
            {
                return false;
            }
        }
        return true;
    }

    private static bool IsNotValid(Transform child)
    {
        return !IsValid(child.position);
    }

    public static bool IsValid(Vector2 vector)
    {
        return ifIsInWide(vector) && ifIsInHeigh(vector) && isNotTaken(vector);
    }

    private static bool isNotTaken(Vector2 vector)
    {
        var xIndex = Mathf.RoundToInt(vector.x) - 1;
        var yIndex = Mathf.RoundToInt(vector.y) - 1;
        if(grid[xIndex, yIndex])
        {
            return false;
        }
        return true;
    }

    private static bool ifIsInWide(Vector2 vector)
    {
        return 0 < vector.x && vector.x < wide;
    }

    private static bool ifIsInHeigh(Vector2 vector)
    {
        return 0 < vector.y && vector.y < heigh;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}