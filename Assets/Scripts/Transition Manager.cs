using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager instance;

    public bool transitioning { get; private set; }

    [Min(0f)]
    public float fadeSeconds = 5f;

    public Material fullScreenMaterial;

    const string propertyName = "_Cutoff";
    float cutoff
    {
        get { return fullScreenMaterial.GetFloat(propertyName); }
        set { fullScreenMaterial.SetFloat(propertyName, value); }
    }

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        FadeIn();
    }

    public void FadeIn() => StartCoroutine(Transition(1, 0));
    public void FadeOut() => StartCoroutine(Transition(0, 1));
    IEnumerator Transition(float a, float b)
    {
        transitioning = true;
        cutoff = a;
        for (float t = 0f; cutoff != b; t += Time.deltaTime/fadeSeconds)
        {
            cutoff = Mathf.Clamp01(Mathf.SmoothStep(a, b, t));
            yield return null;
        }
        transitioning = false;
    }

    public static void TransitionToScene(int index) => instance.StartCoroutine(instance._TransitionToScene(index));
    IEnumerator _TransitionToScene(int index)
    {
        yield return new WaitUntil(() => !transitioning);
        FadeOut();
        yield return new WaitUntil(() => !transitioning);

        var op = SceneManager.LoadSceneAsync(index);
        while (!op.isDone)
            yield return null;

        FadeIn();
        yield return new WaitUntil(() => !transitioning);
    }
}
