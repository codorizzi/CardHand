using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiscardPile : MonoBehaviour {

	TextMeshProUGUI text;

	List<Card> cards;

	int count {
		set { text.SetText(value.ToString()); }
	}

	void Awake() {
		text = GetComponentInChildren<TextMeshProUGUI>();
		cards = new List<Card>();
		text.SetText(cards.Count.ToString());
	}

}
