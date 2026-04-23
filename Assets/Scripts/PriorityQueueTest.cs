using UnityEngine;
using Random = UnityEngine.Random;
public class PriorityQueueTest : MonoBehaviour
{
    private PriorityQueue<float, int> queue = new PriorityQueue<float, int>();
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            queue.Enqueue(Random.Range(-10f, 10f), Random.Range(0, 100));
        }


        Debug.Log(queue.PrintData());
    }
}
