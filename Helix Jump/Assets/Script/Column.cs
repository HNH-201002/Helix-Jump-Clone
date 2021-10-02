using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
	[SerializeField] private float rotationSpeed = 0.2f;
	public GameObject column;

	public void OnMouseDrag()
	{
		if (GameManager.instance.isDead == false) {
			float XaxisRotation = Input.GetAxis("Mouse X") * rotationSpeed;
			float YaxisRotation = Input.GetAxis("Mouse Y") * rotationSpeed;
			// select the axis by which you want to rotate the GameObject
			column.transform.RotateAround(Vector3.down, XaxisRotation);
			column.transform.RotateAround(Vector3.up, YaxisRotation);
		}
	}
}
