using UnityEngine;

public class CollisionGround : MonoBehaviour
{
    [Header("Referência do Ground")]
    public bool isGround;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isGround = true;
            Debug.Log(isGround);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            isGround = false;
            Debug.Log(isGround);
        }
    }
}
