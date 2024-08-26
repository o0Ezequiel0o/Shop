using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProductBox : MonoBehaviour
{
    [SerializeField] private GameObject productPrefab;
	[SerializeField] public Transform buyPosition;
    [Space]
	[SerializeField] private int startProducts = 3;
    [SerializeField] private int maxProducts = 9;
    [Space]
    [SerializeField] private Transform startPosition;
    [SerializeField] private float productOffset = 1f;

    private Stack<GameObject> products = new Stack<GameObject>();
    Vector3 pos = new Vector3(200, 200, 0);

    public bool HasProducts => products.Count > 0;
    public bool occupied = false;

    private void Start()
    {
        for (int i = 0; i < startProducts; i++)
        {
            AddProduct();
        }
    }
    public void AddProduct()
    {
        if (products.Count < maxProducts)
        {
            Vector3 currentOffset = new Vector3(0, 0, products.Count * productOffset);
            products.Push(Instantiate(productPrefab, startPosition.position + currentOffset, Quaternion.identity));
        }
    }
    public void TakeProduct()
    {
        if (products.Count > 0)
        {
			Destroy(products.Peek());
			products.Pop();
		}
    }
}