using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public int players = 0;

    public float gameSpeed = 1f;
    public bool isPlay;


    public TextMeshProUGUI timerTxt;
    public int time;

    private void Start()
    {

       // playBtn.SetActive(false);
        isPlay = true;
        Debug.Log(isPlay);
        //onPlay.Invoke(isPlay);
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

    public void CheckGameOver()
    {
        Debug.Log(players);
        if (players == 1)
        {
            //playBtn.SetActive(true);
            isPlay = false;
            //onPlay.Invoke(isPlay);
            Debug.Log("last man standing");
            StopCoroutine(AddTime());
            SceneManager.LoadScene("End");
        }
    }
}
