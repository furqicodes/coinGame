using UnityEngine;

public class Timer : MonoBehaviour
{
    [Header("Timer Properties")]
    public float maxTime = 60f;
    [SerializeField]
    private float countDown;

    //Genetic algorithm
    private GridManager g;

    void Start()
    {
        //Genetic algorithm
        g = GameObject.Find("GameObject").GetComponent<GridManager>();

        countDown = maxTime;
    }

    [System.Obsolete]
    void Update()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0)
        {
            countDown = maxTime;

            //Genetic algorithm
            g.bestGenes = BestGenes();
            //Prepare environment for next generation
            foreach (var item in GameObject.FindGameObjectsWithTag("Ground"))
            {
                Destroy(item);
            }
            g.NextGen();
        }
    }

    private double[] BestGenes()
    {
        float f = Mathf.NegativeInfinity;
        Blob blob;
        GameObject fittest = null;
        foreach (var item in GameObject.FindGameObjectsWithTag("Player"))
        {
            blob = item.GetComponent<Blob>();
            if (blob.CalculateFitness() > f)
            {
                f = blob.CalculateFitness();
                fittest = item;
            }
        }
        GameObject.Find("GameObject").GetComponent<GridManager>().bestFitness.Add("Score: " + f.ToString() + " Index: " + fittest.GetComponent<Blob>().index);
        return fittest.GetComponent<Blob>().weights;

    }
}
