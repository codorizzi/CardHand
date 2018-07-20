using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMat : MonoBehaviour {	
	
	public DiscardPile discardPile;
	public DrawPile drawPile;
	public CardHand cardHand;
	
	void Start() {
		
		for(int i = 0; i < 20; i++) {
			 GameObject go = Instantiate(Resources.Load<GameObject>("Prefabs/Card"), drawPile.transform);
			drawPile.addCard(go.GetComponent<Card>());
		}

		for (int i = 0; i < 8; i++)
			StartCoroutine(drawCard());
	}

	public IEnumerator drawCard() {

		Card card = drawPile.drawCard();

		card.isVisible = true;
		card.GetComponent<Animator>().Play("draw");

		card.setPosition(drawPile.transform.position);
		card.position = drawPile.transform.position;
		cardHand.addCard(card);			

		yield return null;
	}

	public IEnumerable playCard() {
		// 

		yield return null;
	}

	public IEnumerable discardCard() {

		yield return null;

	}

}
