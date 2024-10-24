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

    [System.Serializable]
    public class TileChance
    {
        public GameObject Prefab;
        public int Chance;
    }

    public float TileSize = 1;

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
            var tileInstance = Instantiate(prefab, this.transform);
            tileInstance.transform.localPosition = new Vector2(tile.X * TileSize, tile.Y * TileSize);
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
        Seed = Random.Range(0, 100000000);

        for (int i = this.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
        Build();
    }

}