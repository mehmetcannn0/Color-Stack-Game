using System;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    [SerializeField] List<PrefabData> prefabs = new List<PrefabData>();
    public static PrefabManager Instance { get; private set; }
  
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameObject InstantiateObjet(PrefabType prefabType, Vector3 objectPosition, MaterialType materialType, MaterialType chargedMaterialType = MaterialType.Green, Transform parent = null)
    {
        PrefabData data = prefabs.Find(p => p.Type == prefabType);
        GameObject newObj = Instantiate(data.Prefab, objectPosition, Quaternion.identity, parent);
        if (prefabType == PrefabType.Gate)
        {
            newObj.GetComponent<Gate>().MaterialType = materialType;

        }
        else
        {
            Block block = newObj.GetComponent<Block>() ;
            block.MaterialType = materialType;
            block.ChargedMaterialType = chargedMaterialType;

        }
        return newObj;
    }
}

public enum PrefabType
{
    Gate,
    Block,

}

[Serializable]
public class PrefabData
{
    public PrefabType Type;
    public GameObject Prefab;
}