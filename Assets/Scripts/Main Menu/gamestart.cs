using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void LoadScene(int index)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(index);
    }

    private void Start()
    {
        AudioSource[] sources = GameObject.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource volume in sources)
        {
            volume.volume = PlayerPrefs.GetFloat("music", 0.3f);
        }
    }

    public void ExitApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}

