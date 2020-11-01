using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public class Component
  {
    internal Entity Entity { get; private set; }

    internal void SetOwnerEntity(Entity entity)
    {
      if (this.Entity != null)
        throw new System.Exception("Component already has an owner entity");

      this.Entity = entity;
    }

    internal virtual void Draw(GameTime gameTime, RenderLayer renderLayer, SpriteBatch spriteBatch) { }
  }
}