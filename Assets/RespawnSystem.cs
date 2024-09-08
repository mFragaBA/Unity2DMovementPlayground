using System;
using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    PlayerController playerController;

    [SerializeField]
    [Range(0f, 1f)]
    float speedScale;

    [SerializeField]
    float waitTimeWhileFadedOut = 1f;

    Vector3 levelStartPosition;
    Texture2D texture;

    float alpha = 0f;
    float time = 0f;
    // Whether it's fading in or out or doing nothing
    int direction = 0;

    public void RespawnPlayer()
    {
        StartCoroutine(InitRespawnSequence());
    }

    IEnumerator InitRespawnSequence()
    {
        playerController.FreezePlayer();
        yield return new WaitUntil(() => direction == 0);
        FadeOut();
        yield return new WaitUntil(() => direction == 0);
        ReloactePlayer();
        yield return new WaitForSeconds(waitTimeWhileFadedOut);
        FadeIn();
        yield return new WaitUntil(() => direction == 0);
        playerController.UnfreezePlayer();
    }

    void Start()
    {
        alpha = 0;
        levelStartPosition = playerTransform.position;
        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, new Color(Color.black.r, Color.black.g, Color.black.b, alpha));
        texture.Apply();
    }

    void OnGUI()
    {
        if (alpha > 0f) GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
        if (direction != 0)
        {
            time += direction * Time.deltaTime * speedScale;
            alpha = Mathf.Lerp(0f, 1f, time);
            texture.SetPixel(0, 0, new Color(Color.black.r, Color.black.g, Color.black.b, alpha));
            texture.Apply();
            if (alpha <= 0f || alpha >= 1f) direction = 0;
        }
    }

    private void FadeIn()
    {
        direction = -1;
        time = 0f;
    }

    private void ReloactePlayer()
    {

        playerTransform.position = levelStartPosition;
    }

    private void FadeOut()
    {
        direction = 1;
        time = 0f;
    }
}
