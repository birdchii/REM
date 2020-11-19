using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float maxHeight = 10f;
    [SerializeField] float minHeight = 0f;
    bool moveRight = true;

    void Update()
    {
        //Debug.Log(transform.position.y.ToString() + " - " + moveRight.ToString());
        if (transform.position.y > maxHeight)
            moveRight = false;
        if (transform.position.y < minHeight)
            moveRight = true;

        if (moveRight)
            transform.position = new Vector2(transform.position.x, transform.position.y + moveSpeed * Time.deltaTime);
        else
            transform.position = new Vector2(transform.position.x , transform.position.y - moveSpeed * Time.deltaTime);
    }
}
