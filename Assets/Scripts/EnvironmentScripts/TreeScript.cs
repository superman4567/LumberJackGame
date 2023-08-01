using System;
using UnityEngine;
using UnityEngine.UIElements;
using static TreeScript;

public class TreeScript : MonoBehaviour
{
    [Header("Tree Physics")]
    [SerializeField] private Rigidbody TreeTop;
    [SerializeField] private float TreeFallingCooldown = 2f;

    private TreeState currentState;
    public Action onInteraction;
    private GameObject targetObject;
    private bool isSubscribed = false;
    private bool eHasBeenPressed = false;
    private bool treeFalling = false;
    public int newPercentage;
    private float treeChopPercentage = 0;

    [Header("Tree Audio")]
    [SerializeField] private AudioSource TreeBreakingSound;

    public delegate void AddWoodDelegate();
    public event AddWoodDelegate addWoodDelegate;

    public delegate void OnInteractionComplete(int newPercent);
    public static event OnInteractionComplete InteractionComplete;

    private int interactionIncrement;
    private float interactionPercentage;

    public enum TreeState
    {
        StandingTree,
        FallingTree,
        FallenTree,
        DestroyedTree
    }

    private void Awake()
    {
        TreeTop = GetComponentInChildren<Rigidbody>();
        TreeBreakingSound = GetComponent<AudioSource>();
        TreeTop.isKinematic = true;
    }

    private void Start()
    {
        currentState = TreeState.StandingTree;
        targetObject = gameObject;
    }

    private void OnEnable()
    {
        Player.InteractKeyPressed += OnInteractButtonPressed;
        isSubscribed = true;
    }

    private void OnDisable()
    {
        if (isSubscribed)
        {
            Player.InteractKeyPressed -= OnInteractButtonPressed;
            isSubscribed = false;
        }
    }

    private void Update()
    {
        TreeFallingTimer();
    }

    public void TreeChopPercent(float newValue)
    {
        treeChopPercentage = newValue;
    }

    public float GetTreeValue()
    {
        return treeChopPercentage;
    }

    private void TreeFallingTimer()
    {
        if (treeFalling == true)
        {
            TreeFallingCooldown -= Time.deltaTime;
        }
    }

    public void OnInteractButtonPressed(GameObject targetObject)
    {
        if (targetObject == gameObject)
        {
            switch (currentState)
            {
                case TreeState.StandingTree:
                    GameManager.Instance.AddWood();
                    currentState = TreeState.FallingTree;
                    TreeFalling();
                    break;

                case TreeState.FallingTree:
                    currentState = TreeState.FallenTree;
                    break;

                case TreeState.FallenTree:
                    if (TreeFallingCooldown <= 0f)
                    {
                        currentState = TreeState.DestroyedTree;
                        GameManager.Instance.AddWood();
                        DestroyTree();
                    }
                    break;

                default:
                    Debug.LogWarning("Unexpected tree state!");
                    break;
            }
        }
    }

    public void TreeFalling()
    {
        TreeTop.isKinematic = false;
        treeFalling = true;
        TreeBreakingSound.Play();
    }

    public void DestroyTree()
    {
        gameObject.SetActive(false);
    }
}
