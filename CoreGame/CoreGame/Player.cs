using CraftEnd.CoreGame.Content.Loader;
using CraftEnd.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CraftEnd.CoreGame
{
  public class Player : Entity
  {
    private SpriteAnimator characterAnimator;
    private float speed = 1;

    public void LoadContent(DungenonTilesetII0x72Loader content, Texture2D characterShadow)
    {
      var spriteRenderer = new SpriteRenderer();
      this.AddComponent(spriteRenderer);

      this.characterAnimator = new SpriteAnimator(this, new[] {
        new SpriteAnimation("idle", content.Texture, content.TryGetSpriteCoordinates("knight_m_idle_anim")),
        new SpriteAnimation("run", content.Texture, content.TryGetSpriteCoordinates("knight_m_run_anim")),
        new SpriteAnimation("hit", content.Texture, content.TryGetSpriteCoordinates("knight_m_hit_anim"))
      }, "idle");
      this.characterAnimator.Scale = new Vector2(2, 2);
      this.characterAnimator.RenderPivot = RenderPivot.Center;

      var shadowSprite = new SpriteStatic(this, characterShadow, null, new Vector2(0, 1f), RenderPivot.Center);
      spriteRenderer.Sprites.Add(shadowSprite);
      spriteRenderer.Sprites.Add(characterAnimator);
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

      if (verticalMovement != 0 || horizontalMovement != 0 && this.characterAnimator.CurrentAnimationName != "run")
        this.characterAnimator.SetAnimation("run");
      else if (verticalMovement == 0 && horizontalMovement == 0 && this.characterAnimator.CurrentAnimationName != "idle")
        this.characterAnimator.SetAnimation("idle");

      if (horizontalMovement > 0)
        this.characterAnimator.FlipHorizontal = false;
      else if (horizontalMovement < 0)
        this.characterAnimator.FlipHorizontal = true;

      base.Update(gameTime);
    }
  }
}