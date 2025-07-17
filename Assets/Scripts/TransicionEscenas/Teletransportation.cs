using UnityEngine;

public class Teletransportation : MonoBehaviour
{
    [SerializeField] Camera camera;
    private TransitionManager transitionManager;
    public Transform puntoDestino;
    

    private void Start()
    {
        transitionManager = FindAnyObjectByType<TransitionManager>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            transitionManager.LoadTransitionOnScene(puntoDestino, other.transform, camera);
        }
    }
}
