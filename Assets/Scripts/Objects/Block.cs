using System;
using UnityEngine;

public class Block : MonoBehaviour, IStackable, IColorChangeable
{
    [field: SerializeField] public bool IsStacked { get; private set; }

    [SerializeField] MeshRenderer blockRenderer;
    [SerializeField] Rigidbody blockRigidbody;

    private Material blockMaterial;

    private MaterialType MaterialType;
    private MaterialType ChargedMaterialType;

    MaterialManager materialManager;
    GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.Instance;
        materialManager = MaterialManager.Instance;
    }

    private void Start()
    {
        MaterialTypeData requestedMaterialData = materialManager.GetMaterialTypeData(MaterialType);
        blockRenderer.material = requestedMaterialData.material;
        blockMaterial = requestedMaterialData.material;
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

    public void Init(MaterialType blockMaterialType, MaterialType gateMaterialType)
    {
        MaterialType = blockMaterialType;
        ChargedMaterialType = gateMaterialType;
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

        MaterialTypeData requestedMaterialType = materialManager.GetMaterialTypeData(ChargedMaterialType);
        blockRenderer.material = requestedMaterialType.material;
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
        ActionController.UpdateScore?.Invoke(1f);
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

