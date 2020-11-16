using CraftEnd.CoreGame.Content.Loader;
using CraftEnd.Engine;
using CraftEnd.Engine.Colission;
using Microsoft.Xna.Framework;

namespace CraftEnd.CoreGame
{
  public class LightSource : Entity
  {
    public void LoadContent(DungenonTilesetII0x72Loader loader)
    {
      var renderer = new SpriteRenderer();
      var animator = new SpriteAnimator(this, new SpriteAnimation[] {
        new SpriteAnimation("idle", loader.Texture, loader.TryGetSpriteCoordinates("coin_anim"))
      }, "idle");
      renderer.Sprites.Add(animator);
      this.AddComponent(renderer);
      this.AddComponent(new BoxCollider(new Vector2(1,1), new Vector2(0, 0)));
    }
  }
}