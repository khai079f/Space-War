using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TileChecker : GameMonoBehaviour
{
    [SerializeField] protected List<TileChecker> surroundingTiles;
    public List<TileChecker> SurroundingTiles => surroundingTiles;
    [SerializeField] protected Collider2D tileCollider; // Collider của tile
    [SerializeField] protected  Renderer tileRenderer;
    [SerializeField] protected Vector2 tileSize; // Kích thước của mỗi tile
    public Vector2 TileSize => tileSize;
    [SerializeField] protected List<TileChecker> aroundTile;
     public List<TileChecker> AroundTile => aroundTile;
    [SerializeField] protected bool isVisibleCamera = false;
    private Vector3 lastCameraPosition;

    protected override void Start()
    {
        base.Start();
        lastCameraPosition = Camera.main.transform.position;
    }
    private void FixedUpdate()
    {
        if (Vector3.Distance(Camera.main.transform.position, lastCameraPosition) > 3f) // Threshold cho camera di chuyển
        {
            CheckIsVisibleCamera();
            lastCameraPosition = Camera.main.transform.position;
        }
    }
    protected override void LoadComponents()
    {
        base.LoadComponents();

        this.LoadTileCollider();
        this.LoadRenderer();
        this.LoadTileSize();
        this.LoadSurroundingTiles();
        this.UpdateTileSizeForCollider();
    }
    public virtual void UpdateTileSizeForCollider()
    {
        if (this.tileCollider == null) return;
        // Ép kiểu sang BoxCollider2D
        BoxCollider2D boxCollider = this.tileCollider as BoxCollider2D;
        if (boxCollider != null)
        {
            boxCollider.size = tileSize;
           // Debug.Log($"{transform.name}: Updated BoxCollider2D size to {tileSize}", gameObject);
        }
        else
        {
            Debug.LogWarning($"{transform.name}: tileCollider is not a BoxCollider2D", gameObject);
        }
    }
    protected virtual void LoadTileCollider()
    {
        if (tileCollider != null) return;

        tileCollider = GetComponent<Collider2D>();
        if (tileCollider == null)
        {
            Debug.LogError($"{gameObject.name} -> TileCollider is not assigned or missing!");
        }
    }
    protected virtual void LoadRenderer()
    {
        if (tileRenderer != null) return;

        tileRenderer = GetComponent<Renderer>();
        if (tileCollider == null)
        {
            Debug.LogError($"{gameObject.name} -> tileCollider is not assigned or missing!");
        }
    }
    public virtual void LoadSurroundingTiles()
    {
        Vector2 currentPosition = transform.position;

        // Tính toán vị trí các tile xung quanh
        surroundingTiles.Clear(); // Đảm bảo danh sách sạch trước khi thêm

        surroundingTiles.Add(FindTileAtPosition(currentPosition + new Vector2(-tileSize.x, 0))); // Left
        surroundingTiles.Add(FindTileAtPosition(currentPosition + new Vector2(0, tileSize.y))); // Top
        surroundingTiles.Add(FindTileAtPosition(currentPosition + new Vector2(tileSize.x, 0))); // Right
        surroundingTiles.Add(FindTileAtPosition(currentPosition + new Vector2(0, -tileSize.y))); // Bottom
    }

    protected virtual TileChecker FindTileAtPosition(Vector2 position)
    {
        Collider2D hit = Physics2D.OverlapPoint(position);
        if (hit != null && hit.transform != this.transform) // Bỏ qua chính nó
        {
            TileChecker tile = hit.transform.GetComponent<TileChecker>();
            return tile;
        }
        return null;
    }
    public virtual void LoadTileSize()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            tileSize = spriteRenderer.bounds.size;
         //   Debug.Log($"{gameObject.name} -> Tile size calculated: {tileSize}");
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} -> SpriteRenderer not found! Using default tile size: {tileSize}");
        }
    }



    protected virtual void CheckIsVisibleCamera()
    {
        if (this.tileRenderer == null || Camera.main == null) return;

        Bounds tileBounds = this.tileRenderer.bounds;

        // Xác định góc của camera
        Vector3 camMin = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        Vector3 camMax = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, Camera.main.nearClipPlane));

        // Kiểm tra nếu tile và camera trùng vùng
        this.isVisibleCamera = tileBounds.min.x < camMax.x && tileBounds.max.x > camMin.x &&
                               tileBounds.min.y < camMax.y && tileBounds.max.y > camMin.y;
    }
    public virtual void SetDefaultAroundTile(List<TileChecker> aroundTile)
    {
        this.aroundTile = aroundTile;
    }
    public virtual void SetPositonTile(float x, float y, float smoothFactor = 1f)
    {
        // Lấy z hiện tại để giữ nguyên độ cao (nếu có)
        float currentZ = this.transform.position.z;

        // Dịch chuyển mượt mà với một factor
        this.transform.position = Vector3.Lerp(this.transform.position, new Vector3(x, y, currentZ), smoothFactor);
    }

    public virtual bool GetIsVisibleCamera()
    {
        return this.isVisibleCamera;
    }
/*    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, (Vector3)tileSize); // Vẽ khung tile trong Scene View

    }*/
}
