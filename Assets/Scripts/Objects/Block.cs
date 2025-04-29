using System;
using UnityEngine;

public class Block : MonoBehaviour, IStackable, IColorChangeable
{
    [field: SerializeField] public bool IsStacked { get; private set; }

    private MeshRenderer blockRenderer;

    private Material blockMaterial;

    //public static Action OnColorChange;

    private void Awake()
    {
        blockRenderer = GetComponent<MeshRenderer>();
        blockMaterial = blockRenderer.material;
    }

    private void OnEnable()
    {
        ActionController.OnGateInteracted += OnGateInteracted;
    }

    private void OnDisable()
    {
        ActionController.OnGateInteracted -= OnGateInteracted;
    }

    private void OnGateInteracted(Material material)
    {
        if (!IsStacked) 
            return;

        blockRenderer.material= material;
    }

    public void ChangeColor(Material material)
    {
        GetComponent<Renderer>().material = material;
    }

    public void OnStack()
    {
        Debug.Log("stack");
        IsStacked = true;
    }

    public Material GetMaterial()
    {
        return blockMaterial;
    }
}

public static partial class ActionController
{
    public static Action<Material> OnGateInteracted { get; set; }
}


