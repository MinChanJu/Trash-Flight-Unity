using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex = 0;

    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.05f;
    private float lastShotTime = 0f;

    private Vector3 screenSize;

    void Start() {
        screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
    }

    // Update is called once per frame
    void Update()
    {
        // float horizontalInput = Input.GetAxisRaw("Horizontal");
        // float verticalInput = Input.GetAxisRaw("Vertical");
        // Vector3 moveTo = new Vector3(horizontalInput, 0f, 0f);
        // transform.position += moveTo * moveSpeed * Time.deltaTime;

        // Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        // if (Input.GetKey(KeyCode.LeftArrow)) {
        //     transform.position -= moveTo;
        // } else if (Input.GetKey(KeyCode.RightArrow)) {
        //     transform.position += moveTo;
        // }

        // Debug.Log(Input.mousePosition);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        // Debug.Log(mousePos);
        float toX = Mathf.Clamp(mousePos.x, -screenSize.x+0.5f, screenSize.x-0.5f);
        // Debug.Log(screenSize);
        transform.position = new Vector3(toX, transform.position.y, transform.position.z);

        if (GameManager.instance.isGamOver == false) {
            Shoot();
        }
    }

    void Shoot() {
        if (Time.time - lastShotTime > shootInterval) {
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);
            lastShotTime = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss") {
            GameManager.instance.SetGameOver(false);
            Destroy(gameObject);
        } else if (other.gameObject.tag == "Coin") {
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);

        }
    }

    public void Upgrade() {
        weaponIndex++;
        if (weaponIndex >= weapons.Length) {
            weaponIndex = weapons.Length - 1;
        }
    }
}
