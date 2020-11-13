using System.Linq;
using Microsoft.Xna.Framework;

namespace CraftEnd.Engine.Colission
{
  public class Rigidbody : Component
  {
    private Collider collider;
    public Vector2 Velocity;
    public Vector3 OldEntityPosition { get; private set; }

    public Rigidbody(Collider collider)
    {
      this.collider = collider;
      this.collider.Rigidbody = this;
    }

    internal override void OwnerEntitySet(Entity owner)
    {
      if (owner.Components.Find(c => c.GetType() == typeof(Rigidbody) && c != this) != null)
        throw new System.Exception("Entity can only contains one rigidbody");

      this.OldEntityPosition = this.Entity.Position;
    }

    internal void Update(GameTime gameTime, BoxCollider thisBoxCollider, BoxCollider hit)
    {
      var l = this.Entity.Position.X + thisBoxCollider.Position.X;
      var r = l + thisBoxCollider.Size.X;
      var t = this.Entity.Position.Y + thisBoxCollider.Position.Y;
      var b = t + thisBoxCollider.Size.Y;

      var ol = this.OldEntityPosition.X + thisBoxCollider.Position.X;
      var or = ol + thisBoxCollider.Size.X;
      var ot = this.OldEntityPosition.Y + thisBoxCollider.Position.Y;
      var ob = ot + thisBoxCollider.Size.Y;

      var hitL = hit.Position.X + hit.Entity.Position.X;
      var hitR = hitL + hit.Size.X;
      var hitT = hit.Position.Y + hit.Entity.Position.Y;
      var hitB = hitT + hit.Size.Y;

      var oHitL = hit.Rigidbody != null ? hit.Position.X + hit.Rigidbody.OldEntityPosition.X : hitL;
      var oHitR = oHitL + hit.Size.X;
      var oHitT = hit.Rigidbody != null ? hit.Position.Y + hit.Rigidbody.OldEntityPosition.Y : hitT;
      var oHitB = oHitT + hit.Size.Y;

      if (b >= hitT && ob < oHitT)
      {
        this.Entity.Position.Y = hitT - thisBoxCollider.Position.Y - thisBoxCollider.Size.Y - 0.001f;
        this.Velocity.Y = hit.Rigidbody != null ? hit.Rigidbody.Velocity.Y : 0;
      }
      else if (t <= hitB && ot > oHitB)
      {
        this.Entity.Position.Y = hitB + thisBoxCollider.Size.Y + 0.001f;
        this.Velocity.Y = hit.Rigidbody != null ? hit.Rigidbody.Velocity.Y : 0;
      }
      else if (r >= hitL && or < oHitL)
      {
        this.Entity.Position.X = hitL - thisBoxCollider.Position.X - thisBoxCollider.Size.X - 0.001f;
        this.Velocity.X = hit.Rigidbody != null ? hit.Rigidbody.Velocity.X : 0;
      }
      else if (l <= hitR && ol > oHitR)
      {
        this.Entity.Position.X = hitR + thisBoxCollider.Position.X + thisBoxCollider.Size.X + 0.001f;
        this.Velocity.X = hit.Rigidbody != null ? hit.Rigidbody.Velocity.X : 0;
      }
    }

    internal override void Update(GameTime gameTime)
    {
      this.Entity.Position = this.Entity.Position + new Vector3(
        this.Velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds,
        this.Velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds,
        this.Entity.Position.Z);

      var collisions = this.collider.GetOverlapCollider();

      if (collisions.Count() > 0)
      {
        if (this.collider.GetType() == typeof(BoxCollider) && collisions.First().GetType() == typeof(BoxCollider))
          Update(gameTime, (BoxCollider)this.collider, (BoxCollider)collisions.First());
        else
          throw new System.NotImplementedException("Colldier type not supported");
      }

      this.OldEntityPosition = this.Entity.Position;
    }
  }
}