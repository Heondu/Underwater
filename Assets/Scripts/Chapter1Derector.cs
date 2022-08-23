using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chapter1Derector : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private PlayerRigidbody playerRigidbody;
    [SerializeField]
    private GameObject clownfish;
    [SerializeField]
    private GameObject pieceOfLight;

    public void OnPlayerReachPieceOfLight()
    {
        StartCoroutine(ClownfishRoutine());
    }

    private IEnumerator ClownfishRoutine()
    {
        StopInput();
        playerRigidbody.UseGravity(false);
        clownfish.SetActive(true);

        Vector3 start = clownfish.transform.position;
        Vector3 end = pieceOfLight.transform.position + Vector3.right * 0.25f;
        yield return StartCoroutine(MoveRoutine(start, end, 2));

        yield return new WaitForSeconds(1f);

        pieceOfLight.transform.SetParent(clownfish.transform);
        clownfish.transform.localScale = Vector3.one;
        yield return StartCoroutine(MoveRoutine(clownfish.transform.position, start, 2));

        PlayInput();
        playerRigidbody.UseGravity(true);
    }

    private IEnumerator MoveRoutine(Vector3 start, Vector3 end, float t)
    {
        float percent = 0;
        while (percent <= 1)
        {
            percent += Time.deltaTime / t;
            clownfish.transform.position = Vector3.Lerp(start, end, percent);
            yield return null;
        }
    }

    public void OnClownfishReachPieceOfLight()
    {

    }

    private void StopInput()
    {
        PlayerInput.inputState = InputState.Stop;
    }

    private void PlayInput()
    {
        PlayerInput.inputState = InputState.Play;
    }
}
