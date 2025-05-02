
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoSingleton<PlayerInteractionController>
{
    [SerializeField] private List<GameObject> playerObjects;
    [SerializeField] private MaterialType stackBaseMaterialType;
    [SerializeField] private Transform stackParent;

    private MeshRenderer meshRenderer;

    private List<GameObject> stackList = new List<GameObject>();
    private bool isCharged = false;

    MaterialManager materialManager;
    GameManager gameManager;


    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        materialManager = MaterialManager.Instance;
        gameManager = GameManager.Instance;

    }

    private void OnEnable()
    {
        ActionController.OnLevelStart += ClearStackList;
    }

    private void OnDisable()
    {
        ActionController.OnLevelStart -= ClearStackList;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!PlayerInputManager.Instance.IsActive)
            return;

        if (collision.gameObject.TryGetComponent(out IFinishLevel finishLevel))
        {
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
        RemoveFromStack();
        Destroy(collision.gameObject);
    }

    private void SetActiveCharge()
    {
        transform.localScale = Utils.CHARGED_STACK_BASE_SCALE;

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
            transform.localScale = Utils.STACK_BASE_SCALE;
            isCharged = false;
            ActionController.OnUncharged?.Invoke();
        }
        return;
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
        if (stackList.Count > 0)
        {
            GameObject topBlock = stackList[stackList.Count - 1];
            stackList.RemoveAt(stackList.Count - 1);
            Destroy(topBlock);
        }
    }

    private void ClearStackList()
    {
        stackList.Clear();
        isCharged = false;
        transform.localScale = Utils.STACK_BASE_SCALE;
    }
}
