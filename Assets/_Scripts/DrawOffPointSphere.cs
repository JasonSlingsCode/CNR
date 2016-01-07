using UnityEngine;
using System.Collections;

public class DrawOffPointSphere : MonoBehaviour {

	public Transform ThisTransform;

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(ThisTransform.position, (transform.localScale.x / 2));
	}
}
