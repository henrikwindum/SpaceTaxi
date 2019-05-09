using System.Collections.Generic;
using System.IO;
using System.Threading;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Physics;
using DIKUArcade.State;
using SpaceTaxi_1.LevelParser;

namespace SpaceTaxi_1.SpaceStates {
    public class GameRunning : IGameState {
        private static GameRunning instance;
        private PaintBoard paintBoard;
        private Entity backGroundImage;
        private Player player;
        private List<Player> playerList = new List<Player>();
        
        private int explosionLength = 500;
        private AnimationContainer explosion;
        private List<Image> explosionStrides;
        
        
        public GameRunning() {
            paintBoard = new PaintBoard("short-n-sweet.txt");
            
            player = new Player();
            player.SetPosition(0.45f,0.6f);
            player.SetExtent(0.1f,0.1f);
            playerList.Add(player);
            
            backGroundImage = new Entity(
                new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
            );

            explosionStrides =
                ImageStride.CreateStrides(8, Path.Combine("Assets", "Images", "Explosion.png"));
            explosion = new AnimationContainer(8);
        }
        
        public static GameRunning GetInstance() {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }

        public static GameRunning NewGetInstance() {
            return GameRunning.instance = new GameRunning();
        }
        
        private void AddExplosion(Vec2F pos, Vec2F extend) {
            explosion.AddAnimation(
                new StationaryShape(pos, extend), explosionLength,
                new ImageStride(explosionLength / 8, explosionStrides));
        }
        
        private void Collision() {
            foreach (Entity entity in paintBoard.Images) {
                switch (CollisionDetection.Aabb(
                    (DynamicShape) player.Entity.Shape, entity.Shape).Collision) {
                case true:
                    AddExplosion(player.Entity.Shape.Position,
                        player.Entity.Shape.Extent);
                    player.Entity.DeleteEntity();    
                    break;
                }

                var newPlayerList = new List<Player>();
                foreach (var player in playerList) {
                    if (!player.Entity.IsDeleted()) {
                        newPlayerList.Add(player);
                    }
                    if (player.Entity.IsDeleted()) {
                        SpaceBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors
                            (GameEventType.GameStateEvent, this,
                                "CHANGE_STATE", "GAME_OVER", ""));
                    }
                }

                playerList = newPlayerList;
            }
        }
        
        public void GameLoop() {
        }

        public void InitializeGameState() {
        }

        public void UpdateGameLogic() {
            player.Movement();
            Collision();
            player.Entity.Shape.Move();
            player.Gravity();
        }

        public void RenderState() {
            backGroundImage.RenderEntity();
            foreach (var player in playerList) {
                player.RenderPlayer();
            }
            paintBoard.Images.RenderEntities();
            explosion.RenderAnimations();
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            switch (keyValue) {
            case "KEY_P":
                SpaceBus.GetBus().RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors
                    (GameEventType.GameStateEvent, this,
                        "CHANGE_STATE", "GAME_PAUSED", ""));
                break;
            }
        }
    }
}