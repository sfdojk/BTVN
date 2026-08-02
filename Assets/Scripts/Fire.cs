using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class Fire : MonoBehaviour
{
    [SerializeField] private GameObject train;
    [SerializeField] private float spawnInterval;
    [SerializeField] private Transform[] spawnPoints;

    private float timi = 0f;

    private void Update()
    {
        timi += Time.deltaTime;

        if(timi >= spawnInterval)
        {
            timi = 0;
            int Ri = Random.Range(0, spawnPoints.Length);
            Instantiate(train, spawnPoints[Ri].position, spawnPoints[Ri].rotation);
        }
    }
}
