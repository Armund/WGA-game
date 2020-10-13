using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
	public Cell cellPrefab;

	// игровое поле
	Cell[,] cells = new Cell[5,5];

	// utility
	int[] cellsLeft = { 5, 5, 5 }; // red, green, blue cells осталось поместить

    void Start()
    {
		initField();
    }

	void initField() {
		// заполняем поле ячейками
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				cells[i, j] = Instantiate(cellPrefab, new Vector3(j,0,i), Quaternion.identity);
				cells[i, j].setXY(i, j);
			}
		}
		// ставим пустые ячейки
		for (int i = 1; i < 4; i+=2) {
			for (int j = 1; j < 4; j+=2) {
				cells[i, j].setCellType(Cell.CellType.EMPTY);
			}
		}
		// ставим блок ячейки
		for (int i = 0; i < 5; i+=2) {
			for (int j = 1; j < 5; j+=2) {
				cells[i,j].setCellType(Cell.CellType.BLOCK);
			}
		}
		// ставим цветные ячейки
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j+=2) {
				int random = getRandomCellType();
				switch (random) {
					case 0:
						cells[i, j].setCellType(Cell.CellType.RED);
						break;
					case 1:
						cells[i, j].setCellType(Cell.CellType.GREEN);
						break;
					case 2:
						cells[i, j].setCellType(Cell.CellType.BLUE);
						break;
					default:
						Debug.Log("Switch Case Default");
						break;
				}
			}
		}
	}

	private int getRandomCellType() {
		int random = Random.Range(0, 3);
		
		if (cellsLeft[random] > 0) {
			cellsLeft[random]--;
			return random;
		} else {
			return getRandomCellType();
		}		
	}

	public void swapCells(Cell cell1, Cell cell2) {
		// проверка на доступность перестановки
		if (!isSwapLegal(cell1, cell2)) {
			return;
		}

		// меняем позицию на сцене
		Vector3 positionBuffer = cell1.gameObject.transform.position;
		cell1.gameObject.transform.position = cell2.gameObject.transform.position;
		cell2.gameObject.transform.position = positionBuffer;

		// меняем координаты внутри ячейки
		int x1 = cell1.getX();
		int y1 = cell1.getY();
		int x2 = cell2.getX();
		int y2 = cell2.getY();

		cell1.setXY(x2, y2);
		cell2.setXY(x1, y1);

		// меняем ссылки в массиве
		cells[x1, y1] = cell2;
		cells[x2, y2] = cell1;
	}

	private bool isSwapLegal(Cell cell1, Cell cell2) {
		// если хотя бы одна ячейка - блок
		if (cell1.isCellOfType(Cell.CellType.BLOCK) || cell2.isCellOfType(Cell.CellType.BLOCK)) {
			return false;
		}

		// если переставляется не на пустую ячейку
		if (!cell1.isCellOfType(Cell.CellType.EMPTY) && !cell2.isCellOfType(Cell.CellType.EMPTY)) {
			return false;
		}

		// если ячейки стоят не рядом
		int dx = Mathf.Abs(cell1.getX() - cell2.getX());
		int dy = Mathf.Abs(cell1.getY() - cell2.getY());
		if (dx + dy != 1) {
			return false;
		}

		return true;
	}

	public bool isGameOver() {
		for (int i = 0; i < 5; i++) {
			if (!cells[i,0].isCellOfType(Cell.CellType.RED)) {
				return false;
			}
		}
		for (int i = 0; i < 5; i++) {
			if (!cells[i, 2].isCellOfType(Cell.CellType.GREEN)) {
				return false;
			}
		}
		for (int i = 0; i < 5; i++) {
			if (!cells[i, 4].isCellOfType(Cell.CellType.BLUE)) {
				return false;
			}
		}

		return true;
	}
}
