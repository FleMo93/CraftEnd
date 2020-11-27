using CraftEnd.CoreGame.Content.Loader;
using CraftEnd.Engine;
using CraftEnd.Engine.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CraftEnd.CoreGame
{
  public class Player : Entity
  {
    private SpriteAnimator characterAnimator;
    private float speed = 1;
    private Rigidbody rigidbody;

    public Player(DungenonTilesetII0x72Loader content, Texture2D characterShadow)
    {
      var spriteRenderer = new SpriteRenderer();
      this.AddComponent(spriteRenderer);

      var idleCoord = content.TryGetSpriteCoordinates("knight_m_idle_anim");
      var height = (float)idleCoord[0].Height;
      var width = (float)idleCoord[0].Width;
      if (height > width)
      {
        height = height / width;
        width = 1;
      }
      else if (width > height)
      {
        width = width / height;
        height = 1;
      }


      this.characterAnimator = new SpriteAnimator(this, new[] {
        new SpriteAnimation("idle", content.Texture, idleCoord),
        new SpriteAnimation("run", content.Texture, content.TryGetSpriteCoordinates("knight_m_run_anim")),
        new SpriteAnimation("hit", content.Texture, content.TryGetSpriteCoordinates("knight_m_hit_anim"))
      }, "idle");
      this.characterAnimator.Scale = new Vector2(width, height);
      this.characterAnimator.RenderPivot = RenderPivot.BottomCenter;

      var shadowSprite = new SpriteStatic(this, characterShadow, null, new Vector2(0, 0f), RenderPivot.Center);
      spriteRenderer.Sprites.Add(shadowSprite);
      spriteRenderer.Sprites.Add(characterAnimator);

      var collider = new BoxCollider(new Vector2(1, 0.4f), new Vector2(-0.5f, -0.4f));
      this.AddComponent(collider);
      this.AddComponent(this.rigidbody = new Rigidbody(collider));
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

      this.rigidbody.Velocity = new Vector2(horizontalMovement, verticalMovement) * speed;

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