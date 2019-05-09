using System;
using System.IO;
using System.Linq.Expressions;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace SpaceTaxi_1 {
    public class Player : IGameEventProcessor<object> {
        private readonly Image taxiBoosterOffImageLeft;
        private readonly Image taxiBoosterOffImageRight;
        private readonly DynamicShape shape;
        private Orientation taxiOrientation;

        private Boolean MoveUp = false;
        private Boolean MoveLeft = false;
        private Boolean MoveRight = false;
        
        private Vec2F stopMovement = new Vec2F(0.0f, 0.0f);
        private Vec2F moveUp = new Vec2F(0.0f,0.00005f);
        private Vec2F moveLeft = new Vec2F(0.00005f,0.0f);
        private Vec2F moveRight = new Vec2F(-0.00005f,0.0f);
        

        public Player() {
            shape = new DynamicShape(new Vec2F(), new Vec2F());
            taxiBoosterOffImageLeft =
                new Image(Path.Combine("Assets", "Images", "Taxi_Thrust_None.png"));
            taxiBoosterOffImageRight =
                new Image(Path.Combine("Assets", "Images", "Taxi_Thrust_None_Right.png"));

            Entity = new Entity(shape, taxiBoosterOffImageLeft);
            SpaceBus.GetBus().Subscribe(GameEventType.PlayerEvent,this);
        }

        public Entity Entity { get; }

        public void SetPosition(float x, float y) {
            shape.Position.X = x;
            shape.Position.Y = y;
        }

        public void SetExtent(float width, float height) {
            shape.Extent.X = width;
            shape.Extent.Y = height;
        }

        public void RenderPlayer() {
            //TODO: Next version needs animation. Skipped for clarity.
            Entity.Image = taxiOrientation == Orientation.Left
                ? taxiBoosterOffImageLeft
                : taxiBoosterOffImageRight;
            Entity.RenderEntity();
        }

        public void Gravity() {
            shape.Direction.Y -= 0.00002f;
        }
        
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.PlayerEvent) {
                switch (gameEvent.Message) {
                case "BOOSTER_UPWARDS":
                    MovementUp(shape.Direction);
                    break;
                case "BOOSTER_TO_LEFT":
                    MovementRight(shape.Direction);
                    break;
                case "BOOSTER_TO_RIGHT":
                    MovementLeft(shape.Direction);
                    break;
                case "STOP_ACCELERATE_LEFT":
                case "STOP_ACCELERATE_RIGHT":
                case "STOP_ACCELERATE_UP":
                    StopMovement(shape.Direction);
                    break;
                }   
            }
        }

        private void MovementUp(Vec2F vec) {
            MoveUp = true;
        }
        
        private void MovementLeft(Vec2F vec) {
            MoveLeft = true;
        }

        private void MovementRight(Vec2F vec) {
            MoveRight = true;
        }

        private void StopMovement(Vec2F vec) {
            MoveUp = false;
            MoveRight = false;
            MoveLeft = false;
        }
        
        public void Movement() {
            if (MoveUp) {
                shape.Direction += moveUp;
            }
            if (MoveLeft) {
                shape.Direction += moveLeft;
            }
            if (MoveRight) {
                shape.Direction += moveRight;
            }
        }
    }
}