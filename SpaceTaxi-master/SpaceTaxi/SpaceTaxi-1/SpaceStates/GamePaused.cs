using System.Drawing;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using Image = DIKUArcade.Graphics.Image;

namespace SpaceTaxi_1.SpaceStates {
    public class GamePaused : IGameState {
        private static GamePaused instance;
        private int activeMenuButton;
        private int inactiveMenuButton;

        private Entity pauseImage;
        private Text[] menuButtons;
        
        public GamePaused() {
            pauseImage = new Entity(
                new StationaryShape(new Vec2F(0.0f,0.0f), 
                    new Vec2F(1.0f,1.0f)), 
                new Image(Path.Combine("Assets", "Images", "SpaceTaxiImage.png")));    
            menuButtons = new[] {
                new Text("Resume", new Vec2F(0.4f, 0.25f), 
                    new Vec2F(0.3f, 0.3f)),
                new Text("Quit", new Vec2F(0.4f, 0.15f), 
                    new Vec2F(0.3f, 0.3f))
            };

            activeMenuButton = 0;
            inactiveMenuButton = 1;
        }

        public static GamePaused GetInstance() {
            return GamePaused.instance ?? (GamePaused.instance = new GamePaused());
        }
        
        public void GameLoop() {
        }

        public void InitializeGameState() {
        }

        public void UpdateGameLogic() {
            menuButtons[activeMenuButton].SetColor(Color.Red);
            menuButtons[inactiveMenuButton].SetColor(Color.White);
        }

        public void RenderState() {
            pauseImage.RenderEntity();
            foreach (var button in menuButtons) {
                button.RenderText();
            }
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            switch (keyValue) {
            case "KEY_UP":
                if (keyAction == "KEY_PRESS") {
                    activeMenuButton = 0;
                    inactiveMenuButton = 1;
                }

                break;
            case "KEY_DOWN":
                if (keyAction == "KEY_PRESS") {
                    activeMenuButton = 1;
                    inactiveMenuButton = 0;
                }
                break;
            case "KEY_ENTER":
                switch (activeMenuButton) {
                case 0:
                    SpaceBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors
                        (GameEventType.GameStateEvent, this,
                            "CHANGE_STATE", "GAME_RUNNING", ""));
                    break;
                case 1:
                    if (keyAction == "KEY_PRESS") {
                        SpaceBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.WindowEvent, this, "CLOSE_WINDOW", 
                                "", ""));   
                    }
                    break;
                }
                break;
            }
        }
    }
}