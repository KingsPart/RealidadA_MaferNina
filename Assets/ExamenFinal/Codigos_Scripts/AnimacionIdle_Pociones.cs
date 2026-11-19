using UnityEngine;

public class AnimacionIdle_Pociones : MonoBehaviour
{
    [Header("Idle Float")]
    public float floatSpeed = 1.2f;
    public float floatAmount = 0.01f;

    private Vector3 startPosition;
    private float adjustedAmount;

    void Start()
    {
        startPosition = transform.localPosition;

        // Compensar la escala del objeto padre
        adjustedAmount = floatAmount;

        if (transform.parent != null)
        {
            float parentScaleY = transform.parent.TransformVector(Vector3.up).magnitude;

            if (parentScaleY > 0.0001f)
            {
                adjustedAmount = floatAmount / parentScaleY;
            }
        }
    }

    void Update()
    {
        float newY = Mathf.Sin(Time.time * floatSpeed) * adjustedAmount;

        transform.localPosition = startPosition + new Vector3(0, newY, 0);
    }
}
