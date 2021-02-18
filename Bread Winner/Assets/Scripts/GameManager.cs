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

    public delegate void OnPlay(bool isplay);
    public OnPlay onPlay;

    public float gameSpeed = 1f;
    public bool isPlay;
    public GameObject playBtn;

    public Text timerTxt;
    public int time;

    private void Start()
    {
        isPlay = true;
        Debug.Log(isPlay);
        onPlay.Invoke(isPlay);
        StartCoroutine(AddTime());
    }



    IEnumerator AddTime()
    {
        while (isPlay) {
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
            playBtn.SetActive(true);
            isPlay = false;
            onPlay.Invoke(isPlay);
            Debug.Log("last man standing");
            StopCoroutine(AddTime());
        }
    }
}
