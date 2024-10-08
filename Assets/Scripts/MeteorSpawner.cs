using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawner : MonoBehaviour
{
    public GameObject[] Meteors;
    public float minDistanceBetweenMeteors; // Minimum distance between meteors
    public int lvl;

    public float rareMinRadius;  // Min distance for rare meteors
    public float rareMaxRadius;  // Max distance for rare meteors

    public float uncommonMinRadius; // Min distance for less common meteors
    public float uncommonMaxRadius; // Max distance for less common meteors

    public float commonMinRadius;   // Min distance for common meteors
    public float commonMaxRadius;  // Max distance for common meteors

    private List<Vector3> spawnPoints = new List<Vector3>();

    void Start()
    {
        Meteor(lvl);
    }

    public void Meteor(int lvl)
    {
        int meteorCount = Random.Range(10, 20);

        for (int i = 0; i < meteorCount; i++)
        {
            int randomIndex = Random.Range(0, 10);
            int randomMeteor;
            float radius;
            float angle;
            Vector3 randomSpawnPoint = Vector3.zero;

            // Determine meteor rarity and spawn distance from the center
            if (randomIndex >= 8 && randomIndex < 10)
            {
                randomMeteor = 2; // Rarest meteor type
                radius = Random.Range(rareMinRadius, rareMaxRadius); // Farthest from the center
            }
            else if (randomIndex >= 4 && randomIndex < 8)
            {
                randomMeteor = 1; // Less common meteor
                radius = Random.Range(uncommonMinRadius, uncommonMaxRadius); // Medium distance from the center
            }
            else
            {
                randomMeteor = 0; // Most common meteor
                radius = Random.Range(commonMinRadius, commonMaxRadius); // Closest to the center
            }

            // Generate random angle for position around the center of the rectangle
            angle = Random.Range(0f, 360f);

            // Calculate spawn point based on radius and angle (spreading from the center)
            float posX = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float posY = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

            randomSpawnPoint = new Vector3(posX, posY, 0); // Spawn point is calculated from the center (0,0)

            // Check if the spawn point is far enough from other meteors
            bool validSpawn = false;
            int attempts = 0;

            while (!validSpawn && attempts < 100)
            {
                validSpawn = true;
                foreach (Vector3 spawn in spawnPoints)
                {
                    if (Vector3.Distance(spawn, randomSpawnPoint) < minDistanceBetweenMeteors)
                    {
                        validSpawn = false;
                        break;
                    }
                }

                attempts++;
            }

            if (validSpawn)
            {
                spawnPoints.Add(randomSpawnPoint);
                Instantiate(Meteors[randomMeteor], randomSpawnPoint, Quaternion.identity);
            }
        }
    }
}
