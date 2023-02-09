using System.Collections.Generic;
using UnityEngine;

public class UISetBlock : MonoBehaviour
{
    public static UISetBlock Instance { get; private set; }
    [SerializeField] private Transform blockUsing;
    [SerializeField] private Transform blocksPrepare;

    public void SetBlockUsing(Block block)
    {
        foreach (Transform child in blockUsing)
            Destroy(child.gameObject);
        GameObject prefab = Resources.Load<GameObject>($"UI/Blocks/{block.Name}");
        Instantiate(prefab, blockUsing);
    }

    public void SetBlocksPrepare(List<Block> blocks)
    {
        foreach (Transform child in blocksPrepare)
            Destroy(child.gameObject);
        foreach (var block in blocks)
        {
            GameObject prefab = Resources.Load<GameObject>($"UI/Blocks/{block.Name}");
            Instantiate(prefab, blocksPrepare);
        }
    }

    private void Awake()
    {
        Instance = this;
    }
}
