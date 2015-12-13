using UnityEngine;
using System.Collections;
using Vuforia;

public class VideoController : MonoBehaviour {

	//private bool mVideoIsPlaying;
	private VideoPlaybackBehaviour currentVideo;
	

	// Update is called once per frame
	void Update () {
		if (Input.touchCount == 1) {
			if (Input.touches [0].phase == TouchPhase.Began) {
				HandleSingleTap ();
			}
		}
	}

	/// <summary>
	/// Handle single tap event
	/// </summary>
	private void HandleSingleTap()
	{
		
		if (VuforiaRuntimeUtilities.IsPlayMode())
		{
			if (PickVideo(Input.touches [0].position) != null)
				Debug.LogWarning("Playing videos is currently not supported in Play Mode.");
		}
		
		// Find out which video was tapped, if any
		currentVideo = PickVideo(Input.touches [0].position);
		
		if (currentVideo != null)
		{
//			if (IsFullScreenModeEnabled())
//			{
//				if (currentVideo.VideoPlayer.IsPlayableFullscreen())
//				{
//					//On Android, we use Unity's built in player, so Unity application pauses before going to fullscreen. 
//					//So we have to handle the orientation from within Unity. 
//					#if UNITY_ANDROID
//					Screen.orientation = ScreenOrientation.LandscapeLeft;
//					#endif
//					// Pause the video if it is currently playing
//					currentVideo.VideoPlayer.Pause();
//					
//					// Seek the video to the beginning();
//					currentVideo.VideoPlayer.SeekTo(0.0f);
//					
//					// Display the busy icon
//					currentVideo.ShowBusyIcon();
//					
//					// Play the video full screen
//					StartCoroutine( PlayFullscreenVideoAtEndOfFrame(currentVideo) );
//					
//					UpdateFlashSettingsInUIView();
//				}
//			}
//			else
//			{
				if (currentVideo.VideoPlayer.IsPlayableOnTexture())
				{
					// This video is playable on a texture, toggle playing/paused
					
					VideoPlayerHelper.MediaState state = currentVideo.VideoPlayer.GetStatus();
					if (state == VideoPlayerHelper.MediaState.PAUSED ||
					    state == VideoPlayerHelper.MediaState.READY ||
					    state == VideoPlayerHelper.MediaState.STOPPED)
					{
						// Pause other videos before playing this one
						PauseOtherVideos(currentVideo);
						
						// Play this video on texture where it left off
						currentVideo.VideoPlayer.Play(false, currentVideo.VideoPlayer.GetCurrentPosition());
					}
					else if (state == VideoPlayerHelper.MediaState.REACHED_END)
					{
						// Pause other videos before playing this one
						PauseOtherVideos(currentVideo);
						
						// Play this video from the beginning
						currentVideo.VideoPlayer.Play(false, 0);
					}
					else if (state == VideoPlayerHelper.MediaState.PLAYING)
					{
						// Video is already playing, pause it
						currentVideo.VideoPlayer.Pause();
					}
				}
				else
				{
					// Display the busy icon
					currentVideo.ShowBusyIcon();
					
					// This video cannot be played on a texture, play it full screen
//					StartCoroutine( PlayFullscreenVideoAtEndOfFrame(currentVideo) );
				}
//			}
		}
	}

//	public static IEnumerator PlayFullscreenVideoAtEndOfFrame(VideoPlaybackBehaviour video)
//	{
//		Screen.orientation = ScreenOrientation.AutoRotation;
//		Screen.autorotateToPortrait = true;
//		Screen.autorotateToPortraitUpsideDown = true;
//		Screen.autorotateToLandscapeLeft = true;
//		Screen.autorotateToLandscapeRight = true;
//		
//		yield return new WaitForEndOfFrame ();
//		
//		// we wait a bit to allow the ScreenOrientation.AutoRotation to become effective
//		yield return new WaitForSeconds (0.3f);
//		
//		video.VideoPlayer.Play(true, 0);
//	}
	
	//Flash turns off automatically on fullscreen videoplayback mode, so we need to update the UI accordingly
//	private void UpdateFlashSettingsInUIView()
//	{
//		VideoPlaybackUIEventHandler handler = GameObject.FindObjectOfType(typeof(VideoPlaybackUIEventHandler)) as VideoPlaybackUIEventHandler;
//		if (handler != null)
//		{
//			// add code as needed if you have set the flash on
//		}
//	}
	
	/// <summary>
	/// Checks to see if the 'Play FullScreen' Mode is enabled/disabled in the UI Menu
	/// </summary>
	/// <returns></returns>
//	private bool IsFullScreenModeEnabled()
//	{
//		VideoPlaybackUIEventHandler handler = FindObjectOfType(typeof(VideoPlaybackUIEventHandler)) as VideoPlaybackUIEventHandler;
//		if (handler != null)
//		{
//			return handler.mFullScreenMode;
//		}
//		
//		return false;
//	}
	
	/// <summary>
	/// Find the video object under the screen point
	/// </summary>
	private VideoPlaybackBehaviour PickVideo(Vector3 screenPoint)
	{
		VideoPlaybackBehaviour[] videos = (VideoPlaybackBehaviour[])
			FindObjectsOfType(typeof(VideoPlaybackBehaviour));
		
		GameObject go = VuforiaManager.Instance.ARCameraTransform.gameObject;
		Camera[] cam = go.GetComponentsInChildren<Camera> ();
		Ray ray = cam[0].ScreenPointToRay(screenPoint);
		
		RaycastHit hit = new RaycastHit();
		
		foreach (VideoPlaybackBehaviour video in videos)
		{
			if (video.GetComponent<Collider>().Raycast(ray, out hit, 10000))
			{
				return video;
			}
		}
		
		return null;
	}
	
	/// <summary>
	/// Pause all videos except this one
	/// </summary>
	private void PauseOtherVideos(VideoPlaybackBehaviour currentVideo)
	{
		VideoPlaybackBehaviour[] videos = (VideoPlaybackBehaviour[])
			FindObjectsOfType(typeof(VideoPlaybackBehaviour));
		
		foreach (VideoPlaybackBehaviour video in videos)
		{
			if (video != currentVideo)
			{
				if (video.CurrentState == VideoPlayerHelper.MediaState.PLAYING)
				{
					video.VideoPlayer.Pause();
				}
			}
		}
	}
}
