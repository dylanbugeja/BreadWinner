﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    public void SceneClickListener(int sceneNumber)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
