using System.Collections.Generic;
using UnityEngine;
public enum Transition
{
    NullTransition = 0, // ϵͳ�в�����ת��
    // ��Ҫ����ʵ��Ӧ�ü�����չ
    StartButtonClick = 1,
    PauseButtonClick = 2,
}
// ״̬ID
public enum StateID
{
    NullStateID = 0,    // ϵͳ�в�����״̬
    Play = 1,
    Menu = 2,
    Pause = 3,
    GameOver = 4
}
// ת����Ϣ�ࣺ
public class TransInfo
{
    public string comment;
}
/// <summary>
/// ״̬���е�״̬�ࣺ���ֵ����ʽ����״̬��ת��
/// 1. Reason() ���������ĸ�ת��
/// 2. Act() ����NPC�ڵ�ǰ״̬�µ���Ϊ
/// </summary>
public abstract class FSMState : MonoBehaviour
{
    protected Ctrl ctrl;
    public Ctrl CTRL { set { ctrl = value; } }

    protected FSMSystem fsm;
    public FSMSystem FSM { set { fsm = value; } }
    protected Dictionary<Transition, StateID> map = new Dictionary<Transition, StateID>();  // ״̬ת��ӳ��
    protected StateID stateID;                     // ״̬ID
    public StateID ID { get { return stateID; } }  // ��ȡ��ǰ״̬ID
    /// ���ת��
    public void AddTransition(Transition trans, StateID id)
    {
        // ��ֵ����
        if (trans == Transition.NullTransition)
        {
            Debug.LogError("FSMState ERROR: ������ӿ�ת��");
            return;
        }
        if (id == StateID.NullStateID)
        {
            Debug.LogError("FSMState ERROR: ������ӿ�״̬");
            return;
        }
        // ����Ƿ��Ѿ��и�ת��
        if (map.ContainsKey(trans))
        {
            Debug.LogError("FSMState ERROR: ״̬ " + stateID.ToString() + " �Ѿ�����ת�� " + trans.ToString() + "���������һ��״̬");
            return;
        }
        map.Add(trans, id);
    }
    /// ɾ��״̬ת��
    public void DeleteTransition(Transition trans)
    {
        // ��ֵ����
        if (trans == Transition.NullTransition)
        {
            Debug.LogError("FSMState ERROR: ����ɾ����ת��");
            return;
        }
        // �����Ƿ�����Ե�ת��
        if (map.ContainsKey(trans))
        {
            map.Remove(trans);
            return;
        }
        Debug.LogError("FSMState ERROR: ת�� " + trans.ToString() + " - ״̬ " + stateID.ToString() + " ������");
    }
    /// ��ȡ��һ��״̬
    public StateID GetOutputState(Transition trans)
    {
        // �������ת�������ض�Ӧ״̬
        if (map.ContainsKey(trans))
        {
            return map[trans];
        }
        return StateID.NullStateID;
    }
    /// ����״̬֮ǰִ��
    public virtual void DoBeforeEntering() { }
    /// �뿪״̬֮ǰִ��
    public virtual void DoBeforeLeaving() { }
    /// ״̬ת������
    public virtual void Reason() { }
    /// ������Ϊ
    public virtual void Act() { }
}
/// <summary>
/// ״̬���ࣺ����״̬�б�
/// 1. ɾ��״̬
/// 2. �ı䵱ǰ״̬
/// </summary>
public class FSMSystem
{
    private List<FSMState> states;  // ״̬�б�
    // ��״̬���иı䵱ǰ״̬��Ψһ;����ͨ��ת������ǰ״̬����ֱ�Ӹı�
    private StateID currentStateID;
    public StateID CurrentStateID { get { return currentStateID; } }
    private FSMState currentState;
    public FSMState CurrentState { get { return currentState; } }
    
    //���õ�ǰ״̬
    public void SetCurrentState(FSMState s)
	{
        currentState = s;
        currentStateID = s.ID;
        s.DoBeforeEntering();
	}

    public FSMSystem()
    {
        states = new List<FSMState>();
    }
    /// ���״̬
    public void AddState(FSMState s,Ctrl c)
    {
        // ��ֵ����
        if (s == null)
        {
            Debug.LogError("FSM ERROR: ������ӿ�״̬");
        }
        s.FSM = this;
        s.CTRL = c;
        // �������״̬Ϊ��ʼ״̬
        if (states.Count == 0)
        {
            states.Add(s);
            currentState = s;
            currentStateID = s.ID;
            return;
        }
        // ����״̬�б��������ڸ�״̬�������
        foreach (FSMState state in states)
        {
            if (state.ID == s.ID)
            {
                Debug.LogError("FSM ERROR: �޷����״̬ " + s.ID.ToString() + " ��Ϊ��״̬�Ѵ���");
                return;
            }
        }
        states.Add(s);
    }
    /// ɾ��״̬
    public void DeleteState(StateID id)
    {
        // ��ֵ����
        if (id == StateID.NullStateID)
        {
            Debug.LogError("FSM ERROR: ״̬ID ����Ϊ��ID");
            return;
        }
        // ������ɾ��״̬
        foreach (FSMState state in states)
        {
            if (state.ID == id)
            {
                states.Remove(state);
                return;
            }
        }
        Debug.LogError("FSM ERROR: �޷�ɾ��״̬ " + id.ToString() + ". ״̬�б��в�����");
    }
    /// ִ��ת��
    public void PerformTransition(Transition trans)
    {
        // ��ֵ����
        if (trans == Transition.NullTransition)
        {
            Debug.LogError("FSM ERROR: ת������Ϊ��");
            return;
        }
        // ��ȡ��ǰ״̬ID
        StateID id = currentState.GetOutputState(trans);
        if (id == StateID.NullStateID)
        {
            Debug.LogError("FSM ERROR: ״̬ " + currentStateID.ToString() + " ������Ŀ��״̬ " +
                           " - ת���� " + trans.ToString());
            return;
        }
        // ���µ�ǰ״̬ID �� ��ǰ״̬        
        currentStateID = id;
        foreach (FSMState state in states)
        {
            if (state.ID == currentStateID)
            {
                // ִ�е�ǰ״̬����
                currentState.DoBeforeLeaving();
                currentState = state;
                // ִ�е�ǰ״̬ǰ����
                currentState.DoBeforeEntering();
                break;
            }
        }
    }
}
