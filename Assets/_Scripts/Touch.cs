using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Touch : MonoBehaviour
{

    public static Tile selectedPiece;
    public Controller con;
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began){
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit) && Hit.transform.tag == "ChessPiece"){
                selectedPiece = Hit.transform.parent.GetComponent<Tile>();
                Debug.Log(selectedPiece.name + "Touch");
                con.CheckViableMoves(selectedPiece.name,selectedPiece.white,selectedPiece.Position);
            }
        }
        if (Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit) && Hit.transform.tag == "ChessPiece"){
                selectedPiece = Hit.transform.parent.GetComponent<Tile>();
                //Debug.Log(selectedPiece.name);
            }
        }
    }
}
