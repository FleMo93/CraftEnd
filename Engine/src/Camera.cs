using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public enum RenderPivot { TopLeft, Center }

  public class Camera : Entity
  {
    private GraphicsDeviceManager graphicsDeviceManager;
    private Vector2 renderPosition;
    public Vector2 RenderPosition { get { return this.renderPosition; } }
    public RenderPivot RenderPivot = RenderPivot.TopLeft;

    public Camera(GraphicsDeviceManager graphicsDeviceManager)
    {
      this.graphicsDeviceManager = graphicsDeviceManager;
      this.renderPosition = new Vector2();
    }

    internal override void Draw(GameTime gameTime, RenderLayer renderLayer, SpriteBatch spriteBatch)
    {
      switch (this.RenderPivot)
      {
        case RenderPivot.TopLeft:
          this.renderPosition.X = this.Position.X;
          this.renderPosition.Y = this.Position.Y;
          break;
        case RenderPivot.Center:
          this.renderPosition.X = this.Position.X * -1 + renderLayer.TargetHeight / 2;
          this.renderPosition.Y = this.Position.Y * -1 + renderLayer.TargetHeight / 2;
          break;
      }
    }
  }
}