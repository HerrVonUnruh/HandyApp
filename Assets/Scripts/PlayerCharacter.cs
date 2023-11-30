using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody rb;
    private Vector2 touchStartPosition;

    void Update()
    {
        // Überprüfen, ob der Bildschirm berührt wird
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStartPosition = touch.position;
                    break;

                case TouchPhase.Moved:
                    // Berechne die Distanz, die der Finger auf dem Bildschirm zurückgelegt hat
                    float touchDelta = touch.position.x - touchStartPosition.x;

                    // Berechne die Bewegung basierend auf der Distanz und der Geschwindigkeit
                    float move = Mathf.Clamp(touchDelta * Time.deltaTime * moveSpeed, -moveSpeed, moveSpeed);

                    // Bewegung auf der x-Achse basierend auf der Touch-Bewegung
                    rb.velocity = new Vector2(move, rb.velocity.y);
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    // Setze die Geschwindigkeit auf Null, wenn der Finger den Bildschirm nicht mehr berührt
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    break;
            }
        }
    }
}
