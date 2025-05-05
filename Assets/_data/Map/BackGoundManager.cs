using UnityEngine;
using System.Collections.Generic;
public class BackGoundManager : GameMonoBehaviour
{
    [SerializeField] private Vector2 tileSize; // Kích thước của mỗi tile (ví dụ: (13.7, 13.7))
    [SerializeField] private List<TileChecker> backgroundTiles; // Danh sách các tile con
    [SerializeField] private int row=4; // Danh sách các tile con
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadBackgroundTiles();
        this.MemorizeAroundTile();
        this.SetPositonTile();
        this.UpdateForTileChecker();
    }
    private void FixedUpdate()
    {
        this.EndlessMap();
    }
    protected virtual void UpdateForTileChecker()
    {
        if (this.backgroundTiles == null || this.backgroundTiles.Count <= 0) return;
        foreach(TileChecker tileChecker in backgroundTiles)
        {
            tileChecker.UpdateTileSizeForCollider();
        }
    }
    protected virtual void LoadBackgroundTiles()
    {
        // Nếu mảng đã được gán sẵn trong Inspector, không cần thực hiện lại
        if (this.backgroundTiles != null && this.backgroundTiles.Count > 0) this.backgroundTiles.Clear();
        if (this.backgroundTiles != null && this.backgroundTiles.Count > 0) return;
            // Lấy tất cả các con của BackGroundManager và lưu vào mảng
            int childCount = transform.childCount;
        TileChecker[] tileCheckers = new TileChecker[childCount];

        for (int i = 0; i < childCount; i++)
        {
            tileCheckers[i] = transform.GetChild(i).GetComponent<TileChecker>();
        }
        this.backgroundTiles.AddRange(tileCheckers);
     //   Debug.Log($"{transform.name} loaded {childCount} background tiles.", gameObject);
    }

    protected virtual void MemorizeAroundTile()
    {
        // Đảm bảo backgroundTiles có đủ 16 tile (4x4)
        int gridSize = 4;
        if (backgroundTiles.Count != gridSize * gridSize)
        {
            Debug.LogError("Background tiles are not properly set for a 4x4 grid.");
            return;
        }

        // Duyệt qua tất cả các tile và gán các tile xung quanh theo thứ tự (left, top, right, bottom)
        for (int i = 0; i < backgroundTiles.Count; i++)
        {
            List<TileChecker> surroundingTiles = new List<TileChecker>(new TileChecker[4]); // Tạo một list với 4 phần tử null
            int row = i / gridSize; // Tính chỉ số hàng
            int col = i % gridSize; // Tính chỉ số cột

            // Left: Kiểm tra xem tile có ở cột đầu tiên không
            if (col > 0) surroundingTiles[0] = backgroundTiles[i - 1]; // leftTile
            else surroundingTiles[0] = backgroundTiles[(row * gridSize) + (gridSize - 1)]; // leftTile từ cột cuối cùng

            // Top: Kiểm tra xem tile có ở hàng đầu tiên không
            if (row > 0) surroundingTiles[1] = backgroundTiles[i - gridSize]; // topTile
            else surroundingTiles[1] = backgroundTiles[(gridSize * (gridSize - 1)) + col]; // topTile từ hàng cuối cùng

            // Right: Kiểm tra xem tile có ở cột cuối không
            if (col < gridSize - 1) surroundingTiles[2] = backgroundTiles[i + 1]; // rightTile
            else surroundingTiles[2] = backgroundTiles[i - (gridSize - 1)]; // rightTile từ cột đầu tiên

            // Bottom: Kiểm tra xem tile có ở hàng cuối không
            if (row < gridSize - 1) surroundingTiles[3] = backgroundTiles[i + gridSize]; // botTile
            else surroundingTiles[3] = backgroundTiles[col]; // botTile từ hàng đầu tiên

            // Gán các tile xung quanh cho tile hiện tại
            backgroundTiles[i].SetDefaultAroundTile(surroundingTiles);
            backgroundTiles[i].LoadSurroundingTiles();
        }
    }
    protected virtual void SetPositonTile()
    {
        // Đảm bảo rằng bạn đã có danh sách các tile và tileSize đã được thiết lập
        if (backgroundTiles == null || backgroundTiles.Count == 0)
        {
            Debug.LogError("Tiles or tile size is not set.");
            return;
        }

        // Đặt vị trí cho tile đầu tiên ở vị trí khởi đầu (-20, 10, 0)
        this.backgroundTiles[0].LoadTileSize();
        float xPosStart = -backgroundTiles[0].TileSize.x * 2; // Vị trí X của tile đầu tiên (-20)
        float yPosStart = backgroundTiles[0].TileSize.y;      // Vị trí Y của tất cả các tile (10)

        // Thiết lập vị trí cho tile đầu tiên
        backgroundTiles[0].SetPositonTile(xPosStart, yPosStart);

        // Duyệt qua các tile còn lại và đặt vị trí theo lưới
        for (int i = 1; i < backgroundTiles.Count; i++)
        {
            this.backgroundTiles[i].LoadTileSize();
            int row = i / this.row;  // Chỉ số hàng (Giả sử mỗi hàng có 3 tile)
            int col = i % this.row;  // Chỉ số cột

            // Tính toán vị trí của tile dựa trên hàng và cột
            float xPos = col * backgroundTiles[i].TileSize.x - backgroundTiles[i].TileSize.x * 2; // Dịch chuyển ban đầu
            float yPos = yPosStart - row * backgroundTiles[i].TileSize.y;  // Dịch chuyển theo chiều dọc

            // Thiết lập vị trí mới cho tile
            this.backgroundTiles[i].SetPositonTile(xPos, yPos);


        }
    }
    protected virtual void EndlessMap()
    {
        foreach (TileChecker backgroundTile in backgroundTiles)
        {
            // Kiểm tra xem tile có đang ở trong vùng nhìn thấy của camera không
            if (backgroundTile.GetIsVisibleCamera())
            {
                // Kiểm tra các tile xung quanh
                List<TileChecker> surroundingTiles = backgroundTile.SurroundingTiles;

                // Kiểm tra các tile thiếu trong surroundingTiles và di chuyển các tile từ aroundTile vào
                if (surroundingTiles[0] == null) // Left
                {
                    TileChecker leftTile = backgroundTile.AroundTile[0];
                    if (leftTile != null)
                    {
                        Vector3 newPosition = backgroundTile.transform.position + new Vector3(-backgroundTile.TileSize.x, 0, 0);
                        leftTile.SetPositonTile(newPosition.x, newPosition.y); // Di chuyển tile vào đúng vị trí
                        surroundingTiles[0] = leftTile; // Cập nhật lại surroundingTile với tile mới
                    }
                }

                if (surroundingTiles[1] == null) // Top
                {
                    TileChecker topTile = backgroundTile.AroundTile[1];
                    if (topTile != null)
                    {
                        Vector3 newPosition = backgroundTile.transform.position + new Vector3(0, backgroundTile.TileSize.y, 0);
                        topTile.SetPositonTile(newPosition.x, newPosition.y); // Di chuyển tile vào đúng vị trí
                        surroundingTiles[1] = topTile; // Cập nhật lại surroundingTile với tile mới
                    }
                }

                if (surroundingTiles[2] == null) // Right
                {
                    TileChecker rightTile = backgroundTile.AroundTile[2];
                    if (rightTile != null)
                    {
                        Vector3 newPosition = backgroundTile.transform.position + new Vector3(backgroundTile.TileSize.x, 0, 0);
                        rightTile.SetPositonTile(newPosition.x, newPosition.y); // Di chuyển tile vào đúng vị trí
                        surroundingTiles[2] = rightTile; // Cập nhật lại surroundingTile với tile mới
                    }
                }

                if (surroundingTiles[3] == null) // Bottom
                {
                    TileChecker bottomTile = backgroundTile.AroundTile[3];
                    if (bottomTile != null)
                    {
                        Vector3 newPosition = backgroundTile.transform.position + new Vector3(0, -backgroundTile.TileSize.y, 0);
                        bottomTile.SetPositonTile(newPosition.x, newPosition.y); // Di chuyển tile vào đúng vị trí
                        surroundingTiles[3] = bottomTile; // Cập nhật lại surroundingTile với tile mới
                    }
                }

                // Cập nhật lại các tile xung quanh sau khi di chuyển
                backgroundTile.LoadSurroundingTiles();
            }
        }
    }





}
