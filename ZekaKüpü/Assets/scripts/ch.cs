 
using UnityEngine;
using UnityEngine.SceneManagement;

public class ch : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
     [SerializeField] AudioClip crash;
     [SerializeField] ParticleSystem  successParticles;
     [SerializeField] ParticleSystem crashParticles;

     AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisable = false;

      void Start()
      {
        audioSource = GetComponent<AudioSource> ();
     }

      void Update() 
      {
         RespondToDebugKey();
     }

     void RespondToDebugKey()
     {
      if(Input.GetKeyDown(KeyCode.L))
      {
         LoadNextLevel();
      }
      else if(Input.GetKeyDown(KeyCode.C ) )
         {
            collisionDisable = !collisionDisable;

         }
      
     }



   void OnCollisionEnter(Collision other)
  
     {
         if(isTransitioning  || collisionDisable) { return; }


         switch(other.gameObject.tag)
           {
            case "Friendly":
               Debug.Log("friendly");
               break;
            case "Finish":
               StartSuccess();
               break;
            default:
               StartCrashSequence();
               break;
        
           }
         }
         void StartSuccess()
         {
            isTransitioning = true;
            audioSource.Stop();
            audioSource.PlayOneShot(success);
            successParticles.Play();
            GetComponent<movement>().enabled = false;
            Invoke("LoadNextLevel", levelLoadDelay);
         }

         void StartCrashSequence()
         {
             isTransitioning = true;
             audioSource.Stop();

            audioSource.PlayOneShot(crash);
            crashParticles.Play();
            GetComponent<movement>().enabled = false;
            Invoke("ReloadLevel" , levelLoadDelay);
         }


         void LoadNextLevel()
         {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex =currentSceneIndex + 1;
            if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
            SceneManager.LoadScene(nextSceneIndex );
         }

        void  ReloadLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);

        }
}
