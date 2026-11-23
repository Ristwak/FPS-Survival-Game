using UnityEngine;

public class AttackScript : MonoBehaviour
{
    [SerializeField] private float damage = 10f;
    public float radius = 1f;
    public LayerMask layerMask;

    private readonly Collider[] hits = new Collider[10];

    void Update()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(
            transform.position,
            radius,
            hits,
            layerMask
        );

        if (hitCount > 0)
        {
            HealthScript healthScript = hits[0].GetComponent<HealthScript>();

            if (healthScript != null)
            {
                healthScript.ApplyDamage(damage);
                gameObject.SetActive(false);
            }
        }
    }
}