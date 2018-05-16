using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour {

    [SerializeField] GameObject playerToFollow;
    Vector3 offset;

    // Use this for initialization
    void Start ()
    {
        offset = transform.localPosition - playerToFollow.transform.localPosition;
    }

    // Update is called once per frame
    void LateUpdate ()
    {

        transform.position = playerToFollow.transform.position + offset;
      
	}
}
