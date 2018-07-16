using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHand : MonoBehaviour {

	[Header("Card Position Options")]
	public float overlapPercent = 0.4f;
	public float baseYOffset = 40f;
	public float curveYOffset = 10f;
	public int rotationFactor = 3;
	public int startCards = 6;

	List<Card> cards;

	float panelWidth;

	void Awake() {
		cards = new List<Card>();
		panelWidth = ((RectTransform)transform).rect.width;		
	}

	void Start () {

		for(int i = 0; i < startCards; i++) {
			GameObject card = Instantiate(Resources.Load<GameObject>("Prefabs/Card"), transform);
			addCard(card.GetComponent<Card>());
		}

	}

	public void addCard(Card card) {		
		
		cards.Add(card);

		rebuildHand();

	}

	private void rebuildHand() {		

		for (int i = 0; i < cards.Count; i++) {

			Card card = cards[i];

			float xOffset = getCardXOffset(card.width, i);
			float x = (panelWidth / 2) + xOffset;
			float y = baseYOffset + getCardYOffset(i);
			
			card.position = new Vector2(x, y);
			card.angle = getCardRotation(i);

			card.sortOrder = i;

		}
	}

	private float getCardRotation(int position) {
		bool isOddNumber = (cards.Count % 2) == 0 ? false : true;

		if (isOddNumber) {
			return getOddRotation(position);
		} else {
			return getEvenRotation(position);
		}

	}

	private float getOddRotation(int position) {

		int middleCard = Mathf.FloorToInt(cards.Count / 2);

		int d = 0;
		if (position > middleCard)
			d = position - middleCard + 1;
		else if (position < middleCard)
			d = ((middleCard - position) + 1) * -1;

		if (position != middleCard) {
			return rotationFactor * -d;
		} else {
			return 0;
		}

	}

	private float getEvenRotation(int position) {

		int middleRight = cards.Count / 2;
		int middleLeft = middleRight - 1;

		if (position == middleLeft)
			return rotationFactor;
		else if (position == middleRight)
			return rotationFactor * -1;

		int d = 0;
		if (position > middleRight)
			d = position - middleRight + 1;
		else if (position < middleLeft)
			d = ((middleLeft - position) + 1)* -1;		

		return rotationFactor * d * -1;		

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

	private float getCardYOffset(int position) {
		
		bool isOddNumber = (cards.Count % 2) == 0 ? false : true;

		if (isOddNumber) {
			return getCardYOddOffset(position);
		} else {
			return getCardYEvenOffset(position);
		}

	}

	private float getCardYOddOffset(int position) {

		int middleCard = Mathf.FloorToInt(cards.Count / 2);

		int d = 0;
		if (position > middleCard)
			d = position - middleCard + 1;
		else if (position < middleCard)
			d = (middleCard - position) + 1;

		if (position != middleCard) {
			return curveYOffset * -d;
		} else {
			return 0;
		}

	}

	private float getCardYEvenOffset(int position) {

		int middleRight = cards.Count / 2;
		int middleLeft = middleRight - 1;		

		int d = 0;
		if (position > middleRight)
			d = position - middleRight;
		else if (position < middleLeft)
			d = (middleLeft - position);

		//Debug.Log(string.Format("p: {0}, d: {1}", position, d));

		return curveYOffset * d * -1;

	}
		
}
