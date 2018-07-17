using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHand : MonoBehaviour {

	public static CardHand instance;

	[Header("Card Position Options")]
	public float overlapPercent = 0.4f;
	public float baseYOffset = 40f;		
	public int startCards = 6;

	List<Card> cards;

	float panelWidth;

	void Awake() {
		cards = new List<Card>();
		panelWidth = ((RectTransform)transform).rect.width;
		instance = this;
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
			float y = baseYOffset;
			
			card.position = new Vector2(x, y);

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
		
}
