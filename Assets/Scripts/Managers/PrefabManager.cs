using UnityEngine;

public class PrefabManager : MonoSingleton<PrefabManager>
{
    [SerializeField] private GameObject gatePrefab;
    [SerializeField] private GameObject blockPrefab;  

    public GameObject InstantiateGate(Vector3 objectPosition, Quaternion rotation, Transform parent) 
    {
        return Instantiate(gatePrefab, objectPosition, rotation, parent);
    }

    public GameObject InstantiateBlock(Vector3 objectPosition, Quaternion rotation, Transform parent)
    {
        return Instantiate(blockPrefab, objectPosition, rotation, parent);
    }

}