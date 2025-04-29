
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    [SerializeField] List<GameObject> playerObjects = new List<GameObject>();
    [SerializeField] private Material stackBaseMaterial;
    [SerializeField] private Transform stackParent;

    private List<GameObject> stackList = new List<GameObject>();
    private MeshRenderer meshRenderer;

    private Vector3 stackOffset = new Vector3(0, 0.1f, 0);
    private int chargeCount = 0;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IStackable stackable))
        {
            if (stackBaseMaterial.name == stackable.GetMaterial().name)
            {
                stackable.OnStack();
                chargeCount++;
                AddToStack(collision.gameObject);

            }
            else
            {
                chargeCount = 0;
                RemoveFromStack();
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.TryGetComponent(out IColorGate colorGate))
        {
            Material gateMaterial = colorGate.GateMaterial();
            meshRenderer.material = gateMaterial;
            stackBaseMaterial = gateMaterial;
            //foreach (GameObject block in stackList)
            //{
            //    block.GetComponent<Renderer>().material = gateMaterial;
            //}

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
}
public static partial class ActionController
{


}
