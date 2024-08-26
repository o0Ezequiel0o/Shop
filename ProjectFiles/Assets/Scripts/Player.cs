using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private Camera camera;

	private Ray ray;
	private RaycastHit hit;

	void Update()
    {
		if(Input.GetMouseButtonDown(0))
		{
			ray = camera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100))
			{
				ProductBox clickedBox;
				if (hit.transform.gameObject.TryGetComponent<ProductBox>(out clickedBox))
				{
					clickedBox.AddProduct();
				}
			}
		}

		if (Input.GetMouseButtonDown(1))
		{
			ray = camera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out hit, 100))
			{
				ProductBox clickedBox;
				if (hit.transform.gameObject.TryGetComponent<ProductBox>(out clickedBox))
				{
					clickedBox.TakeProduct();
				}
			}
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			ClientManager.Instance.CreateBuyer();
		}
	}
}
