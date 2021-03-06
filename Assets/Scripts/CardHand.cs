﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;

public class CardHand : MonoBehaviour {

	public static CardHand instance;

	[Header("Card Position Options")]
	public float overlapPercent = 0.6f;
	public float baseYOffset = 40f;		
	public int startCards = 6;

	List<Card> cards;

	public GameObject discardPile;
	public GameObject drawPile;

	float panelWidth;

	[System.Serializable]
	public class CardSelected : UnityEvent<Card> {}
	public CardSelected cardSelected;

	[System.Serializable]
	public class CardDeselected : UnityEvent<Card> { }
	public CardDeselected cardDeselected;

	void Awake() {

		if (cardSelected == null) cardSelected = new CardSelected();
		if (cardDeselected == null) cardDeselected = new CardDeselected();

		cards = new List<Card>();
		panelWidth = ((RectTransform)transform).rect.width;
		instance = this;
	}

	void Start () {

		/*for(int i = 0; i < startCards; i++) {
			GameObject card = Instantiate(Resources.Load<GameObject>("Prefabs/Card"), transform);
			//drawCard(card.GetComponent<Card>());
		}*/

	}

	public void addCard(Card card) {
	
		cards.Add(card);
		card.transform.SetParent(transform);

		card.selectedEvent.AddListener(handleCardSelect);
		card.deselectedEvent.AddListener(handleCardDeselect);
		card.clickedEvent.AddListener(handleCardClick);

		rebuildHand();

	}

	public void discardCard(Card card) {

	}

	public void playCard(Card card) {
		//card.played = true;
	}

	private void rebuildHand() {		

		for (int i = 0; i < cards.Count; i++) {

			Card card = cards[i];

			float xOffset = getCardXOffset(card.width, i);
			float x = (panelWidth / 2) + xOffset;
			float y = baseYOffset;
			
			card.position = new Vector2(x, y);

			Card left = null;
			if (i > 0)
				left = cards[i - 1];

			Card right = null;
			if (i < cards.Count - 1)
				right = cards[i + 1];

			setNavigation(card, left, right);

			card.sortOrder = i;

		}
	}

	private float getCardXOffset(float cardWidth, int position) {

		float offset = cardWidth * overlapPercent;		

		bool isOddNumber = (cards.Count % 2) == 0 ? false : true;		

		if (isOddNumber) {
			return getOddXOffset(offset, position);
		} else {
			return getEvenXOffset(offset, position);			
		}
		
	}

	private float getOddXOffset(float offset, int position) {

		int middleCard = Mathf.FloorToInt(cards.Count / 2);

		int d = 0;
		if (position > middleCard)
			d = position - middleCard;
		else if (position < middleCard)
			d = (middleCard - position) * -1;

		if (position != middleCard) 
			return offset * d;		
		else
			return 0;		

	}

	private float getEvenXOffset(float offset, int position) {

		int middleRight = cards.Count / 2;
		int middleLeft = middleRight - 1;

		float middleOffset = 0.5f * offset;
		if (position <= middleLeft)
			middleOffset = (offset / 2) * -1;
		else if (position >= middleRight)
			middleOffset = (offset / 2);		

		int d = 0;
		if (position > middleRight)
			d = position - middleRight;
		else if (position < middleLeft)
			d = (middleLeft - position) * -1;		

		return middleOffset + d * offset;

	}

	private void handleCardSelect(Card card) {
		cardSelected.Invoke(card);			
	}

	private void handleCardDeselect(Card card) {
		cardDeselected.Invoke(card);		
	}		

	private void handleCardClick(Card card) {		
	}

	private void setNavigation(Card card, Card left, Card right) {

		Selectable
			leftButton = null, 
			rightButton = null;

		if (left != null)
			leftButton = left.GetComponent<Selectable>();

		if (right != null)
			rightButton = right.GetComponent<Selectable>();		

		Navigation nav = card.GetComponent<Button>().navigation;

		nav.selectOnLeft = leftButton;
		nav.selectOnRight = rightButton;

		card.GetComponent<Button>().navigation = nav;		

	}

}
