using UnityEngine;

public class StateEnemyManager
{
    // Lưu trữ các trạng thái đã khởi tạo
    public BaseState RetreatState { get; private set; }
    public BaseState MoveState { get; private set; }
    public BaseState BombRetreatState { get; private set; }
    public BaseState BombEnemyMoveState { get; private set; }
    // Bạn có thể thêm các state khác nếu cần

    public StateEnemyManager(NormalEnemyCtrl enemy, StateMachine stateMachine)
    {
        // Khởi tạo các trạng thái dựa trên các tham số truyền vào
        RetreatState = new RetreatState(enemy, stateMachine);
        BombRetreatState = new BombRetreatState(enemy, stateMachine);
        MoveState = new NormalEnemyMoveState(enemy, stateMachine);
        BombEnemyMoveState = new BombEnemyMoveState(enemy, stateMachine);
        // Nếu cần, khởi tạo thêm trạng thái khác
    }
}
