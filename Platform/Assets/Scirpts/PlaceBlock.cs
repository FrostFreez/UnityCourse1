using UnityEngine;
using UnityEngine.Tilemaps;

public class PlaceBlock : MonoBehaviour
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Grid grid;
    [SerializeField] private GameObject tileMapPrefab;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile tile;
    void Start()
    {
        input = GetComponent<PlayerInput>();
        sr = GetComponentInChildren<SpriteRenderer>();
        grid = FindAnyObjectByType<Grid>();
        tilemap = Instantiate(tileMapPrefab, grid.transform).GetComponent<Tilemap>();
    }

    void Update()
    {
        if (input.usePressed)
        {
            Vector3Int pos = grid.WorldToCell(transform.position + (Vector3.right * (sr.flipX ? -1 : 1)));
            tilemap.SetTile(pos, tile);
        }
    }
}
