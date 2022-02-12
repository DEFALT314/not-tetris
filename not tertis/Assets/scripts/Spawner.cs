using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private static int wide = 10;
    private static int heigh = 20;
    private bool[,] grid = new bool[wide, heigh];
    [SerializeField] private List<GameObject> blocks;

    // Start is called before the first frame update
    private void Start()
    {
        spawnRandomBlock();
        foreach(var item in grid)
        {
            Debug.Log(item);
        }
    }

    private void spawnRandomBlock()
    {
        var randomBlockIndex = Random.Range(0, blocks.Count - 1);
        var randomXlocation = System.Convert.ToSingle(Random.Range(3, 37));
        Instantiate(blocks[randomBlockIndex], new Vector3(randomXlocation, 18), Quaternion.identity);
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
        return ifIsInWide(vector) && ifIsInHeigh(vector);
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