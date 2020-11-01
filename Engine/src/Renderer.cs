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

    private static SpriteBatch _spriteBatch;
    private static GraphicsDevice _graphicsDevice;
    private static GraphicsDeviceManager _graphicsDeviceManager;
    public static void LoadContent(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager, float targetHeight)
    {
      _graphicsDevice = graphicsDevice;
      _graphicsDeviceManager = graphicsDeviceManager;
      _spriteBatch = new SpriteBatch(graphicsDevice);
      TargetHeight = targetHeight;
    }

    public static void Draw(GameTime gameTime)
    {
      _graphicsDevice.Clear(Color.CornflowerBlue);
      _spriteBatch.Begin(transformMatrix: scaleMatrix);

      Entity.Entities.ForEach(e => e.Draw(gameTime, _spriteBatch));
      _spriteBatch.End();
    }
  }
}