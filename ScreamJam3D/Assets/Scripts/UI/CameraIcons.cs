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
        // ¯\_(ツ)_/¯

        float height = GetComponentInParent<RectTransform>().rect.width / 3;
        float width = GetComponentInParent<RectTransform>().rect.height / (float)1.3;

        leftArrow.transform.localPosition = new Vector3(-width - 10, 0, 0);
        rightArrow.transform.localPosition = new Vector3(width + 10, 0, 0);
        upArrow.transform.localPosition = new Vector3(0, height - 20, 0);
        downArrow.transform.localPosition = new Vector3(0, -height + 20, 0);
    }

    /// <summary>
    /// sets the arrows based on the active frame
    /// </summary>
    /// <param name="frame">where the player is looking</param>
    public void CameraChange(PlayerLookState frame)
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
