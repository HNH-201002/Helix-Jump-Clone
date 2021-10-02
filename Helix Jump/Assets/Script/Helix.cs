using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helix : MonoBehaviour
{
    public GameObject helix;
    public GameObject checkToSpawnHelix;
    public GameObject column;
    Vector3 newPos;
    List<GameObject> helixList = new List<GameObject>();

    [Tooltip("This is amount of helix adjustable")]
    [Range(1,100)]
    public int amountOfHelixChunk;
    [Tooltip("Space in each helix Chunk , placing it higher makes game easier")]
    [Range(1,7)]
    public int amountOfSpace;
    [Tooltip("amount of dead helix chunk, setting it higher makes the game harder")]
    [Range(1, 7)]
    public int amountOfDeadHelixChunk;
    // Start is called before the first frame update
    void Start()
    {
        newPos = transform.position + new Vector3(0,0,0);
        SpawnHelix();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnHelix()
    {
        int randAmountSpace = Random.Range(1, amountOfSpace);
        int randDeadHelix = Random.Range(0, amountOfDeadHelixChunk);
        for (int i = 0;i < amountOfHelixChunk; i++) {
            newPos.y -= 4.0f;                
            GameObject helixGameObject = Instantiate(helix.gameObject, newPos, Quaternion.identity).gameObject;
            helixList.Add(helixGameObject.gameObject);
           for (int j = 1; j <= randAmountSpace; j++)
            {
                int randPos1 = Random.Range(0, 7);
                helixGameObject.transform.GetChild(randPos1).gameObject.GetComponent<MeshRenderer>().enabled = false;
                helixGameObject.transform.GetChild(randPos1).localScale -= new Vector3(0,0, 44.0f);
                helixGameObject.transform.GetChild(randPos1).position -= new Vector3(0,0.24f, 0);
                helixGameObject.transform.GetChild(randPos1).GetComponent<MeshCollider>().isTrigger = true;
            }
            for (int k = 0; k <= randDeadHelix; k++)
            {
                int randPos2 = Random.Range(0, 7);
                if (helixGameObject.transform.GetChild(randPos2).GetComponent<MeshCollider>().isTrigger != true) {
                    helixGameObject.transform.GetChild(randPos2).gameObject.GetComponent<MeshRenderer>().material.color = new Color(255, 0, 0);
                    helixGameObject.transform.GetChild(randPos2).tag = "DeadGround";
                }
            }
            helixGameObject.transform.SetParent(column.transform);
        }
        GameObject checkToSpawnHelixGameObject = Instantiate(checkToSpawnHelix,helixList[helixList.Count -3].transform.position,Quaternion.identity);
        Destroy(helixList[helixList.Count - 3]);
        checkToSpawnHelixGameObject.transform.SetParent(column.transform);    
    }
    private void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.tag == "DeadGround")
        {
            GameManager.instance.GameOver();
        }
    }
}
