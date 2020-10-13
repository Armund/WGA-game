using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materials : MonoBehaviour
{
	public static Materials instance;
	public Material[] materials = new Material[5];

	private void Awake() {
		instance = this;
	}
}
