using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public static class Renderer
  {
    public static bool ShowFPS { get; set; } = true;
    private static SpriteFont _debugFont;
    private static GraphicsDevice _graphicsDevice;
    private static GraphicsDeviceManager _graphicsDeviceManager;
    private static Texture2D _debugBackground;
    private static Vector2 _debugBackgroundPosition = new Vector2();
    private static SpriteBatch _spriteBatch;
    internal static Texture2D DebugPositionTexture { get; private set; }

    public static void LoadContent(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager, Texture2D debugPositionTexture = null, SpriteFont debugFont = null)
    {
      _graphicsDevice = graphicsDevice;
      _graphicsDeviceManager = graphicsDeviceManager;

      _debugFont = debugFont;
      _spriteBatch = new SpriteBatch(_graphicsDevice);

      _debugBackground = new Texture2D(graphicsDevice, 170, 20);
      var data = new Color[170 * 20];
      for (int i = 0; i < data.Length; ++i)
        data[i] = new Color(Color.Black, 200);
      _debugBackground.SetData(data);

      DebugPositionTexture = debugPositionTexture;
    }

    public static void Draw(GameTime gameTime)
    {
      _graphicsDevice.Clear(Color.Black);
      Camera.Cameras.ForEach(r => r.Draw(gameTime));

      if (ShowFPS && _debugFont != null)
      {
        _spriteBatch.Begin();
        _spriteBatch.Draw(_debugBackground, _debugBackgroundPosition, Color.White);
        _spriteBatch.DrawString(_debugFont, "Frametime: " + gameTime.ElapsedGameTime.TotalSeconds, new Vector2(1, 1), Color.White, 0, new Vector2(0, 0), 1, new SpriteEffects(), 0);
        _spriteBatch.End();
      }
    }
  }
}