using UnityEngine;

public class ThrowingBall : MonoBehaviour
{
    public float moveSpeedMultiplier = 5f;
    public float jumpForce = 15f;
    public Rigidbody rb;
    private Vector2 lastTouchPosition;
    private float lastTouchTime;
    private bool isJumping = false;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && TouchWithinObject(touch.position))
            {
                lastTouchPosition = touch.position;
                lastTouchTime = Time.time;
            }

            if (touch.phase == TouchPhase.Moved && TouchWithinObject(touch.position))
            {
                float touchDeltaX = touch.position.x - lastTouchPosition.x;
                float touchDeltaY = touch.position.y - lastTouchPosition.y;
                float touchDeltaTime = Time.time - lastTouchTime;

                float moveX = touchDeltaX / touchDeltaTime * moveSpeedMultiplier * Time.deltaTime;
                float moveY = touchDeltaY / touchDeltaTime * moveSpeedMultiplier * Time.deltaTime;

                rb.velocity = new Vector2(moveX, rb.velocity.y + moveY);

                if (touchDeltaY > 100 && !isJumping)
                {
                    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                    isJumping = true;
                }

                lastTouchPosition = touch.position;
                lastTouchTime = Time.time;
            }

            if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
                isJumping = false;
            }
        }
    }

    bool TouchWithinObject(Vector2 touchPosition)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(touchPosition), Vector2.zero);
        return (hit.collider != null && hit.collider.gameObject == gameObject);
    }
}
