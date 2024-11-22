using Assets.WorldGeneration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldBuilderController : MonoBehaviour
{
    public int Seed;
    private int Width = 10, Height = 10;
    public List<TileChance> TilePrefabs;
    public float TileSize = 1;

    // Set the TilePool in the inspector 
    public TileObjectPool TilePool; 
    // List to track the active Tiles
    private List<GameObject> activeTiles = new List<GameObject>(); 

    [System.Serializable]
    public class TileChance
    {
        public GameObject Prefab;
        public int Chance;
    }

    public void Analyse(List<IWorldTile> world)
    {
        var typeIds = world.Select(x => x.TypeId);
        var distinctIds = typeIds.Distinct().ToList();
        foreach (var id in distinctIds)
        {
            Debug.Log($"{id} count = '{typeIds.Count(t => t == id)}'");
        }
    }

    public void Display(List<IWorldTile> world)
    {
        Analyse(world);

        foreach (var tile in world)
        {
            var prefab = TilePrefabs[tile.TypeId].Prefab;
            var tileInstance = TilePool.Get(prefab);
            tileInstance.transform.SetParent(this.transform);
            tileInstance.SetActive(true);
            tileInstance.transform.localPosition = new Vector2(tile.X * TileSize, tile.Y * TileSize);
            activeTiles.Add(tileInstance);
        }
    }


    public void Build()
    {
        IWorldFactory factory = WorldFactory.Get(WorldFactory.WorldFactoryGeneratorType.Basic).Seed(Seed).GridSize(Width, Height);
        for (int id = 0; id < TilePrefabs.Count; id++)
        {
            factory.CreateTiles(id, TilePrefabs[id].Chance);
        }
        Display(factory.Build());
    }

    private void OnEnable()
    {
        Build();
    }

    [ContextMenu("Rebuild Map")]
    public void Rebuild()
    {
        Debug.Log("Rebuilding Map with Seed: " + Seed);
        // Start the coroutine for delayed rebuild
        StartCoroutine(RebuildCoroutine());
    }

    private IEnumerator RebuildCoroutine()
    {
        //Destroy or Returns Old Tiles
        for (int i = this.transform.childCount - 1; i >= 0; i--)
        {
            var child = this.transform.GetChild(i).gameObject;
            Debug.Log($"Destroying or returning: {child.name}");
            TilePool.Return(child); 
            yield return null; 
        }

        //Second delay before rebulding
        yield return new WaitForSeconds(1f); 

        //New seed and calls Build Function 
        Seed = Random.Range(0, 100000000);
        Debug.Log("New Seed: " + Seed);
        Build();
    }
}