using UnityEngine;
using UnityEngine.EventSystems;

public class UISoundEffect : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    private AudioClip clip;

    public void OnPointerDown(PointerEventData eventData)
    {
        SoundManager.Instance.PlaySFX(clip);
    }
}
