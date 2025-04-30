using System;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    public List<MaterialTypeData> materials = new List<MaterialTypeData>();

    public static MaterialManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

[Serializable]
public class MaterialTypeData
{
    public MaterialType materialType;
    public Material material;
}