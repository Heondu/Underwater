using UnityEngine;
using UnityEngine.Events;

public class TombstonePuzzle : MonoBehaviour
{
    [SerializeField]
    private int eventID;

    [SerializeField]
    private Transform center;
    private Transform player;

    [SerializeField]
    private float radius;

    private bool isSolve = false;

    public UnityEvent onPuzzleSolved = new UnityEvent();

    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
    }

    private void Update()
    {
        if (isSolve)
            return;
        if (!EventManager.CheckEventFlag(eventID))
            return;

        Vector3 center = new Vector3(this.center.position.x, this.center.position.y, 0);
        Vector3 player = new Vector3(this.player.position.x, this.player.position.y, 0);
        if (Vector3.Distance(center, player) <= radius && this.player.localScale.x == -1)
        {
            isSolve = true;
            onPuzzleSolved.Invoke();
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (center == null)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(center.position, radius);
    }
}
