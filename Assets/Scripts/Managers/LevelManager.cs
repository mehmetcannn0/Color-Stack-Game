using DG.Tweening;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Transform gateParent;
    [SerializeField] private Transform blockParent;
    [SerializeField] private Transform stackParent;
    [SerializeField] private Transform coinsParent;

    PrefabManager prefabManager;

    private float[] possibleX = { -3.45f, -1f, 1f, 3.45f };

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

        PlaceCoins();

        MaterialType gateMaterialType = MaterialType.Green;
        MaterialType[] columnMaterialTypes = new MaterialType[Utils.COLUMN_COUNT];

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

                for (int col = 0; col < Utils.COLUMN_COUNT; col++)
                {
                    columnMaterialTypes[col] = (MaterialType)Random.Range(0, 4);
                }
                if (!columnMaterialTypes.Contains(gateMaterialType))
                {
                    int selectedColumnIndex = Random.Range(0, Utils.COLUMN_COUNT);
                    columnMaterialTypes[selectedColumnIndex] = gateMaterialType;
                }

                PlaceGate(gateMaterialType, i);
            }
            else
            {
                for (int x = 0; x < Utils.COLUMN_COUNT; x++)
                {
                    MaterialType blockMaterialType = columnMaterialTypes[x];

                    Vector3 blockPosition = new Vector3((x - 1.5f) * Utils.BLOCK_HORIZONTAL_SPACE_SIZE, 0f, i);

                    PlaceBlock(blockPosition, blockMaterialType, gateMaterialType);
                }
            }
        }
    }

    private void PlaceBlock(Vector3 blockPosition, MaterialType blockMaterialType, MaterialType gateMaterialType)
    {
        GameObject blockObject = prefabManager.InstantiateBlock(blockPosition, Quaternion.identity, blockParent);

        Block block = blockObject.GetComponent<Block>();
        block.Init(blockMaterialType, gateMaterialType);
    }

    private void PlaceGate(MaterialType materialType, float gatePositionIndex)
    {
        GameObject gateObject = prefabManager.InstantiateGate(new Vector3(0f, 0f, gatePositionIndex), Quaternion.identity, gateParent);
        gateObject.GetComponent<Gate>().Init(materialType);
    }

    public void PlaceCoins()
    {
        for (int g = 1; g < 50; g++)
        {
            float x = possibleX[Random.Range(0, possibleX.Length)];
            float zStart = g * 4f;

            for (int z = 0; z < 5; z++)
            {
                for (int y = 0; y < 4; y++)
                {
                    Vector3 position = new Vector3(x, y, zStart + z);
                    prefabManager.InstantiateCoin(position, Quaternion.identity, coinsParent);
                }
            }
        }
    }

    public void ClearLevel()
    {
        foreach (Transform child in gateParent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in blockParent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in stackParent)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in coinsParent)
        {
            DOTween.Kill(child);
            Destroy(child.gameObject);
        }
    }
}
