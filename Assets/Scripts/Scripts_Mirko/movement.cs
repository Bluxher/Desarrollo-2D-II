using UnityEngine;

public class movement : MonoBehaviour
{
    public float speed = 3f;

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(x, y).normalized;

        transform.Translate(direction * speed * Time.deltaTime);
    }
}
