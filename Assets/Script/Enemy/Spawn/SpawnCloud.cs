using UnityEngine;

public class SpawnCloud : MonoBehaviour
{
    [SerializeField] private GameObject CloudObj;
    [SerializeField] private float spawnInterval = 3f;

    private float timer;

    private void Start()
    {
        Spawn();
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Spawn();
            timer = 0f;
        }
    }

    private void Spawn()
    {
        float randomY = Random.Range(-5f, 100f);
        Vector3 spawnPos = new Vector3(35f, randomY, 0f);

        Instantiate(CloudObj, spawnPos, Quaternion.identity); // ê∂ê¨
    }
}
