using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [Header("Car Settings")]
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private string spawnedCarName = "car 1";

    private void Awake()
    {
        if (carPrefab == null)
        {
            Debug.LogError("CarSpawner: carPrefab не назначен.");
            return;
        }

        GameObject spawnedCar = Instantiate(carPrefab);
        spawnedCar.name = spawnedCarName;
    }
}
