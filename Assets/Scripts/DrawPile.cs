using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DrawPile : MonoBehaviour {

	TextMeshProUGUI text;

	List<Card> cards;

	int count {
		set { text.SetText(value.ToString()); }
	}

	void Awake() {
		text = GetComponentInChildren<TextMeshProUGUI>();
		cards = new List<Card>();		
	}

	public void addCard(Card card) {
		cards.Add(card);
		text.SetText(cards.Count.ToString());
	}

	public Card drawCard() {

		if (cards.Count == 0)
			return null;

		Card card = cards[0];
		cards.Remove(card);

		text.SetText(cards.Count.ToString());

		return card;

	}

}
