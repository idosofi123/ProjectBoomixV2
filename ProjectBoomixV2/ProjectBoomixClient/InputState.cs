using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ProjectBoomixClient {

    public sealed class InputState : IUpdateable {

        private Dictionary<Keys, long> pressDuration;

        public KeyboardState CurrentKeyboardState { get; private set; }
        public KeyboardState PreviousKeyboardState { get; private set; }
        public MouseState CurrentMouseState { get; private set; }
        public MouseState PreviousMouseState { get; private set; }

        public InputState() {
            this.pressDuration = new Dictionary<Keys, long>();
            CurrentKeyboardState = Keyboard.GetState();
            PreviousKeyboardState = CurrentKeyboardState;
            CurrentMouseState = Mouse.GetState();
            PreviousMouseState = CurrentMouseState;
        }

        public void Update(GameTime gameTime) {

            PreviousKeyboardState = CurrentKeyboardState;
            CurrentKeyboardState = Keyboard.GetState();

            PreviousMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();
            
            foreach (Keys currenlyPressedKey in CurrentKeyboardState.GetPressedKeys()) {
                if (this.pressDuration.ContainsKey(currenlyPressedKey)) {
                    this.pressDuration[currenlyPressedKey] += gameTime.ElapsedGameTime.Ticks;
                } else {
                    this.pressDuration[currenlyPressedKey] = gameTime.ElapsedGameTime.Ticks;
                }
            }

            // Remove keys that were just released -
            foreach (Keys previouslyPressedKey in PreviousKeyboardState.GetPressedKeys()) {
                if (!CurrentKeyboardState.IsKeyDown(previouslyPressedKey)) {
                    this.pressDuration.Remove(previouslyPressedKey);
                }
            }
        }

        public long GetPressDuration(Keys key) {
            long duration = 0;
            this.pressDuration.TryGetValue(key, out duration);
            return duration;
        }
    }
}
