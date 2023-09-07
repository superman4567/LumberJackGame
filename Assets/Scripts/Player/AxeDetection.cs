using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class AxeDetection : MonoBehaviour
{
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Knocked");

            if (other.TryGetComponent(out Orc_Health orc))
            {
                Vector3 axeVelocity = GetComponent<Rigidbody>().velocity.normalized;

                Vector3 direction = new Vector3(axeVelocity.x, 0, axeVelocity.z);

                orc.KnockBack(direction);
            }
        }
    }

    public int GetAxeDamage()
    {
        return axeDamage;
    }
}
