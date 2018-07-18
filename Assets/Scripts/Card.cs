﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Card : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, 
	IPointerDownHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler {

	Vector2 targetPosition;
	Canvas canvas;

	float moveSpeed = 15f;	

	public Vector2 position {
		get { return targetPosition; }
		set {
			targetPosition = value;			
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

			zoomed = value;
			_isSelected = value;

			if (value) {
				transform.eulerAngles = new Vector3(0, 0, 0);
				canvas.sortingOrder = 99;				
				selectedEvent.Invoke(this);
			} else { 
				canvas.sortingOrder = _sortOrder;
				transform.eulerAngles = new Vector3(0, 0, _angle);
				deselectedEvent.Invoke(this);
			}

		}
	}

	Animator animator;
	bool zoomed {
		get { return animator.GetBool("zoomed"); }
		set { animator.SetBool("zoomed", value); }
	}

	public float width {
		get {
			return ((RectTransform)transform).rect.width * transform.localScale.x;
		}
	}

	int _sortOrder;
	public int sortOrder {
		set {
			_sortOrder = value;
			canvas.sortingOrder = value;
		}
	}

	[System.Serializable]
	public class SelectedEvent : UnityEvent<Card> { }
	public SelectedEvent selectedEvent;

	[System.Serializable]
	public class DeselectedEvent : UnityEvent<Card> { }
	public DeselectedEvent deselectedEvent;

	void Awake () {

		if (selectedEvent == null) selectedEvent = new SelectedEvent();
		if (deselectedEvent == null) deselectedEvent = new DeselectedEvent();

		animator = GetComponent<Animator>();
		canvas = GetComponent<Canvas>();
	}

	void Update() {

	Vector2 target = new Vector2(targetPosition.x, targetPosition.y);

		if (isSelected)
			target = new Vector2(targetPosition.x, 0);	

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
	}

	public void OnPointerExit(PointerEventData eventData) {
		isSelected = false;			
	}

	public void OnSelect(BaseEventData eventData) {
		isSelected = true;		
	}

	public void OnDeselect(BaseEventData eventData) {
		isSelected = false;		
	}

}
