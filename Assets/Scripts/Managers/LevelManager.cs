using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform gateParents;
    [SerializeField] private Transform blockParents;
    [SerializeField] private Transform stackParent;

    PrefabManager prefabManager; 

    void Start()
    {
        prefabManager = PrefabManager.Instance; 

    }

    private void OnEnable()
    {
        ActionController.OnLevelStart += CreateLevel;
        ActionController.OnLevelRestart += ClearLevel;
    }

    private void OnDisable()
    {
        ActionController.OnLevelStart -= CreateLevel;
        ActionController.OnLevelRestart -= ClearLevel;
    }

    public void CreateLevel()
    {
        ClearLevel();

        MaterialType gateMaterialType = MaterialType.Green;

        int columnCount = 4;
        float blockSpacing = 2.3f;
        MaterialType[] columnMaterialTypes = new MaterialType[columnCount];

        for (float i = 0; i < 200; i += 1.5f)
        {
            if (i % 10 == 0)
            {
                int randomIndex;
                do
                {
                    randomIndex = Random.Range(0, 4);
                }
                while (gateMaterialType == (MaterialType)randomIndex);

                gateMaterialType = (MaterialType)randomIndex;

                for (int col = 0; col < columnCount; col++)
                {
                    columnMaterialTypes[col] = (MaterialType)Random.Range(0, 4);
                }

                GameObject gate = prefabManager.InstantiateObjet(prefabType: PrefabType.Gate, materialType: gateMaterialType, objectPosition: new Vector3(0f, 0f, i), parent: gateParents);
            }
            else
            {
                for (int x = 0; x < columnCount; x++)
                {
                    MaterialType blockMaterialType = columnMaterialTypes[x];

                    Vector3 blockPosition = new Vector3((x - 1.5f) * blockSpacing, 0f, i);

                    GameObject block = prefabManager.InstantiateObjet(prefabType: PrefabType.Block, materialType: blockMaterialType, chargedMaterialType: gateMaterialType, objectPosition: blockPosition, parent: blockParents);
                }
            }
        }
    }

    public void ClearLevel()
    {
        foreach (Transform child in gateParents)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in blockParents)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in stackParent)
        {
            Destroy(child.gameObject);
        } 
    }
}
