using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] public float speed = 5f;
    [SerializeField] public float rotationSpeed = 100f;
    [SerializeField] public float maxSpeed = 10f;

    private float currentSpeed = 0f;

    void Update()
    {
        // Управление скоростью
        if (Input.GetKey(KeyCode.W))
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, Time.deltaTime * 2f);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            currentSpeed = Mathf.Lerp(currentSpeed, -maxSpeed / 2f, Time.deltaTime * 2f);
        }
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * 3f);
        }

        // Движение вперед/назад
        float moveStep = currentSpeed * Time.deltaTime;
        transform.Translate(Vector3.forward * moveStep);

        // Поворот
        if (Mathf.Abs(currentSpeed) > 0.1f) // Поворачиваем только когда движемся
        {
            float turnAmount = 0f;

            if (Input.GetKey(KeyCode.D))
            {
                turnAmount = 1f;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                turnAmount = -1f;
            }

            // Чем выше скорость, тем быстрее поворот
            float turnSpeed = rotationSpeed * Mathf.Abs(currentSpeed) / maxSpeed;
            transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
        }
    }
}