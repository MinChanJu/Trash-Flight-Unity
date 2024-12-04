using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f;
    public int damage = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }
}
