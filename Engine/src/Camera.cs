using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public class Camera
  {
    internal static List<Camera> Cameras = new List<Camera>();
    public Vector2 Position;
    public float Zoom = 1;
    private GraphicsDeviceManager _graphicsDeviceManager;
    public RenderPivot RenderPivot = RenderPivot.TopLeft;
    private List<Entity> _entities;
    private SpriteBatch spriteBatch;
    private Matrix transformMatrix
    {
      get
      {
        var matrix = Matrix.CreateTranslation(-new Vector3(this.Position.X, this.Position.Y, 0)) *
          // Matrix.CreateRotationZ(0) *
          Matrix.CreateScale(this.Zoom, this.Zoom, 1);

        switch (this.RenderPivot)
        {
          case RenderPivot.Center:
            matrix = matrix * Matrix.CreateTranslation(
              _graphicsDeviceManager.PreferredBackBufferWidth / 2,
              _graphicsDeviceManager.PreferredBackBufferHeight / 2, 1);
            break;
          case RenderPivot.TopLeft:
            break;
          case RenderPivot.BottomCenter:
          default:
            throw new System.NotImplementedException();
        }

        return matrix;
      }
    }

    public Camera(GraphicsDeviceManager graphicsDeviceManager, GraphicsDevice graphicsDevice)
    {
      this._graphicsDeviceManager = graphicsDeviceManager;
      this.Position = new Vector2();
      this._entities = new List<Entity>();
      this.spriteBatch = new SpriteBatch(graphicsDevice);
      Camera.Cameras.Add(this);
    }

    internal void Draw(GameTime gameTime)
    {
      spriteBatch.Begin(
        transformMatrix: this.transformMatrix,
        samplerState: SamplerState.PointClamp,
        sortMode: SpriteSortMode.Deferred);

      foreach (var entity in this._entities.OrderBy(e => e.Position.Z).ThenBy(e => e.Position.Y))
        entity.Draw(gameTime, this, spriteBatch);

      spriteBatch.End();
    }

    public Vector2 ScreenToWorldPosition(float x, float y)
    {
      return ScreenToWorldPosition(new Vector2(x, y));
    }

    public Vector2 ScreenToWorldPosition(Point point)
    {
      return ScreenToWorldPosition(new Vector2(point.X, point.Y));
    }

    public Vector2 ScreenToWorldPosition(Vector2 point)
    {
      Matrix invertedMatrix = Matrix.Invert(this.transformMatrix);
      return Vector2.Transform(point, invertedMatrix);
    }

    public void AddEntity(Entity entity)
    {
      this._entities.Add(entity);
    }
  }
}