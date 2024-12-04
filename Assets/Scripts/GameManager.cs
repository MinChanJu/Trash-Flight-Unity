using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private GameObject gameOverPanel;

    [SerializeField]
    private GameObject gameSuccessPanel;

    private int coin = 0;

    [HideInInspector]
    public bool isGamOver = false;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void IncreaseCoin() {
        coin++;
        text.SetText(coin.ToString());

        if (coin % 30 == 0) {
            Player player = FindAnyObjectByType<Player>();
            if (player != null) {
                player.Upgrade();
            }
        }
    }

    public void SetGameOver(bool isWin) {
        isGamOver = true;

        EnemySpawner enemySpawner = FindAnyObjectByType<EnemySpawner>();
        if (enemySpawner != null) {
            enemySpawner.StopEnemyRoutine();
        }

        if (isWin) Invoke("ShowGameSuccessPanel", 1f);
        else Invoke("ShowGameOverPanel", 1f);
    }

    void ShowGameOverPanel() {
        gameOverPanel.SetActive(true);
    }

    void ShowGameSuccessPanel() {
        gameSuccessPanel.SetActive(true);
    }

    public void PlayAgain() {
        SceneManager.LoadScene("SampleScene");
    }
}
