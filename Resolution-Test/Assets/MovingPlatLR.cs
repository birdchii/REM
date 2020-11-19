using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatLR : MonoBehaviour
{

    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float maxHeight = 10f;
    [SerializeField] float minHeight = 3f;
    [SerializeField] bool direction = false;
    bool moveRight = true;

    void Update()
    {
        //Debug.Log(transform.position.y.ToString() + " - " + moveRight.ToString());
        if ((direction) && (transform.position.x > maxHeight))
            moveRight = false;

        if (transform.position.x < minHeight)
            moveRight = true;

        if (moveRight)
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y);

        if (transform.position.y > maxHeight)
            moveRight = false;
        else if (transform.position.y < minHeight)
            moveRight = true;

        if (moveRight)
            transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
        else
            transform.position = new Vector2(transform.position.x, transform.position.y - moveSpeed * Time.deltaTime);
    }
}

