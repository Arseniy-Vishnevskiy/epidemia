using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraFading : MonoBehaviour
{
    public Image fadeImage;
    float fadeDuration = 1f;
    float minLoadTime = 5f;
    private void Start()
    {
        StartCoroutine(FadeIn());
    }
    public void LoadScene(int index)
    {
        StartCoroutine(LoadSceneAsync(index));
    }

    IEnumerator LoadSceneAsync(int index)
    {
        yield return StartCoroutine(FadeOut());

        float timer = 0f;
        AsyncOperation async = SceneManager.LoadSceneAsync(index);
        SceneTransitionData.entranceId = "toLVL2";
        async.allowSceneActivation = false;

        while (async.progress < 0.9f || timer < minLoadTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);

        async.allowSceneActivation = true;
    }
    IEnumerator FadeOut()
    {
        float t = 0;
        Color c = fadeImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = t / fadeDuration;
            fadeImage.color = c;
            yield return null;
        }
        c.a = 1;
        fadeImage.color = c;
    }

    IEnumerator FadeIn()
    {
        float t = 0;
        Color c = fadeImage.color;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = 1- (t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }
        c.a = 0;
        fadeImage.color = c;
    }
}
