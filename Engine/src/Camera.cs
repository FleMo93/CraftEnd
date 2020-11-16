using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public class Camera
  {
    internal static List<Camera> Cameras = new List<Camera>();

    internal float PixelMetersMultiplier { get; } = short.MaxValue;
    public Vector2 Position;
    private float _zoom = 0;
    public float Zoom
    {
      get
      {
        return _zoom;
      }
      set
      {
        _zoom = value;
        var ratio = (float)_graphicsDeviceManager.PreferredBackBufferWidth / (float)_graphicsDeviceManager.PreferredBackBufferHeight;
        var scaleY = (float)_graphicsDeviceManager.PreferredBackBufferHeight / PixelMetersMultiplier / value;
        var scaleX = (float)_graphicsDeviceManager.PreferredBackBufferWidth / PixelMetersMultiplier / value / ratio;
        scaleMatrix = Matrix.CreateScale(scaleX, scaleY, 1);
      }
    }
    private GraphicsDeviceManager _graphicsDeviceManager;
    public RenderPivot RenderPivot = RenderPivot.TopLeft;
    private List<Entity> _entities;
    private SpriteBatch spriteBatch;
    private Matrix scaleMatrix { get; set; }
    private Matrix positionMatrix
    {
      get
      {
        var position = new Vector2();
        switch (this.RenderPivot)
        {
          case RenderPivot.TopLeft:
            position = this.Position * -1;
            break;
          case RenderPivot.Center:
            position.X = this.Position.X * -1 + Zoom / 2;
            position.Y = this.Position.Y * -1 + Zoom / 2;
            break;
        }

        return Matrix.CreateTranslation(
            position.X * PixelMetersMultiplier,
            position.Y * PixelMetersMultiplier, 0);
      }
    }
    private Matrix transformMatrix
    {
      get
      {
        return this.positionMatrix * this.scaleMatrix;
      }
    }

    public Camera(GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicsDevice)
    {
      this._graphicsDeviceManager = graphicsDeviceManager;
      this.Position = new Vector2();
      this._entities = new List<Entity>();
      this.Zoom = 1;
      this.spriteBatch = new SpriteBatch(graphicsDevice);
      Camera.Cameras.Add(this);
    }

    internal void Draw(GameTime gameTime)
    {
      spriteBatch.Begin(transformMatrix: transformMatrix, samplerState: SamplerState.PointClamp);

      foreach (var entity in this._entities.OrderBy(e => e.Position.Z).ThenBy(e => e.Position.Y))
        entity.Draw(gameTime, this, spriteBatch);

      spriteBatch.End();
    }

    public Vector2 ScreenToWorldPosition(float x, float y)
    {
      return ScreenToWorldPosition(new Vector2(x, y));
    }

    public Vector2 ScreenToWorldPosition(Vector2 point)
    {
      Matrix invertedMatrix = Matrix.Invert(this.scaleMatrix);
      return Vector2.Transform(point, invertedMatrix);
    }

    public void AddEntity(Entity entity)
    {
      this._entities.Add(entity);
    }
  }
}