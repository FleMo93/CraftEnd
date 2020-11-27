using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public partial class Entity : IEnumerable<Entity>
  {
    public static List<Entity> Entities = new List<Entity>();

    public readonly string Name;
    internal List<Entity> Children = new List<Entity>();
    public Vector3 Position = new Vector3(0, 0, 0);
    public Vector2 Scale = new Vector2(1, 1);
    private bool _active = true;
    public bool Active
    {
      get
      {
        return this._active;
      }
      set
      {

      }
    }
    internal List<Component> Components = new List<Component>();

    public Entity(string name = "")
    {
      this.Name = name;
      Entities.Add(this);
    }

    public virtual void Update(GameTime gameTime)
    {
      if (!Active) { return; }
      this.Components.ForEach(component => component.Update(gameTime));
    }

    internal virtual void Draw(GameTime gameTime, Camera camera, SpriteBatch spriteBatch)
    {
      if (!Active) { return; }
      this.Components.ForEach(component => component.Draw(gameTime, camera, spriteBatch));
    }

    public virtual void AddComponent(Component component)
    {
      if (this.Components.Contains(component))
        throw new System.Exception("Component already added");

      component.SetOwnerEntity(this);
      this.Components.Add(component);
    }

    public virtual void Destroy()
    {
      this.Components.ForEach(c => c.Destroy());
    }
  
    public void SetParent(Entity entity)
    {
      entity.Children.Add(entity);
    }

    public IEnumerator<Entity> GetEnumerator()
    {
      throw new System.NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      throw new System.NotImplementedException();
    }
  }
}