using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public partial class Entity
  {
    internal static List<Entity> Entities = new List<Entity>();

    public Vector2 Position = new Vector2(0, 1);
    public Vector2 Scale = new Vector2(1, 1);
    private List<Component> components = new List<Component>();

    public Entity(params int[] IComponent)
    {
      Entities.Add(this);
    }

    public virtual void Initialize() { }
    public virtual void LoadContent(ContentManager content) { }
    public virtual void Update(GameTime gameTime) { }
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
  }
}