using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [Header("Car Settings")]
    [SerializeField] private GameObject carPrefab;

    private void Awake()
    {
        Instantiate(carPrefab);
    }
}

