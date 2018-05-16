using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class LoadNextScene : MonoBehaviour
{
    [SerializeField] float loadLevelDelay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem successParticles;


    AudioSource audioSource;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.tag == "Player" )
       {

            audioSource.PlayOneShot(success);
            successParticles.Play();
            Invoke("PlayNextScene", loadLevelDelay);


       }
    }

    void Update()
    {
        if(Debug.isDebugBuild)
        { 
            //Debug Key
            // Load next scene
            if (Input.GetKey(KeyCode.L))
            {
                PlayNextScene();
            }
        }
    }

    void PlayNextScene()
    {
        int ScenesInGame = SceneManager.sceneCountInBuildSettings;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int NextSceneIndex = currentSceneIndex + 1;
  
        if (NextSceneIndex < ScenesInGame)
        {
            SceneManager.LoadScene(NextSceneIndex);
        }
        else
        {
            // Loads Scene 1 when game is completed
            SceneManager.LoadScene(0);
        }

    }
}
