using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	// ссылки на другие объекты
	public Canvas gameOverMessage;
	public Field field;

	// переиспользуемые ссылки
	GameObject hittenObj;
	Cell cell1;
	Cell cell2;

	// utility
	bool isCellSelected;
	
    void Start()
    {
		cell1 = null;
		cell2 = null;
		isCellSelected = false;
		gameOverMessage.gameObject.SetActive(false);
    }

	void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);			

			if (Physics.Raycast(ray, out RaycastHit hit)) {
				hittenObj = hit.collider.gameObject;

				if (!isCellSelected) {
					hittenObj.TryGetComponent(out cell1);
					cell1.selectCell();
					isCellSelected = true;
				} else {
					hittenObj.TryGetComponent(out cell2);
					field.swapCells(cell1, cell2);
					cell1.unselectCell();
					isCellSelected = false;
					cell1 = null;
					cell2 = null;
					hittenObj = null;
					if (field.isGameOver()) {
						gameOverMessage.gameObject.SetActive(true);
					}
				}
			}
		}
    }
}
