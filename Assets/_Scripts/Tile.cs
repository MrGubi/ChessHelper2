using UnityEngine;
using Vuforia;

public class Tile : MonoBehaviour
{
    public ImageTargetBehaviour marker;
    public string name;
    public bool white;
    public GameObject[] chessTiles;

    public Vector2Int Position = new Vector2Int(8,8);
    private Vector2Int previousPosition;
    void Start(){
        chessTiles = GameObject.FindGameObjectsWithTag("Tile");
    }
    void Update()
    {
        if((marker.TargetStatus.Status == Status.TRACKED || marker.TargetStatus.Status == Status.EXTENDED_TRACKED)){
            Vector2 screenPos = Camera.main.WorldToScreenPoint(marker.transform.position);
            Ray ray = Camera.main.ScreenPointToRay(screenPos);
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                for (int i = 0; i < chessTiles.Length; i++)
                {
                    if (hit.collider.gameObject == chessTiles[i])
                    {
                        
                        string[] parts = chessTiles[i].name.Split(' ');

                        if(previousPosition.x != int.Parse(parts[1])||previousPosition.y != int.Parse(parts[2]))
                        {
                            Position.x = int.Parse(parts[1]);
                            Position.y = int.Parse(parts[2]);
                            Controller Con = GameObject.FindGameObjectWithTag("Con").GetComponent<Controller>();
                            Con.UpdatePiecePosition();
                            previousPosition = Position;                        
                            Debug.Log(Position);
                        }
                        
                    }
                }
            }
        }
    }
}