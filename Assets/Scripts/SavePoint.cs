using UnityEngine;
using UnityEngine.Events;

public class SavePoint : MonoBehaviour
{
    [SerializeField] private GameObject glowOutline;
    [SerializeField] private GameObject glowInner;
    [SerializeField] private int index;
    [SerializeField] private UnityEvent onSaved = new UnityEvent();
    private Animator animator;
    private bool isActive = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Activate()
    {
        isActive = true;
        glowOutline.SetActive(false);
        glowInner.SetActive(true);
        animator.SetTrigger("active");
        GameSaveManager.Save();
        onSaved.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive)
            return;

        if (other.CompareTag("Player"))
            glowOutline.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (isActive)
            return;

        if (other.CompareTag("Player"))
            glowOutline.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (isActive)
            return;

        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Activate();
        }
    }
}
