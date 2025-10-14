using UnityEngine;
using UnityEngine.UI;

public class CameraIcons : MonoBehaviour
{
    //using GameObject instead of specifics to change use the SetActive function.
    //which will make the whole arrow invisible, without the need of a second command for the child image
    [SerializeField]
    private GameObject leftArrow, rightArrow, upArrow, downArrow;

    void Start()
    {
        CameraManager.Instance.OnCameraChange += CameraChange;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// sets the arrows based on the active frame
    /// </summary>
    /// <param name="frame">where the player is looking</param>
    public void CameraChange(PlayerLookState frame)
    {
        if (leftArrow != null &&
            rightArrow != null &&
            upArrow != null &&
            downArrow != null)
        {
            switch (frame)
            {
                case PlayerLookState.None:
                    leftArrow.SetActive(false);
                    rightArrow.SetActive(false);
                    upArrow.SetActive(false);
                    downArrow.SetActive(false);
                    break;

                case PlayerLookState.SteeringWheel:
                    leftArrow.SetActive(true);
                    rightArrow.SetActive(true);
                    upArrow.SetActive(true);
                    downArrow.SetActive(true);
                    break;


                case PlayerLookState.LeftWindow:
                    leftArrow.SetActive(false);
                    rightArrow.SetActive(true);
                    upArrow.SetActive(false);
                    downArrow.SetActive(false);
                    break;

                case PlayerLookState.RightWindow:
                    leftArrow.SetActive(true);
                    rightArrow.SetActive(true);
                    upArrow.SetActive(false);
                    downArrow.SetActive(false);
                    break;

                case PlayerLookState.Pedals:
                    leftArrow.SetActive(false);
                    rightArrow.SetActive(false);
                    upArrow.SetActive(true);
                    downArrow.SetActive(false);
                    break;

                case PlayerLookState.Fridge:
                    leftArrow.SetActive(true);
                    rightArrow.SetActive(false);
                    upArrow.SetActive(false);
                    downArrow.SetActive(false);
                    break;

                case PlayerLookState.SunRoof:
                    leftArrow.SetActive(false);
                    rightArrow.SetActive(false);
                    upArrow.SetActive(false);
                    downArrow.SetActive(true);
                    break;

            }
        }
    }
}
