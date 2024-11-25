using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoMask : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public RenderTexture renderTexture;

    void Start()
    {
        // Ensure the video player target texture is set
        videoPlayer.targetTexture = renderTexture;

        // Optionally, start the video on play
        videoPlayer.Play();
    }
}
