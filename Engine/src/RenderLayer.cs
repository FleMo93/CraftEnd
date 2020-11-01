using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public class RenderLayer
  {
    public float PixelMetersMultiplier { get; } = short.MaxValue;
    private SpriteBatch _spriteBatch;
    private GraphicsDevice _graphicsDevice;
    private GraphicsDeviceManager _graphicsDeviceManager;
    private float _targetHeight = 1;
    private Matrix scaleMatrix;
    private List<Entity> _entities;

    public float TargetHeight
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

    public RenderLayer(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager, float targetHeight)
    {
      _graphicsDevice = graphicsDevice;
      _graphicsDeviceManager = graphicsDeviceManager;
      _spriteBatch = new SpriteBatch(graphicsDevice);
      _entities = new List<Entity>();
      TargetHeight = targetHeight;
    }

    internal void Draw(GameTime gameTime)
    {
      _spriteBatch.Begin(transformMatrix: scaleMatrix, samplerState: SamplerState.PointClamp);
      this._entities.ForEach(e => e.Draw(gameTime, this, _spriteBatch));
      _spriteBatch.End();
    }

    public void AddEntity(Entity entity)
    {
      this._entities.Add(entity);
    }
  }
}