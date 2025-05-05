using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    private BaseState currentState;  // Trạng thái hiện tại của đối tượng
    public BaseState CurrentState => currentState;

    // Thiết lập trạng thái ban đầu
    public void Initialize(BaseState startingState)
    {
        this.currentState = startingState;
        this.currentState.Enter();
        //Debug.Log(currentState);
    }

    // Thay đổi sang trạng thái mới
    public void ChangeState(BaseState newState)
    {
        if (newState == null) return;
        this.currentState.Exit(); // Gọi phương thức Exit của trạng thái hiện tại
        this.currentState = newState;  // Thay đổi trạng thái
        this.currentState.Enter();  // Gọi phương thức Enter của trạng thái mới
       // Debug.Log(this.currentState);
    }
    public void ResetState()
    {
        this.currentState = null;  // Thay đổi trạng thái

    }
    // Gọi phương thức Execute của trạng thái hiện tại (để cập nhật)
    public void ExecuteState()
    {
        if (this.currentState != null)
        {
            this.currentState.Execute();
        }
    }
}
