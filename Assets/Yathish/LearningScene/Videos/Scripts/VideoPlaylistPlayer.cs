using UnityEngine;
using UnityEngine.Video;
using System.Collections.Generic;

public class VideoPlaylistPlayer : MonoBehaviour
{
    public VideoPlayer videoPlayer;          
    public List<VideoClip> videoClips;       
    public bool loopPlaylist = false;        

    private int currentVideoIndex = 0;

    void Start()
    {
        if (videoClips.Count == 0 || videoPlayer == null)
        {
            Debug.LogWarning("VideoPlayer or videoClips list not assigned.");
            return;
        }

        videoPlayer.loopPointReached += OnVideoFinished;
        PlayVideo(currentVideoIndex);
    }

    void PlayVideo(int index)
    {
        if (index >= 0 && index < videoClips.Count)
        {
            videoPlayer.clip = videoClips[index];
            videoPlayer.Play();
        }
    }

    void OnVideoFinished(VideoPlayer vp)
    {
        PlayNextVideo();
    }

    // 🔘 Call this from a UI Button to go to the next video
    public void PlayNextVideo()
    {
        currentVideoIndex++;

        if (currentVideoIndex >= videoClips.Count)
        {
            if (loopPlaylist)
            {
                currentVideoIndex = 0;
            }
            else
            {
                Debug.Log("All videos played.");
                return;
            }
        }

        PlayVideo(currentVideoIndex);
    }

    // Optional: 🔘 Call this to go back to the previous video
    public void PlayPreviousVideo()
    {
        currentVideoIndex--;

        if (currentVideoIndex < 0)
        {
            if (loopPlaylist)
            {
                currentVideoIndex = videoClips.Count - 1;
            }
            else
            {
                currentVideoIndex = 0;
                Debug.Log("Already at the first video.");
                return;
            }
        }

        PlayVideo(currentVideoIndex);
    }
}