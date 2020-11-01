using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public static class Renderer
  {
    public static float PixelMetersMultiplier { get; } = short.MaxValue;
    private static float _targetHeight = 1;
    private static Matrix scaleMatrix;

    public static float TargetHeight
    {
      get
      {
        return _targetHeight;
      }
      set
      {
        _targetHeight = value;
        var ratio = (float)_graphicsDeviceManager.PreferredBackBufferWidth / (float)_graphicsDeviceManager.PreferredBackBufferHeight;
        var scaleY = (float)_graphicsDeviceManager.PreferredBackBufferHeight / PixelMetersMultiplier / value;
        var scaleX = (float)_graphicsDeviceManager.PreferredBackBufferWidth / PixelMetersMultiplier / value / ratio;
        scaleMatrix = Matrix.CreateScale(scaleX, scaleY, 1);
      }
    }
    public static bool ShowDebug { get; set; } = false;

    private static SpriteBatch _spriteBatch;
    private static GraphicsDevice _graphicsDevice;
    private static GraphicsDeviceManager _graphicsDeviceManager;
    private static SpriteFont _debugFont;

    public static void LoadContent(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager, float targetHeight, SpriteFont debugFont)
    {
      _graphicsDevice = graphicsDevice;
      _graphicsDeviceManager = graphicsDeviceManager;
      _spriteBatch = new SpriteBatch(graphicsDevice);
      TargetHeight = targetHeight;
      _debugFont = debugFont;
      
    }

    public static void Draw(GameTime gameTime)
    {
      _graphicsDevice.Clear(Color.CornflowerBlue);
      _spriteBatch.Begin(transformMatrix: scaleMatrix);

      Entity.Entities.ForEach(e => e.Draw(gameTime, _spriteBatch));

      if (ShowDebug)
        _spriteBatch.DrawString(_debugFont, "Frametime: " + gameTime.ElapsedGameTime.TotalSeconds, new Vector2(1, 1), Color.White, 0, new Vector2(0, 0), 400, new SpriteEffects(), 1);

      _spriteBatch.End();
    }
  }
}