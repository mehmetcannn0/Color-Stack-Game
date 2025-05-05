
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoSingleton<PlayerInteractionController>
{
    [SerializeField] private List<GameObject> playerObjects;
    [SerializeField] private MaterialType stackBaseMaterialType;
    [SerializeField] private Transform stackParent;


    [SerializeField] private Transform coinCollectObjectTransform;

    private MeshRenderer meshRenderer;

    private List<GameObject> stackList = new List<GameObject>();
    private bool isCharged = false;

    MaterialManager materialManager;
    GameManager gameManager;

    private Vector3 firstLocalPos;
    private Vector3 firstLocalScale;


    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        materialManager = MaterialManager.Instance;
        gameManager = GameManager.Instance;

        firstLocalPos = transform.localPosition;
        firstLocalScale = transform.localScale;
    }

    private void OnEnable()
    {
        ActionController.OnLevelStart += ClearStackList;
        ActionController.GetTopBlockObject += GetTopBlockObject;
    }

    private void OnDisable()
    {
        ActionController.OnLevelStart -= ClearStackList;
        ActionController.GetTopBlockObject -= GetTopBlockObject;
    }

    private GameObject GetTopBlockObject()
    {
        if (stackList.Count > 0)
        {
            return stackList[^1];
        }

        GameObject newObject = new GameObject("TopBlock");
        newObject.transform.position = stackParent.position;

        return newObject;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!PlayerInputManager.Instance.IsActive)
            return;


        if (collision.gameObject.TryGetComponent(out IFinishLevel finishLevel))
        {
            GameObject lastBlock = null;

            try
            {
                lastBlock = stackList[^1];
            }
            catch
            {
                Debug.Log("Stack is empty");
            }

            finishLevel.FinishLevel();
            return;
        }

        if (collision.gameObject.TryGetComponent(out IStackable stackable))
        {
            if (isCharged)
            {
                Oncharged(collision, stackable);
                return;
            }

            if (stackBaseMaterialType == stackable.GetMaterialType())
            {
                StackBlock(collision, stackable);

                if (gameManager.ChargeCount == Utils.CHARGE_LEVEL_LIMIT)
                {
                    SetActiveCharge();
                }
            }
            else
            {
                DestroyBlock(collision);
            }
        }
    }

    private void DestroyBlock(Collision collision)
    {
        gameManager.DecreaseChargeCount(true);
        ActionController.UpdateChargeLevelUI?.Invoke();
        ActionController.UpdateScore?.Invoke(-1f);

        if (stackList.Count > 0)
        {
            RemoveFromStack();
        }
        Destroy(collision.gameObject);
    }

    private void SetActiveCharge()
    {
        transform.localScale = Utils.CHARGED_STACK_BASE_SCALE;
        coinCollectObjectTransform.localScale = new Vector3(8, 1, 1);


        isCharged = true;
        ActionController.OnCharged?.Invoke();
    }

    private void StackBlock(Collision collision, IStackable stackable)
    {
        stackable.OnStack();
        gameManager.IncreaseChargeCount();
        AddToStack(collision.gameObject);
        ActionController.UpdateChargeLevelUI?.Invoke();
    }

    private void Oncharged(Collision collision, IStackable stackable)
    {
        stackable.OnStack();
        gameManager.DecreaseChargeCount();
        AddToStack(collision.gameObject);

        ActionController.UpdateChargeLevelUI?.Invoke();

        if (gameManager.ChargeCount <= 0)
        {
            transform.localScale = firstLocalScale;
            coinCollectObjectTransform.localScale = Vector3.one;
            //transform.localPosition = firstLocalPos;
            isCharged = false;
            ActionController.OnUncharged?.Invoke();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IColorGate colorGate))
        {
            MaterialType gateMaterialType = colorGate.GateMaterialType();

            MaterialTypeData requestedMaterialData = materialManager.GetMaterialTypeData(gateMaterialType);

            stackBaseMaterialType = requestedMaterialData.materialType;

            meshRenderer.material = requestedMaterialData.material;

            ActionController.OnGateInteracted?.Invoke(requestedMaterialData.material);

            foreach (GameObject obj in playerObjects)
            {
                obj.GetComponent<Renderer>().material = requestedMaterialData.material;
            }
        }
    }

    void AddToStack(GameObject block)
    {
        block.transform.SetParent(stackParent);
        Vector3 newPos = Vector3.up * stackList.Count * Utils.BLOCK_VERTICAL_SPACE_SIZE.y;
        block.transform.localPosition = newPos;
        stackList.Add(block);
    }

    void RemoveFromStack()
    {
        GameObject topBlock = stackList[^1];
        stackList.Remove(topBlock);
        Destroy(topBlock);
    }

    private void ClearStackList()
    {
        stackList.Clear();
        isCharged = false;
        transform.localScale = firstLocalScale;
        transform.localPosition = firstLocalPos;
        coinCollectObjectTransform.localScale = Vector3.one;
    }
}
