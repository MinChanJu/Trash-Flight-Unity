using UnityEngine;

public class Background : MonoBehaviour
{
    private float moveSpeed = 3f;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y < -10) {
            transform.position += new Vector3(0, 20, 0);
        }
    }
}
