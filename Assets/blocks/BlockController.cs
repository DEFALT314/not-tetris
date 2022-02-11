using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(automaticlyMoveDown());
    }

    private IEnumerator automaticlyMoveDown()
    {
        while(true)
        {
            moveDown();
            yield return new WaitForSeconds(1);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        var horizontalMove = Input.GetAxisRaw("Horizontal");
        Debug.Log(horizontalMove);
        if(Input.GetKey(KeyCode.S))
        {
            moveDown();
        }
        else if(Input.GetKeyDown(KeyCode.W))
        {
            rotate();
        }
        else if(Input.GetKeyDown(KeyCode.A))
        {
            moveLeft();
        }
        else if(Input.GetKeyDown(KeyCode.D))
        {
            moveRight();
        }
    }

    private void moveRight()
    {
        transform.position += new Vector3(1, 0);
    }

    private void moveLeft()
    {
        transform.position -= new Vector3(1, 0);
    }

    private void moveDown()
    {
        transform.position -= new Vector3(0, 1);
    }

    private void rotate()
    {
        transform.Rotate(0, 0, 90, Space.World);
    }
}