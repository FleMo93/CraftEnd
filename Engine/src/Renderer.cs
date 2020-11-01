using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public static class Renderer
  {
    private static List<RenderLayer> _renderLayers = new List<RenderLayer>();
    public static bool ShowFPS { get; set; } = true;
    private static SpriteFont _debugFont;
    private static GraphicsDevice _graphicsDevice;
    private static GraphicsDeviceManager _graphicsDeviceManager;
    private static RenderLayer _debugLayer;


    public static void LoadContent(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager, SpriteFont debugFont = null)
    {
      _graphicsDevice = graphicsDevice;
      _graphicsDeviceManager = graphicsDeviceManager;

      _debugFont = debugFont;
      _debugLayer = new RenderLayer(graphicsDevice, graphicsDeviceManager, 1);
    }

    public static void Draw(GameTime gameTime)
    {
      _graphicsDevice.Clear(Color.CornflowerBlue);
      _renderLayers.ForEach(r => r.Draw(gameTime));


      if (ShowFPS && _debugFont != null)
      {
        var _spriteBatch = new SpriteBatch(_graphicsDevice);
        _spriteBatch.Begin();
        _spriteBatch.DrawString(_debugFont, "Frametime: " + gameTime.ElapsedGameTime.TotalSeconds, new Vector2(1, 1), Color.White, 0, new Vector2(0, 0), 1, new SpriteEffects(), 1);
        _spriteBatch.End();
      }
    }

    public static RenderLayer CreateRenderLayer(float targetHeight)
    {
      var renderLayer = new RenderLayer(_graphicsDevice, _graphicsDeviceManager, targetHeight);
      _renderLayers.Add(renderLayer);
      return renderLayer;
    }
  }
}