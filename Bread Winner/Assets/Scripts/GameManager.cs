using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TrackTarget tracker;

    public delegate void OnPlay(bool isplay);
    public OnPlay onPlay;
    public int totalPlayers = 0;
    public List<GameObject> players = new List<GameObject>();
    public GameObject leadPlayer;

    public float gameSpeed = 1f;
    public bool isPlay;


    public TextMeshProUGUI timerTxt;
    public int time;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(10, 10);
       // playBtn.SetActive(false);
        isPlay = true;
        Debug.Log(isPlay);
        //onPlay.Invoke(isPlay);
        StartCoroutine(AddTime());
    }
    private void Update()
    {
        foreach (GameObject player in players)
        {
            if (player.transform.position.x > leadPlayer.transform.position.x)
            {
                tracker.AddTarget(player.transform);
                tracker.RemoveTarget(leadPlayer.transform);
                leadPlayer = player;
                tracker.leadPlayer = leadPlayer;
                Debug.Log(leadPlayer.name);
            }
        }
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
        Debug.Log(totalPlayers);
        if (totalPlayers == 1)
        {
            //playBtn.SetActive(true);
            isPlay = false;
            //Please change this line to something better *blegh*
            FindObjectOfType<PlayerManager>().Winner = players[0].name.Split('(')[0];
            //onPlay.Invoke(isPlay);

            StopCoroutine(AddTime());
            SceneManager.LoadScene("End");
        }
    }
}
