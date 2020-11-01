using CraftEnd.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CraftEnd.CoreGame
{
  public class Player : Entity
  {
    private SpriteSheet spriteSheet;
    private Animator animator;
    private float speed = 1;

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

    public override void Update(GameTime gameTime)
    {
      int verticalMovement = 0;
      int horizontalMovement = 0;

      if(Keyboard.GetState().IsKeyDown(Keys.W)) 
        verticalMovement -= 1;

      if (Keyboard.GetState().IsKeyDown(Keys.S))
        verticalMovement += 1;

      if (Keyboard.GetState().IsKeyDown(Keys.A))
        horizontalMovement -= 1;

      if (Keyboard.GetState().IsKeyDown(Keys.D))
        horizontalMovement += 1;

      this.Position.X += (float)(horizontalMovement * gameTime.ElapsedGameTime.TotalSeconds * speed);
      this.Position.Y += (float)(verticalMovement * gameTime.ElapsedGameTime.TotalSeconds * speed);

      if (verticalMovement != 0 || horizontalMovement != 0 && this.animator.CurrentAnimationName != "run")
        this.animator.SetAnimation("run");
      else if (verticalMovement == 0 && horizontalMovement == 0 && this.animator.CurrentAnimationName != "idle")
        this.animator.SetAnimation("idle");

      if (horizontalMovement > 0)
        this.animator.FlipHorizontal = false;
      else if (horizontalMovement < 0)
        this.animator.FlipHorizontal = true;
    }
  }
}