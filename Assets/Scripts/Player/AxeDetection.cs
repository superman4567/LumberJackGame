using UnityEngine;

public class AxeDetection : MonoBehaviour
{
    [SerializeField] private float knockbackForce = 10.0f;
    [SerializeField] private float frontHitAngleThreshold = 60.0f;

    public bool axeHitSomething = false;
    public Transform axe;
    public int axeDamage = 20;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            axeHitSomething = true;
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("I hit an Enemy!");
            axeHitSomething = true;

            Orc_Health orc = other.gameObject.GetComponent<Orc_Health>();
            if (orc != null)
            {
                Vector3 knockbackDirection = other.contacts[0].point - transform.position;

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
    }

    public int GetAxeDamage()
    {
        return axeDamage;
    }
}
