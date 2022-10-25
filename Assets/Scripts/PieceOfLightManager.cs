using UnityEngine;
using UnityEngine.Events;

public class PieceOfLightManager : MonoBehaviour
{
    private static PieceOfLightManager instance;
    public static PieceOfLightManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PieceOfLightManager>();
            return instance; 
        }
    }

    private int pieceOfLightNum = 0;
    public int PieceOfLightNum => pieceOfLightNum;

    public UnityEvent<int> onPieceOfLightAdded = new UnityEvent<int>();

    public void AddPieceOfLight()
    {
        pieceOfLightNum += 1;
        onPieceOfLightAdded.Invoke(pieceOfLightNum);
    }

    public void SetPieceOfLight(int num)
    {
        pieceOfLightNum = num;
        onPieceOfLightAdded.Invoke(pieceOfLightNum);
    }
}
