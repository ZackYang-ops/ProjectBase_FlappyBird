using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public enum Game_State
        {
            None = 0,
            GAME_PREP = 1,
            GAME_PLAY = 2,
            GAME_OVER = 3
        }

        public UnityAction<Game_State> OnStateChange = null;

        private Game_State state;
        protected override void OnStart()
        {
            base.OnStart();

            state = Game_State.GAME_PREP;

            UIManager.Instance.ShowPanel<StartPanel>("StartPanel", UILayer.System);

            //Init managers
            PipeManager.Instance.Init();
        }

        public void StartGame()
        {
            PipeManager.Instance.Clear();

            UIManager.Instance.HidePanel("StartPanel");

            if (state != Game_State.GAME_PLAY && OnStateChange != null)
            {
                OnStateChange.Invoke(Game_State.GAME_PLAY);
                state = Game_State.GAME_PLAY;
            }
        }

        public void EndGame()
        {
            if (state != Game_State.GAME_OVER && OnStateChange != null)
            {
                OnStateChange.Invoke(Game_State.GAME_OVER);
                state = Game_State.GAME_OVER;

                UIManager.Instance.ShowPanel<StartPanel>("StartPanel", UILayer.System);
            }
        }
    }
}
