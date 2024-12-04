using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject coin;

    [SerializeField]
    private float moveSpeed = 10f;

    private float minY = -7f;

    [SerializeField]
    private int hp = 1;

    private int coins;

    public void setMoveSpeed(float moveSpeed) {
        this.moveSpeed = moveSpeed;
    }

    void Start() {
        coins = (int) hp;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if (transform.position.y < minY) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Weapon") {
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            hp -= weapon.damage;
            if (hp <= 0) {
                if (gameObject.tag == "Boss") {
                    GameManager.instance.SetGameOver(true);
                }
                Destroy(gameObject);
                for (int i = 0; i < coins; i++) {
                    Instantiate(coin, transform.position, Quaternion.identity);
                }
            }
            Destroy(other.gameObject);
        }
    }
}
