using UnityEngine;

public class AxeDetection : MonoBehaviour
{
    [SerializeField] private float frontHitAngleThreshold = 60.0f;
    [SerializeField] private float sphereRadius = 1.0f; // Adjust the radius as needed
    [SerializeField] private float localYOffset = 0.5f;

    public bool axeHitSomething = false; // Initialize as false

    public int axeDamage = 20;

    public bool HasAxeHitSomething()
    {
        return axeHitSomething;
    }

    private void Start()
    {
        // Assign the "Axe" layer to the axe GameObject in the Inspector
        gameObject.layer = LayerMask.NameToLayer("Axe");
    }

    private void Update()
    {
        // Reset the flag at the beginning of each frame
        axeHitSomething = false;

        // Calculate the local Y-axis offset in world space
        Vector3 localOffset = new Vector3(0f, localYOffset, 0f);
        Vector3 worldOffset = transform.TransformVector(localOffset);

        // Calculate the starting position for the sphere cast
        Vector3 sphereStartPosition = transform.position + worldOffset;

        // Cast a sphere forward from the axe's position to detect orcs
        Collider[] hitColliders = Physics.OverlapSphere(sphereStartPosition, sphereRadius, LayerMask.GetMask("Enemy"));
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                // Orc hit by the axe
                axeHitSomething = true; // Set the flag to true
                OrcSingleKnockback(hitCollider);
            }
        }
    }

    private void OrcSingleKnockback(Collider orcCollider)
    {
        Orc_Health orc = orcCollider.gameObject.GetComponent<Orc_Health>();
        if (orc != null)
        {
            Vector3 knockbackDirection = orcCollider.ClosestPointOnBounds(transform.position) - transform.position;

            // Calculate angle between axe direction and orc local forward direction
            float angle = Vector3.Angle(knockbackDirection, orc.transform.TransformDirection(Vector3.forward));

            // Apply knockback only if angle is within the threshold
            if (angle < frontHitAngleThreshold)
            {
                // Only apply knockback if the orc is not currently being knocked back
                if (!orc.isKnockbackActive)
                {
                    orc.KnockBack(knockbackDirection);
                }
            }
        }
    }


    public int GetAxeDamage()
    {
        return axeDamage;
    }

    private void OnDrawGizmos()
    {
        // Visualize the sphere by drawing a Gizmos sphere
        Gizmos.color = Color.red;

        // Calculate the local Y-axis offset in world space for visualization
        Vector3 localOffset = new Vector3(0f, localYOffset, 0f);
        Vector3 worldOffset = transform.TransformVector(localOffset);

        // Calculate the starting position for the visualization sphere
        Vector3 sphereStartPosition = transform.position + worldOffset;

        Gizmos.DrawWireSphere(sphereStartPosition, sphereRadius);
    }
}
