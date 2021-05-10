using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public void Move(float speed, float dir)
    {
        if (dir < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (dir > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        transform.position += new Vector3(dir * speed * Time.deltaTime, 0f, 0f);
    }
}
