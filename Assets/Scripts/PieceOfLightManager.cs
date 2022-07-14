using UnityEngine;
using UnityEngine.Events;

public class PieceOfLightManager : MonoBehaviour
{
    private static int pieceOfLightNum = 0;
    public static int PieceOfLightNum => pieceOfLightNum;

    public static UnityEvent<int> onPieceOfLightAdded = new UnityEvent<int>();

    public static void AddPieceOfLight()
    {
        pieceOfLightNum += 1;
        onPieceOfLightAdded.Invoke(pieceOfLightNum);
    }
}
