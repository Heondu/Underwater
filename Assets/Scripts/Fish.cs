using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private new string name;
    public Sprite Sprite => sprite;
    public string Name => name;

    private void OnBecameVisible()
    {
        FishBook.Instance.ActivateFish(name);
    }
}
