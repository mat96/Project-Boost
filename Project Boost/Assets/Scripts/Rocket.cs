using UnityEngine;
using UnityEngine.SceneManagement;


public class Rocket : MonoBehaviour
{
    [SerializeField] float rcsThrust = 250f; 
    [SerializeField] float MainThrust = 250f; 
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip onDeath;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;

  

    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State {Alive, Dying, Transcending }
    State state = State.Alive;
    bool collisionIsDisabled = false;

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


        if (Debug.isDebugBuild)
        {
            DebugKeys();
        }
    }

     // Rocket States are here
    void OnCollisionEnter(Collision collision)
    {
        
        
            if (state != State.Alive || collisionIsDisabled) { return; }

            switch (collision.gameObject.tag)
            {
                case "Friendly":

                    break;
                case "Finish":
                audioSource.Stop();
                state = State.Transcending;
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
        deathParticles.Play();
        Invoke("ReloadLevel", levelLoadDelay);
        
    }

    private void ReloadLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
       state = State.Alive;
        SceneManager.LoadScene(currentLevel); 
    }

    private void Thrust()
    {

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            rigidBody.AddRelativeForce(Vector3.up * MainThrust * Time.deltaTime);

            if (!audioSource.isPlaying)
            {
                mainEngineParticles.Play();
                audioSource.PlayOneShot(mainEngine);
            }

        }
       
        if (Input.GetKey(KeyCode.S)) 
        {
            rigidBody.AddRelativeForce(Vector3.up * -MainThrust * Time.deltaTime);

            if (!audioSource.isPlaying)
            {
                mainEngineParticles.Play();
                audioSource.PlayOneShot(mainEngine);
            }

        }
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
        {

            audioSource.Stop();
            mainEngineParticles.Stop();

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

    void DebugKeys()
    {
     
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionIsDisabled = !collisionIsDisabled;
            
        }
    }
}


