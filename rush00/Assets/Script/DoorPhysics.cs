using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPhysics : MonoBehaviour {

	private const float	smooth = 2.0f;
	private const float	DoorOpenAngle = 90.0f;
	private const float	DoorCloseAngle = 0.0f;
	private bool	open = false;

	void Update ()
	{
		if (open == true)
		{
			Quaternion target = Quaternion.Euler(DoorOpenAngle, 0, 0);
			transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * smooth);
		}
		else
		{
			Quaternion target = Quaternion.Euler(DoorCloseAngle, 0, 0);
			transform.localRotation = Quaternion.Slerp(transform.localRotation, target, Time.deltaTime * smooth);

		 }
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Character" || other.gameObject.tag == "Player")
		{
			open = true;
			StartCoroutine("CloseDoor");
		}
	}

	IEnumerator CloseDoor()
    {
        yield return new WaitForSeconds(5);
		open = false;
    }
}
