using UnityEngine;

public class Ctrl : MonoBehaviour
{
    [HideInInspector]
    public Model model;
    [HideInInspector]
    public View view;
    [HideInInspector]
    public CameraManager cameraManager;
    [HideInInspector]
    public GameManager gameManager;

    private FSMSystem _fsm;
    private void Awake()
    {
        model = GameObject.FindGameObjectWithTag("Model").GetComponent<Model>();
        view = GameObject.FindGameObjectWithTag("View").GetComponent<View>();
        cameraManager = GetComponent<CameraManager>();
        gameManager = GetComponent<GameManager>();
    }
    void Start()
	{
        MakeFSM();
	}

    private void MakeFSM()
	{
        _fsm = new FSMSystem();
        FSMState[] states = GetComponentsInChildren<FSMState>();
		foreach (FSMState state in states)
		{
            _fsm.AddState(state,this);
		}
        MenuState s = GetComponentInChildren<MenuState>();
        _fsm.SetCurrentState(s);
	}
}
