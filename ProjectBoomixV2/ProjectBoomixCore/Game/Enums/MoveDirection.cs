using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ProjectBoomixCore.Game {

    public enum MoveDirection {
        Right,
        Left,
        Up,
        Down
    }

    public static class DirectionUtil {

        public static readonly Dictionary<MoveDirection, Vector2> Directions = new Dictionary<MoveDirection, Vector2> {
            {  MoveDirection.Right, new Vector2(1, 0) },
            {  MoveDirection.Left, new Vector2(-1, 0) },
            {  MoveDirection.Up, new Vector2(0, -1) },
            {  MoveDirection.Down, new Vector2(0, 1) }
        };
    }
}