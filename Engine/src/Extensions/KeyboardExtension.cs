using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace CraftEnd.Engine
{
  public static class KeyboardExtension
  {
    private static Dictionary<Keys, bool> lastKeyStates;
    static KeyboardExtension()
    {
      lastKeyStates = new Dictionary<Keys, bool>();
      foreach(Keys key in Enum.GetValues(typeof(Keys)))
      {
        lastKeyStates.Add(key, false);
      }
    }

    internal static void Update()
    {
      var state = Keyboard.GetState();
      foreach(Keys key in Enum.GetValues(typeof(Keys)))
      {
        lastKeyStates[key] = state.IsKeyDown(key);
      }
    }

    public static bool IsKeyPressed(this KeyboardState keyboard, Keys key)
    {
      return keyboard.IsKeyDown(key) && !lastKeyStates[key];
    }
  }
}