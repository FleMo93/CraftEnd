using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CraftEnd.Engine
{
  public abstract class PenguinGame
  {
    public ContentManager Content { get; private set; }
    protected bool IsMouseVisible
    {
      get { return this.game.IsMouseVisible; }
      set { this.game.IsMouseVisible = value; }
    }

    protected GraphicsDevice GraphicsDevice { get; private set; }
    protected GraphicsDeviceManager GraphicsDeviceManager { get; private set; }

    private Game game;

    public PenguinGame() { }

    internal void Start(Game game, GraphicsDeviceManager graphicsDeviceManager)
    {
      this.game = game;
      this.GraphicsDeviceManager = graphicsDeviceManager;
      this.Content = game.Content;
      Start();
    }

    protected void Exit()
    {
      this.game.Exit();
    }

    protected abstract void Start();
    internal void Initialize(GraphicsDevice graphicsDevice)
    {
      this.GraphicsDevice = graphicsDevice;
    }
    internal protected abstract void Initialize();
    internal protected abstract void LoadContent();
    internal protected abstract void Update(GameTime gameTime);
  }
}