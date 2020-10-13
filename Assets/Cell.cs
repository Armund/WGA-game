using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
	public GameObject cube;
	public GameObject selecter;
	private GameObject currentSelecter;

	// координаты ячейки на игровом поле
	private int x, y;

	public enum CellType {EMPTY, BLOCK, RED, GREEN, BLUE};
	private CellType cellType;

	private void setCubeMaterial(Material material) {
		cube.GetComponent<MeshRenderer>().material = material;
	}

	public void setCellType(CellType type) {
		this.cellType = type;
		switch (type) {
			case CellType.EMPTY:
				setCubeMaterial(Materials.instance.materials[0]);
				break;
			case CellType.BLOCK:
				setCubeMaterial(Materials.instance.materials[1]);
				break;
			case CellType.RED:
				setCubeMaterial(Materials.instance.materials[2]);
				break;
			case CellType.GREEN:
				setCubeMaterial(Materials.instance.materials[3]);
				break;
			case CellType.BLUE:
				setCubeMaterial(Materials.instance.materials[4]);
				break;
		}
	}

	public bool isCellOfType(CellType cellType) {
		return this.cellType == cellType;
	}

	public void selectCell() {
		currentSelecter = Instantiate(selecter, transform.position, Quaternion.identity);
	}

	public void unselectCell() {
		Destroy(currentSelecter.gameObject);
	}

	public void setXY (int valX, int valY) {
		x = valX;
		y = valY;
	}

	public int getX() {
		return x;
	}

	public int getY() {
		return y;
	}
}
