using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Objects")]
    [SerializeField] private GameObject fishBook;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            fishBook.SetActive(!fishBook.activeSelf);
    }
}
