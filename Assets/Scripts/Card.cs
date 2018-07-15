﻿using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {

	Vector2 targetPosition;

	float moveSpeed = 15f;
	float zoomFactor = 5f;
	float ySelectedOffset = 55f;
	int indexPosition;

	public Vector2 position {
		get { return targetPosition; }
		set {
			targetPosition = value;
			indexPosition = transform.GetSiblingIndex();
		}
	}

	float _angle;
	public float angle {
		get { return _angle; }
		set {
			_angle = value;
			transform.eulerAngles = new Vector3(0, 0, value);
		}
	}

	bool _isSelected = false;
	public bool isSelected {
		get { return _isSelected; }
		set {

			if (value)
				transform.eulerAngles = new Vector3(0, 0, 0);
			else
				transform.eulerAngles = new Vector3(0, 0, _angle);

			_isSelected = value;

		}
	}

	public float width {
		get { return 200f; }
	}

	void Update() {

		Vector2 target = targetPosition;

		if (isSelected)
			target = new Vector2(targetPosition.x, targetPosition.y + ySelectedOffset);			

		if (transform.position.x != target.x || transform.position.y != target.y) {
			float step = moveSpeed * Time.deltaTime * 100;
			transform.position = Vector2.MoveTowards(transform.position, target, step);
		}

	}

	public void setPosition(Vector2 position) {
		transform.position = position;
	}	

	public void OnPointerDown(PointerEventData eventData) {
	}

	public void OnPointerUp(PointerEventData eventData) {
	}

	public void OnPointerEnter(PointerEventData eventData) {
		isSelected = true;
		transform.SetSiblingIndex(99);
	}

	public void OnPointerExit(PointerEventData eventData) {
		isSelected = false;
		transform.SetSiblingIndex(indexPosition);
	}

}
