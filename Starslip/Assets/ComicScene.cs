using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicScene : MonoBehaviour
{
    public GameObject comic1;
    public GameObject comic2;
    public GameObject comic3;

    // Start is called before the first frame update
    void Start()
    {
        comic1.SetActive(true);
        comic2.SetActive(false);
        comic3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (comic1.activeSelf)
            {
                comic1.SetActive(false);
                comic2.SetActive(true);

            }
            else if (comic2.activeSelf)
            {
                comic2.SetActive(false);
                comic3.SetActive(true);

            }
            else if (comic3.activeSelf)
            {
                comic3.SetActive(false);
                SceneManager.LoadScene("Main");

            }
            else
            {
                Debug.LogError("Comics Scene Error");                
            }
        }
    }
}
