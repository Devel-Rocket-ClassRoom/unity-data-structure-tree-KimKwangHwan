using UnityEngine;

public class TreeTest : MonoBehaviour
{
    private void Start()
    {
        var bst = new BinarySearchTree<string, string>();

        bst["123"] = "ABC";
        bst["3"] = "DEF";
        bst["6654"] = "DDD";
        bst["39"] = "GHJ";
        bst["gad"] = "UIE";
        bst["113o3"] = "HGDN";
        bst["e12s3"] = "QW";
        bst["gh4l3"] = "OPPOS";
        bst["hguwe"] = "SJO";

        //foreach (var pair in bst)
        //{
        //    Debug.Log(pair);
        //}

        //Debug.Log($"123 Contains: {bst.ContainsKey("123")}");
        //bst.Remove("123");
        //Debug.Log($"123 Contains: {bst.ContainsKey("123")}");

        foreach (var pair in bst.LevelOrderTraversal())
        {
            Debug.Log(pair);
        }
    }
}
