using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHand : MonoBehaviour {

	[Header("Card Position Options")]
	public float overlapPercent = 0.5f;
	public float defaultYOffset = 60f;

	List<Card> cards;

	float panelWidth;

	void Awake() {
		cards = new List<Card>();
		panelWidth = ((RectTransform)transform).rect.width;		
	}

	void Start () {

		for(int i = 0; i < 4; i++) {
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

			card.position = new Vector2(x, defaultYOffset);
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

		if (position < middleCard) {
			return offset * -1;
		} else if (position > middleCard) {
			return offset;
		} else {
			return 0;
		}

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

		Debug.Log(string.Format("p: {0}, d: {1}, mo: {2}, o: {3}", position, d, middleOffset, offset));

		return middleOffset + d * offset;

	}

	private float getCardYOffset(Card card, int position) { return 0f; }

	private float getCardRotation(Card card, int position) { return 0f; }


		
}
