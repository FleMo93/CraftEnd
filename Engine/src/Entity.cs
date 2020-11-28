using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  // public class EntityEnum : IEnumerator<Entity>
  // {

  //   Entity IEnumerator<Entity>.Current
  //   {
  //     get
  //     {
  //       return this.entities[position];
  //     }
  //   }

  //   public object Current
  //   {
  //     get
  //     {
  //       return this.entities[position];
  //     }
  //   }

  //   public Entity[] entities;
  //   private int position = -1;

  //   public EntityEnum(Entity[] entities)
  //   {
  //     this.entities = entities;
  //   }

  //   public bool MoveNext()
  //   {
  //     position++;
  //     return (position < this.entities.Length);
  //   }

  //   public void Reset()
  //   {
  //     this.position = -1;
  //   }

  //   public void Dispose()
  //   {
  //     entities = null;
  //   }
  // }

  public partial class Entity : IEnumerable<Entity>
  {
    public static List<Entity> Entities = new List<Entity>();

    public readonly string Name;
    internal List<Entity> Children = new List<Entity>();
    public Vector3 Position = new Vector3(0, 0, 0);
    public Vector2 Scale = new Vector2(1, 1);
    public Entity Parent = null;
    private bool _active = true;
    public bool Active
    {
      get
      {
        return this.Parent != null ?
          this.Parent.Active && this._active :
          this._active;
      }
      set
      {
        this._active = value;
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

    public void SetParent(Entity parent)
    {
      if (this.Parent == parent)
        return;

      if (this.Parent != null && !this.Parent.Children.Remove(this))
        throw new System.Exception("Could not remove child");

      parent.Children.Add(this);
      this.Parent = parent;
    }

    public IEnumerator<Entity> GetEnumerator()
    {
      return ((IEnumerable<Entity>)Children).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return ((IEnumerable)Children).GetEnumerator();
    }
  }
}