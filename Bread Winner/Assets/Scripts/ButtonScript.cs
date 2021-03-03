using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    public void Quit()
    {
        Application.Quit(0);
    }
    public void Replay()
    {
        SceneManager.LoadScene("Kitchen");
    }
}
