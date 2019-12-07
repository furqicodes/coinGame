using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject ground;
    public GameObject blob;
    public GameObject coin;

    [Header("Environment Properties")]
    public int gridSize = 3;
    public int coinToSpawn = 10;
    public int generationNum;
    private bool nextGen = false;

    [Header("Genetic Algorithm")]
    public double[] bestGenes;
    [SerializeField]
    public List<string> bestFitness;

    private void Start()
    {
        NewPopulation();
    }

    private void Update()
    {
        if (nextGen)
        {
            NewPopulation();
            nextGen = false;
        }
    }

    public void NextGen() => nextGen = true;

    void NewPopulation()
    {
        generationNum++;
        for (int i = 0; i < gridSize; i++)//Matrix row (x-axis)
        {
            for (int j = 0; j < gridSize; j++)//Matrix column (z-axis)
            {
                try
                {
                    //Base spawning
                    GameObject ground_temp = Instantiate(ground);
                    ground_temp.transform.position = new Vector3(25 * i, 0, 25 * j);
                    //ground_temp.GetComponent<GroundManager>().index = (int)(j * Mathf.Pow(gridSize, 0) + i * Mathf.Pow(gridSize, 1));

                    //Blob spawning
                    GameObject blob_temp = Instantiate(blob, new Vector3(0 + 25 * i, 0.5f, 0 + 25 * j), Quaternion.identity);
                    blob_temp.transform.parent = ground_temp.transform;
                    blob_temp.GetComponent<Blob>().index = (int)(j * Mathf.Pow(gridSize, 0) + i * Mathf.Pow(gridSize, 1));

                    //Coin spawning
                    for (int k = 0; k < coinToSpawn; k++)
                    {
                        GameObject tmp = Instantiate(coin);
                        tmp.transform.position = new Vector3(Random.Range(-10 + 25 * i, 10 + 25 * i), 0.5f, Random.Range(-10 + 25 * j, 10 + 25 * j));
                        tmp.transform.parent = ground_temp.transform;
                        blob_temp.GetComponent<Blob>().coins.Add(tmp);
                    }
                }
                catch (System.Exception ex)
                {
                    print(ex);
                }
            }
        }
    }

}
