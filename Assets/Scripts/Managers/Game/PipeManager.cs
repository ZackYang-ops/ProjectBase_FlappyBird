using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.Events;

public class PipeManager : Singleton<PipeManager>
{
    #region Public properties

    /// <summary>
    /// the waiting time to generate pipes
    /// </summary>
    public float WaitTime = 1.5f;

    #endregion

    #region Private properties

    /// <summary>
    /// all generated pipes
    /// </summary>
    private List<Pipe> pipes = new List<Pipe>();

    /// <summary>
    /// coroutine of pipe generation
    /// </summary>
    private Coroutine pipeGeneration;

    #endregion

    public void Init()
    {
        GameManager.Instance.OnStateChange += OnGameStateChange;
    }

    public void Clear()
    {
        foreach (var pipe in pipes)
        {
            pipe.SelfDestroy();
        }

        pipes.Clear();
    }

    private void OnGameStateChange(GameManager.Game_State state)
    {
        if (state == GameManager.Game_State.GAME_PLAY)
        {
            pipeGeneration = MonoManager.Instance.StartCoroutine(GeneratePipes());
        }
        else if (state == GameManager.Game_State.GAME_OVER)
        {
            //stop generating pipes
            if (pipeGeneration != null)
            {
                MonoManager.Instance.StopCoroutine(pipeGeneration);
            }
            pipeGeneration = null;

            //stop all existing pipe
            foreach (var pipe in pipes)
            {
                pipe.IsOn = false;
            }
        }
    }

    private IEnumerator GeneratePipes()
    {
        while (true)
        {
            PoolManager.Instance.GetObj(ResManager.ResourceType.CommonPrefab, "Pipe", InitPipe);
            yield return new WaitForSeconds(WaitTime);
        }
    }

    private void InitPipe(GameObject obj)
    {
        Pipe current = obj.GetComponent<Pipe>();
        if (!pipes.Contains(current))
        {
            pipes.Add(current);
        }

        current.IsOn = true;
    }
}
