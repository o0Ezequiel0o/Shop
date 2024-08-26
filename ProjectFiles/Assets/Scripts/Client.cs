using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [Space]
    [SerializeField] private float buyTime = 1f;
    [Space]
    [SerializeField] private float speed = 5f;

    private ProductBox[] boxes;
    private ProductBox currentBox;

    private float buyTimer = 0f;

    private enum State
    {
        Waiting,
        Moving,
        Buying
    }

    private State currentState;

    private Vector3 movePosition;

    public Client buyerBehind;

    void Start()
    {
        boxes = FindObjectsOfType<ProductBox>();
	}

    void Update()
    {
        switch(currentState)
        {
            case State.Waiting:
                break;

            case State.Moving:
                Move();
                break;

            case State.Buying:
                Buying();
                break;
        }
	}

    private void Move()
    {
	    _transform.position = Vector3.MoveTowards(_transform.position, movePosition, speed * Time.deltaTime);

	    if (_transform.position == movePosition)
	    {
            currentState = State.Buying;
		}
	}

    private void Buying()
    {
		buyTimer += Time.deltaTime;
		if (buyTimer >= buyTime)
		{
			FinishBuying();
		}
	}

    private void FinishBuying()
    {
		currentBox.TakeProduct();
		currentBox.occupied = false;
		Destroy(gameObject);
	}

    public bool TryBuy()
    {
        for (int i = 0; i < boxes.Length; i++)
        {
            if (boxes[i].HasProducts && !boxes[i].occupied)
            {
                currentBox = boxes[i];
                MoveToBox();

                return true;
            }
        }
        return false;
    }

    private void MoveToBox()
    {
		movePosition = new Vector3(currentBox.buyPosition.position.x, _transform.position.y, currentBox.buyPosition.position.z);
        currentState = State.Moving;

        if (buyerBehind != null)
        {
            buyerBehind.MoveToNextQueuePosition();
        }

		currentBox.occupied = true;
	}

    public void MoveToNextQueuePosition()
    {
        _transform.position -= new Vector3(0, 0, ClientManager.Instance.buyersOffset);

        if (buyerBehind != null)
        {
            buyerBehind.MoveToNextQueuePosition();
        }
    }
}