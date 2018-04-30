using UnityEngine;
using UnityEngine.SceneManagement;


public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 250f; // Changable in the editor
    [SerializeField] float MainThrust = 250f; // Changable in the editor
    [SerializeField] float reloadTime = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip onDeath;
    [SerializeField] AudioClip success;


    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State {Alive, Dying, Transcending }
    State state = State.Alive;

   	// Use this for initialization
	void Start()
    {
        
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (state == State.Alive)
        {
            Thrust();
            Rotation();
        }
	}

     // Rocket States are here
    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; }
        
            switch (collision.gameObject.tag)
            {
            case "Friendly":

                    break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:  // For when the player dies
                StartDeathSequence();
                break;
        }
        
    }

    private void StartDeathSequence()
    {
        audioSource.Stop();
        state = State.Dying;
        audioSource.PlayOneShot(onDeath);
        Invoke("ReloadLevel", reloadTime);
        // kill player
    }

    private void StartSuccessSequence()
    {
        audioSource.Stop();
        state = State.Transcending;
        audioSource.PlayOneShot(success);
        Invoke("LoadNextScene", reloadTime); // parameterise time
    }

    private void ReloadLevel()
    {
       state = State.Alive;
        SceneManager.LoadScene(0); // TODO make this reload current level
    }

    private void LoadNextScene()
    {
        state = State.Alive;
        SceneManager.LoadScene(1);
    }

    private void Thrust()
    {

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            rigidBody.AddRelativeForce(Vector3.up * MainThrust);

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }

        }
       
        if (Input.GetKey(KeyCode.S))  //TODO maybe add another key for Rocket Break
        {
            rigidBody.AddRelativeForce(Vector3.up * -MainThrust);

            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }

        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {
            audioSource.Stop();
        }

    }

    private void Rotation()
    {
        rigidBody.freezeRotation = true; // Manually take control of the rotation

        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {

            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidBody.freezeRotation = false; // physics take back control of rotation
 

    }


}


