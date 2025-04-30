
using UnityEngine;

public class Gate : MonoBehaviour, IColorGate
{
    private MeshRenderer gateRenderer;
    public MaterialType MaterialType;

    private void Awake()
    {
        gateRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        switch (MaterialType)
        {
            case MaterialType.Red:
                gateRenderer.material = MaterialManager.Instance.materials[0].material;
                break;
            case MaterialType.Green:
                gateRenderer.material = MaterialManager.Instance.materials[1].material;
                break;
            case MaterialType.Blue:
                gateRenderer.material = MaterialManager.Instance.materials[2].material;
                break;
            case MaterialType.Yellow:
                gateRenderer.material = MaterialManager.Instance.materials[3].material;
                break;
        }

    }

    public MaterialType GateMaterialType()
    {
        return MaterialType;
    }
}
