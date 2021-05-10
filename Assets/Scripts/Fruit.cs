using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject collected;

    public void DestroyFruit()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        collected.SetActive(true);
        Destroy(this.gameObject, 0.3f);
    }
}
