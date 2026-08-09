using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject train;
    [SerializeField] private float spawnInterval;
    [SerializeField] private Transform[] spawnPoints;
    private float timing;
    private int ri;

    private void Awake()
    {
        timing = 0f;
}

    private void Update()
    {
        timing += Time.deltaTime;

        if(timing >= spawnInterval)
        {
            timing = 0;
            ri = Random.Range(0, spawnPoints.Length);
            Instantiate(train, spawnPoints[ri].position, spawnPoints[ri].rotation);
        }
    }
}
