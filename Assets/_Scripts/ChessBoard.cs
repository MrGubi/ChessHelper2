using UnityEngine;
using Vuforia;

public class ChessBoard : MonoBehaviour
{
    public GameObject planePrefab;
    public ImageTargetBehaviour marker1;
    public ImageTargetBehaviour marker2;
    private Vector3 gridSize;
    private Vector3 gridStartPos;
    public bool GeneratedFlag = false;
    private void Start()
    {
        
    }

    private void Update(){
        if(!GeneratedFlag)
        if((marker1.TargetStatus.Status == Status.TRACKED || marker1.TargetStatus.Status == Status.EXTENDED_TRACKED) && (marker2.TargetStatus.Status == Status.TRACKED || marker2.TargetStatus.Status == Status.EXTENDED_TRACKED)){
            GenerateGrid();
            GeneratedFlag = !GeneratedFlag;
        }
    }

    private void GenerateGrid(){
        gridSize = marker2.transform.position - marker1.transform.position;
        gridStartPos = marker1.transform.position + (gridSize / 2f);

        // Instantiate the planes and position them to create the grid
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Vector3 planePos = gridStartPos + new Vector3(i * gridSize.x / 8f, 0, j * gridSize.z / 8f);
                Quaternion planeRot = Quaternion.Euler(0f, 0f, 0f);
                GameObject plane = Instantiate(planePrefab, planePos, planeRot);
                plane.transform.SetParent(marker1.transform);
            }
        }
    }
}