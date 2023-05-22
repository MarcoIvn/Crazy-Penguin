using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGenerator : MonoBehaviour
{
    public GameObject waterPrefab,trunkPrefab1, trunkPrefab2;
    public float distanceBetweenObstacles;
    private float currentDistance;
    private List<GameObject> obstacles;
    void Start()
    {
        obstacles = new List<GameObject>();
        obstacles.Add(waterPrefab);
        obstacles.Add(waterPrefab);
        obstacles.Add(trunkPrefab1);
        obstacles.Add(trunkPrefab2);
        // Obtiene el tamaño del plano en el eje Z
        float planeSizeZ = GetComponent<Renderer>().bounds.size.z;
        // Calcula la coordenada del final e inicio del plano en Z
        float endOfPlaneZ = transform.position.z + planeSizeZ / 2f;
        float startOfPlaneZ = transform.position.z - planeSizeZ / 2f;
        currentDistance = startOfPlaneZ + 5f;
        while (currentDistance < endOfPlaneZ - 4f)
        {
            int spawner = Random.Range(1, 4);
            if(spawner != 1)
            {
                // Index random para elegir que obstaculo spawnear
                int randomIndex = Random.Range(0, obstacles.Count);
                // Generar el obstáculo en una posición aleatoria a lo largo del camino
                Vector3 obstaclePosition = new Vector3(Random.Range(-7f, 7f), 0f, currentDistance);
                if (randomIndex == 2 || randomIndex == 3)
                {
                    // Genera una escala aleatoria entre 1 y 3
                    float randomScale = Random.Range(2f, 3.5f);
                    Vector3 scale = new Vector3(randomScale, randomScale, randomScale);

                    // Genera una rotación aleatoria de 90 o 270 grados en el eje Y
                    float randomRotation = Random.Range(0, 2) == 0 ? 90f : 270f;
                    Quaternion rotation = Quaternion.Euler(0f, randomRotation, 0f);
                    GameObject spawnedPrefab = Instantiate(obstacles[randomIndex], obstaclePosition, rotation);
                    spawnedPrefab.transform.localScale = scale;
                }
                else
                {
                    Instantiate(obstacles[randomIndex], obstaclePosition, Quaternion.identity);
                }
            }
            currentDistance += distanceBetweenObstacles;
        }
    }

}
