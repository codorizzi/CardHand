using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class Card : MonoBehaviour {
	
	Vector2 targetPosition;

	float moveSpeed = 10f;

	public Vector2 position {
		get { return targetPosition; }
		set { targetPosition = value; }
	}

	public int sortOrder {
		set {
			GetComponent<Canvas>().sortingOrder = value;
		}
	}

	public float width {
		get { return 200f; }
	}

	void Awake () {
		GetComponent<Canvas>().overrideSorting = true;
	}

	void Update () {

		if (transform.position.x != targetPosition.x || transform.position.y != targetPosition.y) {
			float step = moveSpeed * Time.deltaTime * 100;
			transform.position = Vector2.MoveTowards(transform.position, targetPosition, step);
		}
		
	}

	public void setPosition(Vector2 position) {
		transform.position = position;
	}

}
