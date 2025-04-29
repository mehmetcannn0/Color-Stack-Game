
using UnityEngine;

public class Gate : MonoBehaviour , IColorGate
{
    [SerializeField] private Material gateMaterial;
    private void Awake()
    {
        gateMaterial = GetComponent<Renderer>().material;
    }
    public Material GateMaterial()
    {
        return gateMaterial;
    }
 
 
}
