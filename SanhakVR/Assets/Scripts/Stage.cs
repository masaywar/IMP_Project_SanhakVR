using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public ClearUI clearUI;
    private GUIManager guiManager; 

    [SerializeField]
    private string _stageName;
    public string stageName
    {
        get => _stageName;
        set => _stageName = value;
    }

    [SerializeField]
    private int _stageNum;
    public int stageNum 
    {
        get => _stageNum;

    }

    [SerializeField]
    private bool allSolved = false;
    private bool isDone = false;

    [SerializeField]
    private List<IPuzzle> _puzzles = new List<IPuzzle>();
    public List<IPuzzle> puzzles
    {
        get => _puzzles;
    }

    public bool Notify()
    {
        foreach (var puzzle in puzzles)
        {
            if (!puzzle.IsSolved())
                return false;
        }
        return true;
    }

    private void Awake()
    {
        StageController.Instance.AddStage(this);
        StageController.Instance.activeStage = this;
        StageController.Instance.stageState = (StageController.StageState)stageNum;

        guiManager = FindObjectOfType<GUIManager>();
        clearUI = FindObjectOfType<ClearUI>();
    }

    private void Update()
    {
        
        if (!allSolved)
            allSolved = Notify();

        else
        {
            if (!isDone)
            {
                isDone = true;
                if (guiManager != null)
                    guiManager.Clear();

                clearUI.gameObject.SetActive(true);
            }
        }
    }

    public void addPuzzle(IPuzzle puzzle)
    {
        puzzles.Add(puzzle);
    }
}
