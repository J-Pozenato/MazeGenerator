using UnityEngine;

public class OnTouchDestroy : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collision");

        if (collision.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
    }
}
