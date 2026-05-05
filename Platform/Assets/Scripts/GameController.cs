using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    [HideInInspector] public static GameController Instance { get; private set; }
    public PlayerController[] players;
    public House[] houses;
    public Vector3[] playerSpawnPoints;
    public List<Coin> coins;
    public List<Bomb> bombs;
    public Tilemap breakable;
    public Dictionary<Vector3Int, TileBase> tiles = new();
    public Tilemap fences;
    public bool ready = false;
    public delegate void ResetLevel();
    public ResetLevel resetLevel;
    public int levelIndex = 0;
    [SerializeField] private InputActionAsset input;
    [SerializeField] private GameObject winPanel;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        playerSpawnPoints = new Vector3[players.Length];
        for (int i = 0; i < players.Length; i++)
        {
            playerSpawnPoints[i] = players[i].transform.position;
        }
        for (int i = breakable.cellBounds.xMin; i < breakable.cellBounds.xMax; i++)
        {
            for (int j = breakable.cellBounds.yMin; j < breakable.cellBounds.yMax; j++)
            {
                Vector3Int pos = new(i, j);
                TileBase tile = breakable.GetTile(pos);
                if (tile != null)
                {
                    tiles.Add(pos, tile);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ResetGame();
        }
    }
    private void ResetGame()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].transform.position = playerSpawnPoints[i];
        }
        fences.ClearAllTiles();
        for (int i = 0; i < bombs.Count; i++)
        {
            Destroy(bombs[i].gameObject);
        }
        bombs.Clear();
        for (int i = 0; i < coins.Count; i++)
        {
            coins[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < houses.Length; i++)
        {
            houses[i].SetOpen(false);
        }
        foreach (KeyValuePair<Vector3Int, TileBase> tile in tiles)
        {
            breakable.SetTile(tile.Key, tile.Value);
        }
        resetLevel();
    }
    private void ResetGameInput(InputAction.CallbackContext context)
    {
        ResetGame();
    }
    public void AddBomb(Bomb bomb)
    {
        bomb.destroySelf += RemoveBomb;
        bombs.Add(bomb);
    }
    public void RemoveBomb(Bomb bomb)
    {
        bombs.Remove(bomb);
    }
    public void CheckCoins()
    {
        foreach (Coin coin in coins)
        {
            if (coin == null) continue;
            if (coin.gameObject.activeSelf) return;
        }
        for (int i = 0; i < houses.Length; i++)
        {
            houses[i].SetOpen(true);
        }
    }
    public void CheckHouses()
    {
        for (int i = 0; i < houses.Length; i++)
        {
            if (!houses[i].IsReady()) return;
        }
        Win();
    }
    public void OnEnable()
    {
        input.FindActionMap("Game").FindAction("Reset").performed += ResetGameInput;
    }
    public void OnDisable()
    {
        input.FindActionMap("Game").FindAction("Reset").performed -= ResetGameInput;
    }
    public void Win()
    {
        winPanel.SetActive(true);
        for (int i = 0; i < players.Length; i++)
        {
            players[i].gameObject.SetActive(false);
        }
    }
    public void LoadLevel(int next)
    {
        SceneManager.LoadScene("Level" + (levelIndex + next));
    }
    public void LoadHomeScreen()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadGoodEnding()
    {
        SceneManager.LoadScene("GoodEnding");
    }
}
