using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;
using Random = UnityEngine.Random;
public class VisualizeTree : MonoBehaviour
{
    public enum Type
    {
        Pow,
        Level,
        InOrder
    }
    public Type type = Type.Pow;
    private BinarySearchTree<string, string> bst = new BinarySearchTree<string, string>();
    public GameObject nodePrefab;

    private readonly Dictionary<object, Vector3> nodePositions = new();
    public float horizontalSpacing = 2.0f;
    public float verticalSpacing = 2.0f;
    
    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            bst[(Random.Range(0, 100).ToString())] = (Random.Range(0, 100)).ToString();
        }
        switch(type)
        {
            case Type.Pow:
                nodePositions.Clear();
                Pow(bst.root, Vector3.zero, bst.root.Height);
                break;
            case Type.Level:
                Level(bst.root);
                break;
                //case Type.InOrder:
                //    InOrder(bst.root, null, 0f, 0f);
                //    break;
        }
        Draw();
    }

    private void Pow(TreeNode<string, string> node, Vector3 pos, int height)
    {
        if (node != null)
        {
            nodePositions[node] = pos;
            float offset = horizontalSpacing * 0.5f * Mathf.Pow(2, height);

            Vector3 childBase = pos + Vector3.down * verticalSpacing;

            Pow(node.Left, childBase + Vector3.left * offset, height - 1);
            Pow(node.Right, childBase + Vector3.right * offset, height - 1);
            //GameObject me = Instantiate(nodePrefab, new Vector3(xPos, yPos, 0f), Quaternion.identity);
            //me.GetComponentInChildren<TextMeshPro>().text = $"K: {node.Key}\nV: {node.Value}\nH: {node.Height}";
            //if (parent != null)
            //{
            //    LineRenderer line = me.GetComponent<LineRenderer>();
            //    line.positionCount = 2;
            //    line.SetPosition(0, me.transform.position);
            //    line.SetPosition(1, parent.transform.position);
            //}
            //if (node.Left != null)
            //{
            //    Pow(node.Left, me, xPos - Mathf.Pow(2, node.Height), yPos - 4f);
            //}
            //if (node.Right != null)
            //{
            //    Pow(node.Right, me, xPos + Mathf.Pow(2, node.Height), yPos - 4f);
            //}
        }
    }

    private void Level(TreeNode<string, string> root)
    {
        if (root != null)
        {
            var levels = new List<List<TreeNode<string, string>>>();
            var queue = new Queue<(TreeNode<string, string> node, int depth)>();

            queue.Enqueue((root, 0));

            while (queue.Count > 0)
            {
                var (node, depth) = queue.Dequeue();
                while (levels.Count <= depth)
                {
                    levels.Add(new List<TreeNode<string, string>>());
                }
                levels[depth].Add(node);

                if (node.Left != null)
                {
                    queue.Enqueue((node.Left, depth + 1));
                }
                if (node.Right != null)
                {
                    queue.Enqueue((node.Right, depth + 1));
                }
            }
            for (int depth = 0; depth < levels.Count; depth++)
            {
                float y = -depth * verticalSpacing;
                var row = levels[depth];

                for (int i = 0; i < row.Count; i++)
                {
                    nodePositions[row[i]] = new Vector3(i * horizontalSpacing, y, 0f);
                }
            }
            //float xPos = 3f * stack;
            //GameObject me = Instantiate(nodePrefab, new Vector3(xPos, yPos, 0f), Quaternion.identity);
            //me.GetComponentInChildren<TextMeshPro>().text = $"K: {node.Key}\nV: {node.Value}\nH: {node.Height}";

            //if (parent != null)
            //{
            //    LineRenderer line = me.GetComponent<LineRenderer>();
            //    line.positionCount = 2;
            //    line.SetPosition(0, me.transform.position);
            //    line.SetPosition(1, parent.transform.position);
            //}
            //if (node.Left != null)
            //{
            //    Level(node.Left, me, yPos - 3f, 2f * stack);
            //}
            //if (node.Right != null)
            //{
            //    Level(node.Right, me, yPos - 3f, 2f * stack + 1);
            //}
        }
    }

    private void InOrder(TreeNode<string, string> node, GameObject parent, float xPos, float yPos)
    {
        if (node != null)
        {
            GameObject me = Instantiate(nodePrefab, new Vector3(xPos, yPos, 0f), Quaternion.identity);
            me.GetComponentInChildren<TextMeshPro>().text = $"K: {node.Key}\nV: {node.Value}\nH: {node.Height}";

            if (parent != null)
            {
                LineRenderer line = me.GetComponent<LineRenderer>();
                line.positionCount = 2;
                line.SetPosition(0, me.transform.position);
                line.SetPosition(1, parent.transform.position);
            }
            if (node.Left != null)
            {
                InOrder(node.Left, me, xPos - 4f * node.Left.Height, yPos - 3f);
            }
            if (node.Right != null)
            {
                InOrder(node.Right, me, xPos + 4f * node.Right.Height, yPos - 3f);
            }
        }
    }

    private void Draw()
    {
        foreach (TreeNode<string, string> node in nodePositions.Keys)
        {
            GameObject me = Instantiate(nodePrefab, nodePositions[node], Quaternion.identity);

            if (node.Left != null)
            {
                CreateLine(nodePositions[node], nodePositions[node.Left]);
            }
            if (node.Right != null)
            {
                CreateLine(nodePositions[node], nodePositions[node.Right]);
            }
        }
    }

    private void CreateLine(Vector3 from, Vector3 to)
    {
        GameObject lineObj = new GameObject("Line");
        LineRenderer line = lineObj.AddComponent<LineRenderer>();

        line.positionCount = 2;
        line.SetPosition(0, from);
        line.SetPosition(1, to);

        line.startWidth = 0.2f;
        line.endWidth = 0.2f;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = Color.white;
        line.endColor = Color.white;
    }
}
