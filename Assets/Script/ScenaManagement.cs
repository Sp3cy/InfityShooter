using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenaManagement : MonoBehaviour
{
    public GameObject loadingScreen;

    public float loadDelayT = 1f;

    public void CaricaScena(int sceneId)
    {   
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    public void LoadSceneAfterSec(int sceneId)
    {
        // resume timescale
        StartCoroutine(LoadSceneAfterSecCoro(sceneId, loadDelayT));
    }

    private IEnumerator LoadSceneAfterSecCoro(int sceneId, float sec)
    {
        loadingScreen.SetActive(true);

        yield return new WaitForSecondsRealtime(sec);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Single);

        while (!operation.isDone)
        { 
            yield return null;
        }
    }

    private IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        loadingScreen.SetActive(true);  

        while (!operation.isDone)
        {
            // if using a slider this show actual progress
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            yield return null;
        }
    }
}
