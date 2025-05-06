using System;
using UnityEngine;

public class Block : MonoBehaviour, IStackable, IColorChangeable
{
    private bool isStacked;

    [SerializeField] MeshRenderer blockRenderer;
    [SerializeField] Rigidbody blockRigidbody;
    [SerializeField] BoxCollider boxCollider;

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
        ActionController.OnCharged += OnCharged;
        ActionController.OnUncharged += OnUncharged;
    }

    private void OnDisable()
    {
        ActionController.OnCharged -= OnCharged;
        ActionController.OnUncharged -= OnUncharged;

        ActionController.OnGateInteracted -= OnGateInteracted;
        ActionController.AddForce -= AddForce;
    }

    public void Init(MaterialType blockMaterialType, MaterialType gateMaterialType)
    {
        MaterialType = blockMaterialType;
        ChargedMaterialType = gateMaterialType;
    }

    private void OnGateInteracted(MaterialTypeData materialTypeData)
    {
        if (!isStacked)
            return;

        blockRenderer.material = materialTypeData.material;
        MaterialType = materialTypeData.materialType;
    }

    public void OnCharged()
    {
        if (isStacked)
            return;

        MaterialTypeData requestedMaterialType = materialManager.GetMaterialTypeData(ChargedMaterialType);
        blockRenderer.material = requestedMaterialType.material;
    }

    public void OnUncharged()
    {
        if (!isStacked)
            blockRenderer.material = blockMaterial;
    }

    private void AddForce()
    {
        if (!isStacked)
            return;

        MakeEnableBoxCollider();
        blockRigidbody.isKinematic = false;
        blockRigidbody.velocity = Vector3.zero;
        blockRigidbody.AddForce(Vector3.forward * gameManager.KickForce + Vector3.up, ForceMode.Impulse);
    }

    public void OnStack()
    {
        ActionController.OnGateInteracted += OnGateInteracted;
        ActionController.AddForce += AddForce;

        MakeDisableBoxCollider();
        isStacked = true;
        ActionController.UpdateScore?.Invoke(1f);
    }

    public void MakeDisableBoxCollider()
    {
        boxCollider.enabled = false;
    }

    public void MakeEnableBoxCollider()
    {
        boxCollider.enabled = true;
    }

    public MaterialType GetMaterialType()
    {
        return MaterialType;
    }

    public void DestroyBlock()
    {


        Destroy(gameObject);
    }

    public bool IsStacked()
    {
        return isStacked;
    }
}

public static partial class ActionController
{
    public static Action<MaterialTypeData> OnGateInteracted { get; set; }
    public static Action OnCharged { get; set; }
    public static Action OnUncharged { get; set; }

    public static Action AddForce;
}

