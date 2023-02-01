using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public List<Tile> piecePositions;
    public GameObject chessContainer;
    public GameObject[] chessTiles;
    public GameObject movePrefab;
    public GameObject movePrefab2;
    public bool White;
    public Vector2Int Pos;
    void Start()
    {
        chessTiles = GameObject.FindGameObjectsWithTag("Tile");
        piecePositions = new List<Tile>();
    }



    public void UpdatePiecePosition()
    {
        RemoveMoves();
        RemoveOldPositions();
        for (int i = 0; i < chessContainer.transform.childCount; i++){
        Transform chessPiece = chessContainer.transform.GetChild(i);

        Tile piece = chessPiece.GetComponent<Tile>();

        piecePositions.Add(piece);
        }     
    }

    private void RemoveOldPositions()
    {
        piecePositions.Clear();
    }

    public void CheckViableMoves(string name,bool white, Vector2Int position){
        White = white;
        RemoveMoves();
        List<Vector2Int> viableMoves = new List<Vector2Int>();
        switch(name){
            case "Bauer":
                if(white){
                    Pos.x = position.x;
                    Pos.y = position.y;
                    if (position.y == 6)
                    {   
                        Pos.y = position.y - 2;
                        if(IsValidMove(Pos))
                        {
                            viableMoves.Add(Pos);
                        }
                        
                        Pos.y = position.y - 1;
                        if(IsValidMove(Pos))
                        {
                            viableMoves.Add(Pos);
                        }
                    }
                    else
                    {
                        Pos.y = position.y - 1;
                        if(IsValidMove(Pos))
                        {
                            viableMoves.Add(Pos);
                        }
                    }
                    Pos.x = position.x+1;
                    Pos.y = position.y-1;
                    if(IsPieceAt(Pos)){
                        viableMoves.Add(Pos);
                    }
                    Pos.x = position.x-1;
                    Pos.y = position.y-1;
                    if(IsPieceAt(Pos)){
                        viableMoves.Add(Pos);
                    }
                }else{
                    Pos.x = position.x;
                    Pos.y = position.y;
                    if (position.y == 1)
                    {   
                        Pos.y = position.y + 2;

                        if(IsValidMove(Pos))
                        {
                            viableMoves.Add(Pos);
                        }
                        
                        Pos.y = position.y + 1;

                        if(IsValidMove(Pos))
                        {
                            viableMoves.Add(Pos);
                        }
                    }
                    else
                    {
                        Pos.y = position.y + 1;

                        if(IsValidMove(Pos))
                        {
                            viableMoves.Add(Pos);
                        }
                    }
                    Pos.x = position.x+1;
                    Pos.y = position.y+1;
                    if(IsPieceAt(Pos)){
                        viableMoves.Add(Pos);
                    }
                    Pos.x = position.x-1;
                    Pos.y = position.y+1;
                    if(IsPieceAt(Pos)){
                        viableMoves.Add(Pos);
                    }
                }
            break;
            case "Läufer":       
                Pos.x = position.x - 1;
                Pos.y = position.y + 1;
                while (Pos.x >= 0 && Pos.y < 8)
                {
                    if (!IsPieceAt(Pos))
                    {
                        viableMoves.Add(Pos);
                    }
                    else
                    {
                        viableMoves.Add(Pos);
                        break;
                    }
                    Pos.x--;
                    Pos.y++;
                }

                Pos.x = position.x + 1;
                Pos.y = position.y + 1;
                while (Pos.x < 8 && Pos.y < 8)
                {
                    if (!IsPieceAt(Pos))
                    {
                        viableMoves.Add(Pos);
                    }
                    else
                    {
                        viableMoves.Add(Pos);
                        break;
                    }
                    Pos.x++;
                    Pos.y++;
                }

                Pos.x = position.x - 1;
                Pos.y = position.y - 1;
                while (Pos.x >= 0 && Pos.y >= 0)
                {
                    if (!IsPieceAt(Pos))
                    {
                        viableMoves.Add(Pos);
                    }
                    else
                    {
                        viableMoves.Add(Pos);
                        break;
                    }
                    Pos.x--;
                    Pos.y--;
                }

                Pos.x = position.x + 1;
                Pos.y = position.y - 1;
                while (Pos.x < 8 && Pos.y >= 0)
                {
                    if (!IsPieceAt(Pos))
                    {
                        viableMoves.Add(Pos);
                    }
                    else
                    {
                        viableMoves.Add(Pos);
                        break;
                    }
                    Pos.x++;
                    Pos.y--;
                }
            break;
            case "Dame":
                for (int x = -1; x <= 1; x++) {
                    for (int y = -1; y <= 1; y++) {
                        if (x != 0 || y != 0) {
                            Pos.x = position.x + x;
                            Pos.y = position.y + y;
                            
                            while (IsValidMove(Pos)) {
                                viableMoves.Add(Pos);
                                
                                if (IsPieceAt(Pos)) {
                                    break;
                                }
                                
                                Pos.x += x;
                                Pos.y += y;
                            }
                        }
                    }
                }
            break;
            case "König":
                Pos.x = position.x;
                Pos.y = position.y;
                    
                for (int i = position.x-1; i <= position.x+1; i++)
                {
                    Pos.x = i;
                    for (int j = position.y-1; j <= position.y+1; j++)
                    {
                        Pos.y = j;

                        if(IsValidMove(Pos))
                        {
                            viableMoves.Add(Pos);
                        }
                    }
                }
            break;
            case "Turm":
                Pos.x = position.x;
                Pos.y = position.y;
                for (int i = position.y + 1; i <=7 ; i++)
                {
                    Pos.y = i;
                    if(!IsPieceAt(Pos))
                    {
                        viableMoves.Add(Pos);
                    }else{
                        viableMoves.Add(Pos);
                        break;
                    }
                }

                for (int i = position.y - 1; i >=0 ; i--)
                {
                    Pos.y = i;

                    if(!IsPieceAt(Pos))
                    {
                        viableMoves.Add(Pos);
                    }else{
                        viableMoves.Add(Pos);
                        break;
                    }
                }

                Pos.y = position.y;
                for (int i = position.x +1; i <=7 ; i++)
                {
                    Pos.x = i;

                    if(!IsPieceAt(Pos)) 
                    {
                        viableMoves.Add(Pos);
                    }else{
                        viableMoves.Add(Pos);
                        break;
                    }
                }

                for (int i = position.x -1; i >=0 ; i--)
                {
                    Pos.x = i;
                    if(!IsPieceAt(Pos))
                    {
                        viableMoves.Add(Pos);
                    }else{
                        viableMoves.Add(Pos);
                        break;
                    }
                }
            break;
        }
        DrawMoves(viableMoves); 
    }
     public void DrawMoves(List<Vector2Int> moves){
        for(int i=0; i < moves.Count; i++){
            for(int j = 0;j < chessTiles.Length;j++){
                string[] parts = chessTiles[j].name.Split(' ');
                Vector2Int tile = new Vector2Int(int.Parse(parts[1]),int.Parse(parts[2]));
                if(tile == moves[i]){
                    Vector3 spawnPos = new Vector3(chessTiles[j].transform.position.x,chessTiles[j].transform.position.y + 0.2f,chessTiles[j].transform.position.z);
                    if (IsPieceAt(moves[i]) && IsPieceEnemy(moves[i])){
                        Instantiate(movePrefab2,spawnPos,Quaternion.identity);
                    }else if (IsPieceAt(moves[i]) && !IsPieceEnemy(moves[i])){

                    }else{
                        Instantiate(movePrefab,spawnPos,Quaternion.identity);
                    }
                }
            }
        }
     }

    private bool IsValidMove(Vector2Int pos) {
        return pos.x >= 0 && pos.x < 8 && pos.y >= 0 && pos.y < 8;
        }
        private bool IsPieceAt(Vector2Int pos) {
        for(int i=0;i < piecePositions.Count;i++){
            if(piecePositions[i].Position == pos){
                return true;
            }
        }  

        return false;
    }

    private bool IsPieceEnemy(Vector2Int pos){
        for(int i=0;i < piecePositions.Count;i++){
            if(piecePositions[i].Position == pos){
                if(!piecePositions[i].white == White){
                    return true;
                }             
            }
        }
        return false;
    }

    private void RemoveMoves(){
        GameObject[] moves = GameObject.FindGameObjectsWithTag("Move");
        foreach(GameObject move in moves){
            Destroy(move.gameObject);
        }
    }
}

