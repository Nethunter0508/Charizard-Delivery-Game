using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class Driver : MonoBehaviour
{
    [SerializeField] float currentSpeed = 5f;
    [SerializeField] float boostSpeed = 0.2f; // Small boost increase
    [SerializeField] float regularSpeed = 5f;
    [SerializeField] TMP_Text boostText;

    private Vector3 originalScale;

    void Start()
    {
        boostText.gameObject.SetActive(false);
        originalScale = transform.localScale;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Boost"))
        {
            currentSpeed += boostSpeed;

            boostText.text = "Speed: " + currentSpeed.ToString("F1");
            boostText.gameObject.SetActive(true);

            Destroy(collision.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("WorldCollision"))
        {
            currentSpeed = regularSpeed;
            boostText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Keyboard.current.wKey.isPressed)
        {
            moveDirection += Vector3.up;
        }

        if (Keyboard.current.sKey.isPressed)
        {
            moveDirection += Vector3.down;
        }

        if (Keyboard.current.aKey.isPressed)
        {
            moveDirection += Vector3.left;

            transform.localScale = new Vector3(
                Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        }

        if (Keyboard.current.dKey.isPressed)
        {
            moveDirection += Vector3.right;

            transform.localScale = new Vector3(
                -Mathf.Abs(originalScale.x),
                originalScale.y,
                originalScale.z
            );
        }

        transform.Translate(
            moveDirection.normalized * currentSpeed * Time.deltaTime,
            Space.World
        );
    }
}