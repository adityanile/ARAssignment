using TMPro;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public GameObject dragon;
    public GameObject clickBtn;

    private RaycastHit hit;
    private Camera cam;
    private Ray ray;
    private Vector3 touchPos;

    public TextMeshProUGUI debugText;

    // Start The Animation When Click the Dragon and stop when clicked again
    private bool animBool = false;

    // Audio clips manager
    public AudioClip touch;
    public AudioClip credits;

    public AudioSource play;

    private void Awake()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        play = GetComponent<AudioSource>();
    }

    private void Update()
    {

        // Getting Touch Position from the user and converting it to ray
        if (Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchPos = Input.GetTouch(0).position;
                ray = cam.ScreenPointToRay(touchPos);
            }
        }

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.gameObject != null)
            {
                if (hit.collider.CompareTag("Dragon"))
                {
                    // Play the sound Always
                    play.PlayOneShot(touch, 1);

                    Animation anim = hit.collider.GetComponent<Animation>();

                    if (!animBool)
                    {
                        animBool = true;
                        anim.enabled = true;
                    }
                    else
                    {
                        animBool = false;
                        anim.enabled = false;
                    }
                }
            }

            ray = new Ray();
        }


    }

    public void OnClick()
    {
        dragon.SetActive(true);
        clickBtn.SetActive(false);
    }
    public void OnClickInfo()
    {
        play.PlayOneShot(credits, 1);
    }
}
