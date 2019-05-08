using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.State;
using SpaceTaxi_1.LevelParser;

namespace SpaceTaxi_1.SpaceStates {
    public class GameRunning : IGameState {
        private static GameRunning instance;
        private PaintBoard paintBoard;
        private Entity backGroundImage;
        private Player player;

        public GameRunning() {
            paintBoard = new PaintBoard("short-n-sweet.txt");
            
            player = new Player();
            player.SetPosition(0.45f,0.6f);
            player.SetExtent(0.1f,0.1f);
            
            backGroundImage = new Entity(
                new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
            );
        }
        
        public static GameRunning GetInstance() {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }
        
        public void GameLoop() {
        }

        public void InitializeGameState() {
        }

        public void UpdateGameLogic() {
        }

        public void RenderState() {
            backGroundImage.RenderEntity();
            player.RenderPlayer();
            paintBoard.Images.RenderEntities();
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
        }
    }
}