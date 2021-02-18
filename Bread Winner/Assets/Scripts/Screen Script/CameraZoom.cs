using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    private Camera cameraRef;
    private GameObject[] playerPos;

    void Start()
    {
        cameraRef = GetComponent<Camera>();
        playerPos = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log(playerPos[0].transform.position);
        Debug.Log(playerPos[1].transform.position);
        Debug.Log(cameraRef.tag);
        StartCoroutine(ZoomInOut());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator ZoomInOut()
    {
        while (true)
        {
            if (playerPos[0] != null && playerPos[1] != null)
            {

                Vector3 lookPoint = Vector3.Lerp(playerPos[0].transform.position, playerPos[1].transform.position, 0.5f);
                cameraRef.transform.LookAt(lookPoint);

                float distance = Vector3.Distance(playerPos[0].transform.position, playerPos[1].transform.position);
                if (distance > (cameraRef.orthographicSize * 2))
                {
                    cameraRef.orthographicSize += 0.05f;
                    // if (distance < (cameraRef.orthographicSize * 2)) 
                    //{
                    //    cameraRef.orthographicSize -= 0.1f;
                    //}
                }
                else

                    if (distance < (cameraRef.orthographicSize))
                {
                    cameraRef.orthographicSize -= 0.05f;
                }

                yield return new WaitForSeconds(0.02f);
            }

        }



    }
}