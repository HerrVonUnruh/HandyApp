using UnityEngine;

public class ThrowingBall : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Rigidbody rb;
    private Vector2 touchStartPosition;
    private bool isJumping = false;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (TouchWithinObject(touch.position))
            {
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        touchStartPosition = touch.position;
                        break;

                    case TouchPhase.Moved:
                        float touchDeltaX = touch.position.x - touchStartPosition.x;
                        float touchDeltaY = touch.position.y - touchStartPosition.y;

                        float moveX = Mathf.Clamp(touchDeltaX * Time.deltaTime * moveSpeed, -moveSpeed, moveSpeed);
                        float moveY = Mathf.Clamp(touchDeltaY * Time.deltaTime * moveSpeed, -moveSpeed, moveSpeed);

                        rb.velocity = new Vector2(moveX, rb.velocity.y + moveY);

                        if (touchDeltaY > 100 && !isJumping)
                        {
                            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                            isJumping = true;
                        }
                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        rb.velocity = new Vector2(0, rb.velocity.y);
                        isJumping = false;
                        break;
                }
            }
        }
    }

    bool TouchWithinObject(Vector2 touchPosition)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                return true;
            }
        }

        return false;
    }
}
