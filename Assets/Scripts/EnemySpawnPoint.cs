using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [Header("Enemy Movement from this spawn point")]
    [SerializeField] private float _enemySpeed = 3f;

    [Header("Gizmos Visualization")]
    [SerializeField] private Color _gizmoColor = Color.green;
    [SerializeField] private float _gizmoRadius = 0.3f;
    [SerializeField] private float _directionLineLength = 1.5f;

    public Vector3 MoveDirection => transform.forward.normalized;
    public float EnemySpeed => _enemySpeed;

    private void OnDrawGizmos()
    {
        Gizmos.color = _gizmoColor;

        Gizmos.DrawSphere(transform.position, _gizmoRadius);

        Gizmos.DrawLine(
            transform.position,
            transform.position + transform.forward * _directionLineLength
        );
    }
}