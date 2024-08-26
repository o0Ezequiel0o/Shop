using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> buyerPrefabs;
    [SerializeField] private Transform startPosition;
    [Space]
    [SerializeField] private int startBuyers = 10;
    [Space]
    [SerializeField] private float timeBetweenBuyer = 1f;

    private Queue<GameObject> buyers = new Queue<GameObject>();

    [SerializeField] public float buyersOffset = 1f;

    static public ClientManager Instance { get; private set; }

    private Client lastBuyer;

    private GameObject randomBuyerCache;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        for (int i = 0; i < startBuyers; i++)
        {
            CreateBuyer();
        }

        StartCoroutine(WaitBuyer());
    }

    public void CreateBuyer()
    {
        randomBuyerCache = buyerPrefabs[Random.Range(0, buyerPrefabs.Count)];
		GameObject spawnedBuyer = Instantiate(randomBuyerCache, startPosition.position, Quaternion.identity);

        if (lastBuyer != null)
        {
            lastBuyer.buyerBehind = spawnedBuyer.GetComponent<Client>();
        }
        lastBuyer = spawnedBuyer.GetComponent<Client>();

        AddBuyer(spawnedBuyer);
	}

    public void AddBuyer(GameObject buyer)
    {
		Vector3 currentOffset = new Vector3(0, 0, buyers.Count * buyersOffset);

        buyer.transform.position = startPosition.position + currentOffset;
		buyers.Enqueue(buyer);
	}

    private IEnumerator WaitBuyer()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenBuyer);

            if (buyers.Count > 0)
            {
                Client buyer = buyers.Peek().GetComponent<Client>();

                if (buyer.TryBuy())
                {
                    buyers.Dequeue();
				}
			}
		}
    }
}