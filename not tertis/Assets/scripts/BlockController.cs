using System.Collections;
using System.Linq;
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
        horizontalMovment(new Vector3(1, 0));
    }

    private void moveLeft()
    {
        horizontalMovment(new Vector3(-1, 0));
        //transform.position -= new Vector3(1, 0);
    }

    private void horizontalMovment(Vector3 movmentVector)
    {
        var currentChildrenPositon = getChildrenLocation();
        var newPostion = transform.position + movmentVector;
        if(checkIfANewPositionForChildrenIsValid(movmentVector, currentChildrenPositon))
        {
            transform.position = newPostion;
        }
    }

    private void moveDown()
    {
        var currentChildrenPositon = getChildrenLocation();
        var movmentVector = new Vector3(0, -1);
        var newPostion = transform.position + movmentVector;
        if(checkIfANewPositionForChildrenIsValid(movmentVector, currentChildrenPositon))
        {
            transform.position = newPostion;
        }
        else
        {
            Spawner.safeTakenPlace(currentChildrenPositon);
        }
    }

    private bool checkIfANewPositionForChildrenIsValid(Vector3 vector3, Transform[] children)
    {
        for(int i = 0; i < children.Length; i++)
        {
            var newPositon = children[i].position + vector3;
            if(!Spawner.IsValid(newPositon))
            {
                return false;
            }
        }
        return true;
    }

    private void rotate()
    {
        transform.Rotate(0, 0, 90, Space.World);
    }

    private Transform[] getChildrenLocation()
    {
        return transform.Cast<Transform>().ToArray();
    }
}