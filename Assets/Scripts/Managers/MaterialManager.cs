using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MaterialManager : MonoSingleton<MaterialManager>
{
    [SerializeField] private List<MaterialTypeData> materials; 

    public MaterialTypeData GetMaterialTypeData(MaterialType requestedType) => materials.FirstOrDefault(x => x.materialType == requestedType);
}

[Serializable]
public class MaterialTypeData
{
    public MaterialType materialType;
    public Material material;
}