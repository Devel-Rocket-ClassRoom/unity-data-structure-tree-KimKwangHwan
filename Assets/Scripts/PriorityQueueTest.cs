using System;
using UnityEngine;
using Random = UnityEngine.Random;
public class PriorityQueueTest : MonoBehaviour
{
    void Start()
    {
        var pq = new PriorityQueue<string, (float dist, int hp)>(
        (a, b) =>
            {
                int cmp = a.dist.CompareTo(b.dist);
                return cmp != 0 ? cmp : a.hp.CompareTo(b.hp);
            }
        );

        pq.Enqueue("슬라임A", (5.0f, 30));
        pq.Enqueue("고블린", (3.0f, 50));
        pq.Enqueue("드래곤", (10.0f, 200));
        pq.Enqueue("슬라임B", (3.0f, 20));   // 고블린과 거리 같지만 HP 낮음
        pq.Enqueue("스켈레톤", (1.0f, 40));
        pq.Enqueue("좀비", (3.0f, 50));   // 고블린과 거리, HP 둘 다 같음
        pq.Enqueue("위치", (7.0f, 60));
        pq.Enqueue("오크", (1.0f, 80));   // 스켈레톤과 거리 같지만 HP 높음

        // 기대 순서:
        // 스켈레톤  (1, 40)
        // 오크      (1, 80)
        // 슬라임B   (3, 20)
        // 고블린    (3, 50)
        // 좀비      (3, 50)
        // 슬라임A   (5, 30)
        // 위치      (7, 60)
        // 드래곤    (10, 200)

        while(pq.Count > 0)
        {
            Debug.Log(pq.Dequeue());
        }
    }
}
