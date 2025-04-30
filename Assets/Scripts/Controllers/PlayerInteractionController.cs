
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerObjects = new List<GameObject>();
    [SerializeField] private MaterialType stackBaseMaterialType;
    [SerializeField] private Transform stackParent;

    private MeshRenderer meshRenderer;

    private List<GameObject> stackList = new List<GameObject>();
    private Vector3 stackOffset = new Vector3(0, 0.1f, 0);
    private bool isCharged = false;

    MaterialManager materialManager;
    GameManager gameManager;

    public static PlayerInteractionController Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
                stackable.OnStack();
                gameManager.DecreaseChargeCount();
                AddToStack(collision.gameObject);

                ActionController.UpdateChargeLevelUI?.Invoke();

                if (gameManager.chargeCount <= 0)
                {
                    transform.localScale = new Vector3(1.5f, 0.1f, 1);
                    isCharged = false;
                    ActionController.OnUncharged?.Invoke();
                }
                return;
            }

            if (stackBaseMaterialType == stackable.GetMaterialType())
            {
                stackable.OnStack();
                gameManager.IncreaseChargeCount();
                AddToStack(collision.gameObject);
                ActionController.UpdateChargeLevelUI?.Invoke();


                if (gameManager.chargeCount == 30)
                {
                    transform.localScale = new Vector3(8f, 0.1f, 1);

                    isCharged = true;
                    ActionController.OnCharged?.Invoke();
                }
            }
            else
            {
                gameManager.DecreaseChargeCount(true);//chargeCount = 0f;
                ActionController.UpdateChargeLevelUI?.Invoke();
                RemoveFromStack();
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IColorGate colorGate))
        {
            MaterialType gateMaterialType = colorGate.GateMaterialType();
            Material gateMaterial = meshRenderer.material;
            switch (gateMaterialType)
            {
                case MaterialType.Red:
                    gateMaterial = materialManager.materials[0].material;
                    stackBaseMaterialType = materialManager.materials[0].materialType;
                    break;
                case MaterialType.Green:
                    gateMaterial = materialManager.materials[1].material;
                    stackBaseMaterialType = materialManager.materials[1].materialType;
                    break;
                case MaterialType.Blue:
                    gateMaterial = materialManager.materials[2].material;
                    stackBaseMaterialType = materialManager.materials[2].materialType;
                    break;
                case MaterialType.Yellow:
                    gateMaterial = materialManager.materials[3].material;
                    stackBaseMaterialType = materialManager.materials[3].materialType;
                    break;
                default:
                    gateMaterial = materialManager.materials[0].material;
                    stackBaseMaterialType = materialManager.materials[0].materialType;
                    break;
            }

            meshRenderer.material = gateMaterial;

            ActionController.OnGateInteracted?.Invoke(gateMaterial);

            foreach (GameObject obj in playerObjects)
            {
                obj.GetComponent<Renderer>().material = gateMaterial;
            }
        }
    }

    void AddToStack(GameObject block)
    {
        block.transform.SetParent(stackParent);
        Vector3 newPos = Vector3.up * stackList.Count * stackOffset.y;
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
        transform.localScale = new Vector3(1.5f, 0.1f, 1);
    }
}
