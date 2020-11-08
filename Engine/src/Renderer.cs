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
    private static Texture2D _debugBackground;
    private static Vector2 _debugBackgroundPosition = new Vector2();
    private static SpriteBatch _spriteBatch;

    internal static Texture2D DebugPositionTexture;

    public static void LoadContent(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager, SpriteFont debugFont = null)
    {
      _graphicsDevice = graphicsDevice;
      _graphicsDeviceManager = graphicsDeviceManager;

      _debugFont = debugFont;
      _debugLayer = new RenderLayer(graphicsDevice, graphicsDeviceManager, 1);
      _spriteBatch = new SpriteBatch(_graphicsDevice);

      _debugBackground = new Texture2D(graphicsDevice, 170, 20);
      var data = new Color[170 * 20];
      for (int i = 0; i < data.Length; ++i)
        data[i] = new Color(Color.Black, 200);
      _debugBackground.SetData(data);

      DebugPositionTexture = new Texture2D(graphicsDevice, 5, 5);
      var color = new Color[5*5];
      color[2] = color[7] = color[10] = color[11] = color[12] = color[13] = color[14] = color[17] = color[22] = Color.Red;
      DebugPositionTexture.SetData(color);
    }

    public static void Draw(GameTime gameTime)
    {
      _graphicsDevice.Clear(Color.Black);
      _renderLayers.ForEach(r => r.Draw(gameTime));

      if (ShowFPS && _debugFont != null)
      {
        _spriteBatch.Begin();
        _spriteBatch.Draw(_debugBackground, _debugBackgroundPosition, Color.White);
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