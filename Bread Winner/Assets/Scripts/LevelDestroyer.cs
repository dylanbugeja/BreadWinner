using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DestroySelf()");
    }
    IEnumerator DestroySelf()
    {
        Destroy(gameObject);
        yield return new WaitForSeconds(60f);
    }

}
