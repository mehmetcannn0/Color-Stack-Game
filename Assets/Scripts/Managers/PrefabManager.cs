using UnityEngine;

public class PrefabManager : MonoSingleton<PrefabManager>
{
    [SerializeField] private GameObject gatePrefab;
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GameObject coinUIPrefab;
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private Transform canvas;

    public GameObject InstantiateGate(Vector3 objectPosition, Quaternion rotation, Transform parent)
    {
        return Instantiate(gatePrefab, objectPosition, rotation, parent);
    }

    public GameObject InstantiateBlock(Vector3 objectPosition, Quaternion rotation, Transform parent)
    {
        return Instantiate(blockPrefab, objectPosition, rotation, parent);
    }

    public GameObject InstantiateCoin(Vector3 objectPosition, Quaternion rotation, Transform parent)
    {
        return Instantiate(coinPrefab, objectPosition, Quaternion.identity, parent);
    }

    public GameObject InstantiateCoinUI()
    {
        return Instantiate(coinUIPrefab, Vector3.zero, Quaternion.identity, canvas);
    }
}