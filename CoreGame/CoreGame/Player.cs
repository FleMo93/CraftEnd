using CraftEnd.CoreGame.Content.Loader;
using CraftEnd.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CraftEnd.CoreGame
{
  public class Player : Entity
  {
    private Animator animator;
    private float speed = 1;

    public void LoadContent(DungenonTilesetII0x72Loader content)
    {
      this.animator = new Animator(new[] {
        new Animation("idle", content.Texture, content.TryGetSpriteCoordinates("knight_m_idle_anim")),
        new Animation("run", content.Texture, content.TryGetSpriteCoordinates("knight_m_run_anim")),
        new Animation("hit", content.Texture, content.TryGetSpriteCoordinates("knight_m_hit_anim"))
      }, "idle");

      this.AddComponent(animator);
    }

    public override void Update(GameTime gameTime)
    {
      int verticalMovement = 0;
      int horizontalMovement = 0;

      if (Keyboard.GetState().IsKeyDown(Keys.W))
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