
using UnityEngine;

public class MapController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform player;

    PlayerMovment pm;

    [Header("Terrain")]
    [SerializeField] private GameObject[] terrainChunks;
    [SerializeField] private float chunkSize = 20f;
    [SerializeField] private float checkerRadius = 0.2f;
    [SerializeField] private LayerMask terrainMask;

    private Vector3 noTerrainPosition;
    private Vector3 lastChunkPosition;

    void Start()
    {
        if (player == null)
        {
            pm = FindObjectOfType<PlayerMovment>();
            if (pm != null)
                player = pm.transform;
        }
        else
        {
            pm = player.GetComponent<PlayerMovment>();
        }

        if (pm == null)
        {
            Debug.LogError("No se ha encontrado PlayerMovment.");
            enabled = false;
            return;
        }

        lastChunkPosition = player.position;
    }

    void Update()
    {
        ChunkChecker();
    }

    void ChunkChecker()
    {
        Vector2 direction = pm.moveDir;

        // No generar chunks si el jugador está quieto.
        if (direction == Vector2.zero)
            return;

        Vector3 checkPosition = player.position;

        // Determinar la dirección principal del movimiento.
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            checkPosition += direction.x > 0
                ? Vector3.right * chunkSize
                : Vector3.left * chunkSize;
        }
        else
        {
            checkPosition += direction.y > 0
                ? Vector3.up * chunkSize
                : Vector3.down * chunkSize;
        }

        // Evitar generar el mismo chunk repetidamente.
        if (Vector3.Distance(checkPosition, lastChunkPosition) < chunkSize * 0.5f)
            return;

        // Comprobar si ya existe terreno en esa posición.
        Collider2D terrain = Physics2D.OverlapCircle(
            checkPosition,
            checkerRadius,
            terrainMask
        );

        if (terrain == null)
        {
            noTerrainPosition = checkPosition;
            SpawnChunk();
            lastChunkPosition = checkPosition;
        }
    }

    void SpawnChunk()
    {
        if (terrainChunks == null || terrainChunks.Length == 0)
        {
            Debug.LogWarning("No hay chunks asignados en terrainChunks.");
            return;
        }

        int rand = Random.Range(0, terrainChunks.Length);

        Instantiate(
            terrainChunks[rand],
            noTerrainPosition,
            Quaternion.identity
        );
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(noTerrainPosition, checkerRadius);
    }
}

