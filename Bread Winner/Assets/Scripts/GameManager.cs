using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region instance
    public static GameManager instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public float gameSpeed = 1f;

    public bool isPlay;

    public Text timerTxt;
    public int time;

    IEnumerator AddTime()
    {
        while (isPlay)
        {
            time++;
            timerTxt.text = time.ToString();
            yield return new WaitForSeconds(1f);
        }
    }

    public void GameOver()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 2)
        {
            Debug.Log("last man standing");
        }
    }
}
