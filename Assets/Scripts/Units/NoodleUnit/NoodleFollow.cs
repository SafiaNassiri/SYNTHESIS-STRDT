using UnityEngine;

public class NoodleFollow : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 3f;
    public float followDistance = 1.5f;

    void Update()
    {
        if (!player) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > followDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * followSpeed * Time.deltaTime);
        }
    }
}
