using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blob : MonoBehaviour
{
    public int index;

    public List<GameObject> coins;
    private GameObject nearestCoin;

    private float totalDistance;
    private Vector3 lastPosition;

    //Genetic Algorithm
    [SerializeField]
    public double[] weights = new double[4];
    public double biasNode = -1d;
    private double[] oN = new double[2];
    private GridManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameObject").GetComponent<GridManager>();
        double[] best = gm.bestGenes;
        if (best.Length == 0)
        {
            //Genetic Algorithm
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = Random.Range(-1f, 1f);
            }
        }
        else
        {
            weights = MutateWeights();
        }

        lastPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Blob forward direction indicator
        Debug.DrawRay(transform.position, transform.forward, Color.red);

        //Total distance value for fitness calculation
        totalDistance += Vector3.Distance(lastPosition, transform.position);
        lastPosition = transform.position;

        if (coins.Count > 0)
        {
            nearestCoin = NearestCoin();

            //Genetic Algorithm
            double temp1 = weights[0] * DistanceToCoin(nearestCoin) + weights[2] * AngleToCoin() - biasNode;
            double temp2 = weights[1] * DistanceToCoin(nearestCoin) + weights[3] * AngleToCoin() - biasNode;
            oN[0] = Sigmoid(temp1);
            oN[1] = Sigmoid(temp2);
        }

    }

    public double getoN(int index)
    {
        return oN[index];
    }

    public float CalculateFitness()
    {
        ////Fitness Function 1
        //return (gm.coinToSpawn - coins.Count) * 100 - totalDistance;
        //Fitness Function 2
        return Mathf.Exp(gm.coinToSpawn - coins.Count) - totalDistance/10;
    }

    public double DistanceToCoin(GameObject game)
    {
        return Vector3.Distance(transform.position, game.transform.position);
    }

    public double DistanceToWall()
    {
        return 0d;
    }

    public double AngleToCoin()
    {
        if (nearestCoin.Equals(null))
        {
            return 0d;
        }
        else
        {
            Vector3 targetDir = nearestCoin.transform.position - transform.position;
            Vector3 forward = transform.forward;
            return Vector3.SignedAngle(targetDir, forward, Vector3.up);

        }
    }

    public GameObject NearestCoin()
    {
        float distance = Mathf.Infinity;
        GameObject nearest = null;

        for (int i = 0; i < coins.Count; i++)
        {
            float temp = Vector3.Distance(gameObject.transform.position, coins[i].transform.position);
            if (temp < distance)
            {
                distance = temp;
                nearest = coins[i];
            }
        }
        return nearest;
    }

    public double[] MutateWeights()
    {
        double[] bestGenes = gm.bestGenes;
        if (this.index == 0)
        {
            return bestGenes;
        }
        else
        {
            double[] tempWeights = new double[weights.Length];
            for (int i = 0; i < weights.Length; i++)
            {
                if (this.index < Mathf.Pow(gm.gridSize,2) / 2)
                {
                    ////Mutation type 1
                    //tempWeights[i] = GenerateNormalRandom(bestGenes[i] / 2, bestGenes[i] / 6);
                    //Mutation type 2
                    tempWeights[i] = bestGenes[i] + Random.Range(-1f, 1f) * 0.1f;
                }
                else
                {
                    tempWeights[i] = bestGenes[i] + Random.Range(-1f, 1f) * 0.5f;
                }
            }
            return tempWeights;
        }
    }

    public static double Sigmoid(double input)
    {
        return (1.0 / (1.0 + Mathf.Exp(-(float)input)));
    }

    //public static double GenerateNormalRandom(double mu, double sigma)
    //{
    //    float rand1 = Random.Range(0.0f, 1.0f);
    //    float rand2 = Random.Range(0.0f, 1.0f);

    //    double n = Mathf.Sqrt(-2.0f * Mathf.Log(rand1)) * Mathf.Cos((2.0f * Mathf.PI) * rand2);

    //    return (mu + sigma * n);
    //}

}
