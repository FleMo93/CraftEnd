using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public class Component
  {
    internal Entity Entity { get; private set; }
    public bool Active { get; set; } = true;

    internal void SetOwnerEntity(Entity entity)
    {
      if (this.Entity != null)
        throw new System.Exception("Component already has an owner entity");

      this.Entity = entity;
      this.OwnerEntitySet(entity);
    }

    internal virtual void OwnerEntitySet(Entity owner) { }
    internal virtual void Draw(GameTime gameTime, Camera camera, SpriteBatch spriteBatch) { }
    public virtual void Update(GameTime gameTime) { }
    internal virtual void Destroy() { }
  }
}