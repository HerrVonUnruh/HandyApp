using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    public float speed = 3f;
    private Vector2 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }


    private void Update()
    {
        float distanceMoved = speed * Time.deltaTime;

        transform.Translate(Vector2.left * speed * Time.deltaTime);

    }

}
