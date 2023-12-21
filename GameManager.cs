using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> locations = new List<GameObject>();
    [SerializeField] Transform player;
    float levelLength = 106.3f;
    int count = 4;
    [SerializeField] GameObject shield;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(locations[0], transform.forward, transform.rotation);
        for (int i = 0; i < count; i++)
        {
            CreateLocation();
        }
        GenerateObject();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z > levelLength - 106.3f * count)
        {
            CreateLocation();
        }
    }

    void CreateLocation()
    {
        Instantiate(locations[Random.Range(0, locations.Count)], transform.forward * levelLength, transform.rotation);
        levelLength += 106.3f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void GenerateObject()
    {
        float distance = Random.Range(100,200);
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        Instantiate(shield, player.position + new Vector3(0, 2, distance), rotation);
        Invoke("GenerateObject", Random.Range(20,30));
    }
}
