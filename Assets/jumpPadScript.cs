using UnityEngine;

public class jumpPadScript : MonoBehaviour
{
    public float velocityOfGettingTouchedByTheBouncePad = 10f;
    public AudioSource slimeSFX;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, velocityOfGettingTouchedByTheBouncePad);
            slimeSFX.Play();
        }
    }
}
