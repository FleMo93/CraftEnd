using CraftEnd.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.CoreGame
{
  public class Player : Entity
  {
    private SpriteSheet spriteSheet;
    private Animator animator;

    public override void LoadContent(ContentManager content)
    {
      this.spriteSheet = new SpriteSheet(content.Load<Texture2D>("HeroKnight/HeroKnight"), 9, 10);

      this.animator = new Animator(new[] {
        new Animation("idle", this.spriteSheet.SpritesByRow[0..7]),
        new Animation("run", this.spriteSheet.SpritesByRow[8..17]),
        new Animation("attack1", this.spriteSheet.SpritesByRow[18..23]),
        new Animation("attack2", this.spriteSheet.SpritesByRow[24..29]),
        new Animation("attack3", this.spriteSheet.SpritesByRow[30..37]),
      }, "idle");

      this.AddComponent(animator);
    }
  }
}