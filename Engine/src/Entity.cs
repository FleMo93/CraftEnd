using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public partial class Entity
  {
    static List<Entity> entities = new List<Entity>();

    public Vector2 Position = new Vector2(0, 0);
    private List<Component> components = new List<Component>();

    public Entity(params int[] IComponent)
    {
      entities.Add(this);
    }

    public virtual void Initialize() { }
    public virtual void LoadContent(ContentManager content) { }
    public virtual void Update(GameTime gameTime) { }
    public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
      this.components.ForEach(component => component.Draw(gameTime, spriteBatch));
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