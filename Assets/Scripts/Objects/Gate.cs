using UnityEngine;

public class Gate : MonoBehaviour, IColorGate
{
    private MeshRenderer gateRenderer;
    private MaterialType MaterialType;

    private void Awake()
    {
        gateRenderer = GetComponent<MeshRenderer>();
    }

    public void Init(MaterialType materialType)
    {
        MaterialType = materialType;
        MaterialTypeData requestedMaterialData = MaterialManager.Instance.GetMaterialTypeData(MaterialType);
        gateRenderer.material = requestedMaterialData.material;
    }

    public MaterialType GateMaterialType()
    {
        return MaterialType;
    }
}
