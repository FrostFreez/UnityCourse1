using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceBlock : PlaceAbility
{
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject tileMapPrefab;
    [SerializeField] private Tilemap fences;
    [SerializeField] private Tile tile;
    protected override void Start()
    {
        base.Start();
        grid = FindAnyObjectByType<Grid>();
        fences = Instantiate(tileMapPrefab, grid.transform).GetComponent<Tilemap>();
        GameController.Instance.fences = fences;
    }
    protected override void Do()
    {
        Vector3Int pos = grid.WorldToCell(transform.position + (Vector3.right * (sr.flipX ? -1 : 1)));
        fences.SetTile(pos, tile);
    }
}
