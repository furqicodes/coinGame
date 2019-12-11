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
    public Vector3[] coinPositions = new Vector3[10];
    public int generationNum;
    private bool nextGen = false;

    [Header("Genetic Algorithm")]
    public double[] bestGenes;
    [SerializeField]
    public List<string> bestFitness;

    [Header("Timer Properties")]
    public float maxTime = 60f;
    [SerializeField]
    private float countDown;

    private void Start()
    {
        NewPopulation();
        countDown = maxTime;
    }

    private void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0)
        {
            countDown = maxTime;

            //Genetic algorithm
            bestGenes = BestGenes();
            //Prepare environment for next generation
            foreach (var item in GameObject.FindGameObjectsWithTag("Ground"))
            {
                Destroy(item);
            }
            NextGen();
        }
        if (nextGen)
        {
            NewPopulation();
            nextGen = false;
        }
    }

    public void NextGen() => nextGen = true;

    private double[] BestGenes()
    {
        float f = Mathf.NegativeInfinity;
        Blob temp_blob;
        GameObject fittest = null;
        foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
        {
            temp_blob = item.GetComponent<Blob>();
            if (temp_blob.CalculateFitness() > f)
            {
                f = temp_blob.CalculateFitness();
                fittest = item;
            }
        }
        bestFitness.Add("Score: " + f.ToString() + " Index: " + fittest.GetComponent<Blob>().index);
        return fittest.GetComponent<Blob>().weights;
    }

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

                    //Coin spawning (disabled for non random coin generation)
                    for (int k = 0; k < coinToSpawn; k++)
                    {
                        GameObject tmp = Instantiate(coin);
                        tmp.transform.position = new Vector3(coinPositions[k].x + 25 * i, coinPositions[k].y, coinPositions[k].z + 25 * j);
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
