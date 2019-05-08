using System.Diagnostics;
using DIKUArcade.EventBus;
using DIKUArcade.State;
using OpenTK.Graphics.OpenGL;

namespace SpaceTaxi_1.SpaceStates {
    public class StateMachine : IGameEventProcessor<object> {
        public StateMachine() {
            SpaceBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            SpaceBus.GetBus().Subscribe(GameEventType.InputEvent, this);

            ActiveState = MainMenu.GetInstance();
        }

        public IGameState ActiveState { get; private set; }

        
        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
            case GameStateType.MainMenu:
                ActiveState = MainMenu.GetInstance();
                break;
            }
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.GameStateEvent) {
                switch (gameEvent.Message) {
                case "CHANGE_STATE":
                    SwitchState(StateTransformer.TransformStringToState(gameEvent.Parameter1));
                    break;
                }
            } else if (eventType == GameEventType.InputEvent) {
                ActiveState.HandleKeyEvent(gameEvent.Message, gameEvent.Parameter1);
            }
        }
    }
}