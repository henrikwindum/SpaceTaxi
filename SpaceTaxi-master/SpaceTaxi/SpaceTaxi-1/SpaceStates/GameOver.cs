using System.Drawing;
using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using Image = DIKUArcade.Graphics.Image;

namespace SpaceTaxi_1.SpaceStates {
    public class GameOver : IGameState {
        private static GameOver instance;
        private int activeMenuButton;
        private int inactiveMenuButton;
    
        private Entity gameOverImage;
        private Text[] menuButtons;
        
        
        public GameOver() {
            gameOverImage = new Entity(
                new StationaryShape(new Vec2F(0.0f,0.0f),
                    new Vec2F(1.0f,1.0f)), 
                new Image(Path.Combine("Assets","Images","GameOver.png")));
            menuButtons = new [] {
                new Text("Main Menu", new Vec2F(0.4f, 0.25f), 
                    new Vec2F(0.3f, 0.3f)),
                new Text("Quit", new Vec2F(0.4f, 0.15f), 
                    new Vec2F(0.3f, 0.3f))
            };

            activeMenuButton = 0;
            inactiveMenuButton = 1;
        }

        public static GameOver GetInstance() {
            return GameOver.instance ?? (GameOver.instance = new GameOver());
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
            gameOverImage.RenderEntity();
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
                            "CHANGE_STATE", "MAIN_MENU", ""));
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