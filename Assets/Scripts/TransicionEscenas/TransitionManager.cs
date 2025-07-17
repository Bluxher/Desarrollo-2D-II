using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private float transitionDuration = 1f;
    private Animator transitionAnimator;
    private void Start()
    {
        transitionAnimator = GetComponentInChildren<Animator>();
        if (transitionAnimator == null)
        {
            Debug.LogError("Transition Animator not found on the GameObject.");
        }
    }
    private void Update()
    {

    }

    public void LoadTransition()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(Transition(nextSceneIndex));
    }

    public void LoadTransitionOnScene(Transform Destiny, Transform Origin, Camera camera)
    {
        StartCoroutine(TransitionOnScene(Destiny, Origin, camera));
    }

    public IEnumerator Transition(int sceneIndex)
    {
        transitionAnimator.SetTrigger("StartTransition");
        yield return new WaitForSeconds(transitionDuration);
        SceneManager.LoadScene(sceneIndex);
    }

    public IEnumerator TransitionOnScene(Transform Destiny, Transform Origin, Camera camera)
    {
        transitionAnimator.SetTrigger("StartTransition");
        yield return new WaitForSeconds(transitionDuration);
        Origin.transform.position = Destiny.position;
        camera.transform.position = Destiny.position;
        yield return new WaitForSeconds(1f);
        transitionAnimator.ResetTrigger("StartTransition");
        
    }
}
