using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public partial class Entity
  {
    public static List<Entity> Entities = new List<Entity>();

    public List<Entity> Children = new List<Entity>();
    public Vector3 Position = new Vector3(0, 0, 0);
    public Vector2 Scale = new Vector2(1, 1);
    internal List<Component> components = new List<Component>();

    public Entity()
    {
      Entities.Add(this);
    }

    public virtual void Initialize() { }
    public virtual void LoadContent(ContentManager content) { }
    public virtual void Update(GameTime gameTime)
    {
      this.components.ForEach(component => component.Update(gameTime));
    }
    internal virtual void Draw(GameTime gameTime, RenderLayer renderLayer, SpriteBatch spriteBatch)
    {
      this.components.ForEach(component => component.Draw(gameTime, renderLayer, spriteBatch));
    }

    public virtual void AddComponent(Component component)
    {
      if (this.components.Contains(component))
        throw new System.Exception("Component already added");

      component.SetOwnerEntity(this);
      this.components.Add(component);
    }

    public virtual void Destroy()
    {
      this.components.ForEach(c => c.Destroy());
    }
  }
}