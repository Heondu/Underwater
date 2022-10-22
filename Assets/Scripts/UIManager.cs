using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField] private GameObject fishBook;

    private void Update()
    {
        if (PlayerInput.FishBook)
            fishBook.SetActive(!fishBook.activeSelf);
    }
}
