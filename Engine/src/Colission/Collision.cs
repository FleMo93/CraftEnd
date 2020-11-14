namespace CraftEnd.Engine.Colission
{
  public class Collision
  {
    static float GetCollisionArea(BoxCollider a, BoxCollider b)
    {
      return 0;
    }

    public readonly Collider ColliderSource;
    public readonly Collider ColliderTarget;
    public readonly float ColissionArea;

    public Collision (Collider source, Collider target)
    {
      this.ColliderSource = source;
      this.ColliderTarget = target;

      if (source.GetType() == typeof(BoxCollider) && target.GetType() == typeof(BoxCollider))
        this.ColissionArea = GetCollisionArea(source as BoxCollider, target as BoxCollider);
      else
        throw new System.NotImplementedException();
    }
  }
}