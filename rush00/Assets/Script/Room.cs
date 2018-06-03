using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour {
	//	list of checkPoint, for exemple if you are in a room and you want to go to the 4th room,
	// get the 4th component of the list, and it will give you the correct checkPoint's gameObject
	public List<GameObject>	CorrectCheckpoint = new List<GameObject>();
	public int				roomNumber;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject && other.gameObject.tag == "Character")
			other.gameObject.GetComponent<EnemisScript>().currentRoom = gameObject;
		if (other.gameObject && other.gameObject.tag == "Player")
			other.gameObject.transform.GetChild(0).GetComponent<PlayerScript>().currentRoom = gameObject;
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject && other.gameObject.tag == "Character")
			other.gameObject.GetComponent<EnemisScript>().currentRoom = gameObject;
		if (other.gameObject && other.gameObject.tag == "Player")
			other.gameObject.transform.GetChild(0).GetComponent<PlayerScript>().currentRoom = gameObject;
	}
}
