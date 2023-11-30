using UnityEngine;

public class MovingBall : MonoBehaviour
{
    public float moveSpeed = 500f;
    public float jumpForce = 500f;
    public Rigidbody rb;
    private Vector2 touchStartPosition;
    private bool isJumping = false;

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
                    float touchDeltaX = touch.position.x - touchStartPosition.x;
                    float touchDeltaY = touch.position.y - touchStartPosition.y;

                    // Bewegung basierend auf der Distanz und der Geschwindigkeit
                    float moveX = Mathf.Clamp(touchDeltaX * Time.deltaTime * moveSpeed, -moveSpeed, moveSpeed);

                    // Bewegung auf der x-Achse basierend auf der Touch-Bewegung
                    rb.velocity = new Vector2(moveX, rb.velocity.y);

                    // Springen, wenn eine vertikale Bewegung erkannt wird
                    if (touchDeltaY > 100 && !isJumping)
                    {
                        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                        isJumping = true;
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    // Setze die Geschwindigkeit auf Null, wenn der Finger den Bildschirm nicht mehr berührt
                    rb.velocity = new Vector2(0, rb.velocity.y);
                    isJumping = false;
                    break;
            }
        }
    }
}
