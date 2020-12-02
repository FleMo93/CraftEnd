using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public class PenguinEngine : Game
  {
    private PenguinGame penguinGame;
    private GraphicsDeviceManager graphicsDeviceManager;
    private string texture2DCoordinateAxis;
    public PenguinEngine(System.Type penguinGame, string texture2DCoordinateAxis)
    {
      this.penguinGame = (PenguinGame)System.Activator.CreateInstance(penguinGame);
      this.graphicsDeviceManager = new GraphicsDeviceManager(this);
      this.penguinGame.Start(this, graphicsDeviceManager);
      this.texture2DCoordinateAxis = texture2DCoordinateAxis;
    }

    protected override void Initialize()
    {
      this.penguinGame.Initialize(GraphicsDevice);
      base.Initialize();
    }

    protected override void LoadContent()
    {
      Renderer.LoadContent(
        GraphicsDevice,
        graphicsDeviceManager,
        Content.Load<Texture2D>(this.texture2DCoordinateAxis),
        Content.Load<SpriteFont>("Arial"));
      this.penguinGame.LoadContent();
      base.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
      this.penguinGame.Update(gameTime);
      Entity.Entities.ForEach((Entity entity) =>
      {
        if (!entity.Active)
          return;

        entity.Update(gameTime);
      });
      base.Update(gameTime);
      KeyboardExtension.Update();
    }

    protected override void Draw(GameTime gameTime)
    {
      Renderer.Draw(gameTime);
      base.Draw(gameTime);
    }
  }
}