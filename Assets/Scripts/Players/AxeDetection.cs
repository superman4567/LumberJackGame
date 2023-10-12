using System.Collections;
using Enemies;
using Players;
using UnityEngine;
using UnityEngine.AI;

public class AxeDetection : MonoBehaviour, IDataPersistance
{
    public static AxeDetection Instance;
    private PlayerStats playerStats;

    [Header("Axe data")]
    public bool axeHitSomething = false;
    public int defaultDamage = 20;
    public int additionalAxeDamage = 20;
    public bool explosiveRadiusT1 = false;
    public bool explosiveRadiusT2 = false;

    [SerializeField] private PlayerThrowAxe playerThrowAxe;
    [SerializeField] private PlayerCombat playerCombat;
    private SphereCollider sphereCollider;
    private BoxCollider boxCollider;

    [Header("Lifesteal")]
    public bool lifesteal = false;
    public float axeLifestealAmount;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        playerStats = FindObjectOfType<PlayerStats>(); 
    }

    private void Start()
    {
        // Assign the "Axe" layer to the axe GameObject in the Inspector
        gameObject.layer = LayerMask.NameToLayer("Axe");
        sphereCollider = GetComponent<SphereCollider>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void LoadData(GameData data)
    {
        this.lifesteal = data.lifesteal;
        this.axeLifestealAmount = data.axeLifestealAmount;
        this.defaultDamage = data.defaultDamage;
        this.explosiveRadiusT1 = data.explosiveRadiusT1;
        this.explosiveRadiusT2 = data.explosiveRadiusT2;
    }

    public void SaveData(GameData data)
    {
        data.lifesteal = this.lifesteal;
        data.axeLifestealAmount = this.axeLifestealAmount;
        data.defaultDamage = this.defaultDamage;
        data.explosiveRadiusT1 = this.explosiveRadiusT1;
        data.explosiveRadiusT2 = this.explosiveRadiusT2;
    }

    private void Update()
    {
        if(playerCombat.IsAxeThrown() == false)
        {
            sphereCollider.enabled = false;
            boxCollider.enabled = false;
        }
        else
        {
            sphereCollider.enabled = true;
            boxCollider.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        DetectEnemy(other);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Floor"))
        {
            AkSoundEngine.PostEvent("Play_Axe_Hitting_Something_Soft__Moist_", gameObject);
        }
    }

    private void DetectEnemy(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out EnemyMovement enemyMovement) && enemyMovement.CanBeKnockBacked())
            {
                Vector3 axeVelocity = GetComponent<Rigidbody>().velocity.normalized;
                Vector3 direction = new Vector3(axeVelocity.x, 0, axeVelocity.z);

                if (explosiveRadiusT2)
                {
                    enemyMovement.KnockBack(direction, 1.5f);
                    ApplyExplosiveForceToNearbyOrcs(enemyMovement.transform.position, 1.2f);
                }
                else if (explosiveRadiusT1)
                {
                    enemyMovement.KnockBack(direction, 1.2f);
                    ApplyExplosiveForceToNearbyOrcs(enemyMovement.transform.position, .6f);
                }
                else
                {
                    enemyMovement.KnockBack(direction, 1f);
                }
            }

            if (lifesteal && playerStats.Health < (playerStats.maxHealth - axeLifestealAmount))
            {
                playerStats.AddHealth(axeLifestealAmount);
            }
        }
    }

    private void ApplyExplosiveForceToNearbyOrcs(Vector3 explosionPosition, float force)
    {
        // Find all nearby orcs within a certain radius
        Collider[] nearbyOrcs = Physics.OverlapSphere(explosionPosition, 5f, LayerMask.GetMask("Enemy"));
        Debug.Log("Inside AxeDetection");
        foreach (Collider orcCollider in nearbyOrcs)
        {
            /*
            // Check if the collider belongs to an orc
            if (orcCollider.TryGetComponent(out Orc_Health orc))
            {
                // Calculate the direction from the explosion to the orc
                Vector3 direction = orc.transform.position - explosionPosition;

                // Normalize the direction vector and apply the force
                direction.Normalize();
                orc.KnockBack(direction, force);
            }
            */
            
            if (orcCollider.TryGetComponent(out EnemyMovement enemyMovement) && enemyMovement.CanBeKnockBacked())
            {
                var direction = enemyMovement.transform.position - explosionPosition;
                direction.Normalize();
                enemyMovement.KnockBack(direction, force);
            }
        }
    }

    public int GetAxeDamage()
    {
        return defaultDamage + additionalAxeDamage;
    }

    public bool HasAxeHitSomething()
    {
        return axeHitSomething;
    }
}
