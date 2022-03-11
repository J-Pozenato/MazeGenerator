using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector2 moving = new Vector2();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moving.x = moving.y = 0;

        moving.y = Input.GetAxisRaw("Vertical");
        moving.x = Input.GetAxisRaw("Horizontal");

        /*
        if (Input.GetAxis("right"))
        {
            moving.x = 1;
        }
        else if (Input.GetAxisRaw("left"))
        {
            moving.x = -1;
        }

        if (Input.GetKey("up"))
        {
            moving.y = 1;
        }
        else if (Input.GetKey("down"))
        {
            moving.y = -1;
        }
        */
    }
}