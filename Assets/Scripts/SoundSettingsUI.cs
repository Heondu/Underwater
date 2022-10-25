using UnityEngine;
using UnityEngine.Events;

public class SoundSettingsUI : MonoBehaviour
{
    [SerializeField] private GameObject uiObject;

    public UnityEvent onCancelPressed = new UnityEvent();
    public UnityEvent onAcceptPressed = new UnityEvent();
    public UnityEvent onUIOpened = new UnityEvent();
    public UnityEvent onUIClosed = new UnityEvent();

    private bool isUIOpen = false;
    public bool IsUIOpen => isUIOpen;

    private void Start()
    {
        CloseUI();
    }

    private void Update()
    {
        if (PlayerInput.SoundSettings)
        {
            if (!isUIOpen)
                OpenUI();
            else
                Accept();
        }
    }

    public void Cancel()
    {
        CloseUI();
        onCancelPressed.Invoke();
    }

    public void Accept()
    {
        CloseUI();
        onAcceptPressed.Invoke();
    }

    public void OpenUI()
    {
        Time.timeScale = 0;
        isUIOpen = true;
        uiObject.SetActive(true);
        onUIOpened.Invoke();
    }

    public void CloseUI()
    {
        Time.timeScale = 1;
        isUIOpen = false;
        uiObject.SetActive(false);
        onUIClosed.Invoke();
    }
}