using System;
using UnityEngine;

public class Block : MonoBehaviour, IStackable, IColorChangeable
{
    [field: SerializeField] public bool IsStacked { get; private set; }

    [SerializeField] MeshRenderer blockRenderer;
    [SerializeField] Rigidbody blockRigidbody;

    private Material blockMaterial;

    public MaterialType MaterialType;
    public MaterialType ChargedMaterialType;

    MaterialManager materialManager;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        materialManager = MaterialManager.Instance;
    }

    private void Start()
    {
        switch (MaterialType)
        {
            case MaterialType.Red:
                blockRenderer.material = materialManager.materials[0].material;
                blockMaterial = blockRenderer.material;
                break;
            case MaterialType.Green:
                blockRenderer.material = materialManager.materials[1].material;
                blockMaterial = blockRenderer.material;
                break;
            case MaterialType.Blue:
                blockRenderer.material = materialManager.materials[2].material;
                blockMaterial = blockRenderer.material;
                break;
            case MaterialType.Yellow:
                blockRenderer.material = materialManager.materials[3].material;
                blockMaterial = blockRenderer.material;
                break;
        }
    }

    private void OnEnable()
    {
        ActionController.OnGateInteracted += OnGateInteracted;
        ActionController.OnCharged += OnCharged;
        ActionController.OnUncharged += OnUncharged;
        ActionController.AddForce += AddForce;
    }

    private void OnDisable()
    {
        ActionController.OnGateInteracted -= OnGateInteracted;
        ActionController.OnCharged -= OnCharged;
        ActionController.OnUncharged -= OnUncharged;
        ActionController.AddForce -= AddForce;
    }

    private void OnGateInteracted(Material material)
    {
        if (!IsStacked)
            return;

        blockRenderer.material = material;
    }
    public void OnCharged()
    {
        if (IsStacked)
            return;

        switch (ChargedMaterialType)
        {
            case MaterialType.Red:
                blockRenderer.material = materialManager.materials[0].material;
                break;
            case MaterialType.Green:
                blockRenderer.material = materialManager.materials[1].material;
                break;
            case MaterialType.Blue:
                blockRenderer.material = materialManager.materials[2].material;
                break;
            case MaterialType.Yellow:
                blockRenderer.material = materialManager.materials[3].material;
                break;
        }
    }

    public void OnUncharged()
    {
        if (!IsStacked)
            blockRenderer.material = blockMaterial;
    }

    private void AddForce()
    {
        if (!IsStacked)
            return;

        blockRigidbody.isKinematic = false;
        blockRigidbody.velocity = Vector3.zero;
        blockRigidbody.AddForce(Vector3.forward * gameManager.KickForce + Vector3.up, ForceMode.Impulse);
    }

    public void OnStack()
    {
        IsStacked = true;
    }

    public MaterialType GetMaterialType()
    {
        return MaterialType;
    }
}

public static partial class ActionController
{
    public static Action<Material> OnGateInteracted { get; set; }
    public static Action OnCharged { get; set; }
    public static Action OnUncharged { get; set; }

    public static Action AddForce;
}

