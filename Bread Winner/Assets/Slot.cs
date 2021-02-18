using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public int num;

    private void Start()
    {
        num = int.Parse(gameObject.name.Substring(gameObject.name.IndexOf("_") + 1));
    }
}
