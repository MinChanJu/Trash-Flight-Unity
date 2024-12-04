using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private GameObject boss;

    private float[] arrPosX;

    [SerializeField]
    private float spawnInterval = 1.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector3 screenSize = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        int mid = (int) (screenSize.x-1 / 1.1f);
        arrPosX = new float[mid*2 + 1];
        for (int i = 0; i < mid; i++) {
            arrPosX[i] = -1.1f * (mid - i);
        }
        arrPosX[mid] = 0f;
        for (int i = 1; i <= mid; i++)
        {
            arrPosX[i+mid] = 1.1f * i;
        }
        StartEnemyRoutine();
    }

    void StartEnemyRoutine() {
        StartCoroutine("EnemyRoutine");
    }

    public void StopEnemyRoutine() {
        StopCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine() {
        yield return new WaitForSeconds(3f);

        float moveSpeed = 5f;
        int spawnCount = 0;
        int enemyIndex = 0;

        while (true) {
            foreach (float posX in arrPosX) {
                SpawnEnemy(posX, enemyIndex, moveSpeed);
            }

            spawnCount++;
            if (spawnCount % 10 == 0) {
                enemyIndex++;
                moveSpeed += 2;
            }
            
            if (enemyIndex >= enemies.Length) {
                SpawnBoss();
                enemyIndex = 0;
                spawnCount = 0;
                moveSpeed = 5f;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy(float posX, int index, float moveSpeed) {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);

        if (Random.Range(0, 5) == 0) {
            index += 1;
        }

        if (index >= enemies.Length) {
            index = enemies.Length - 1;
        }

        
        GameObject enemyObject = Instantiate(enemies[index], spawnPos, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.setMoveSpeed(moveSpeed);
    }

    void SpawnBoss() {
        Instantiate(boss, transform.position, Quaternion.identity);
    }
}
