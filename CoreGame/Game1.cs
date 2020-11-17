using CraftEnd.CoreGame;
using CraftEnd.CoreGame.Content.Loader;
using CraftEnd.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CraftEnd
{
  public class Game1 : Game
  {
    private GraphicsDeviceManager _graphics;
    private Player player;
    private Camera camera;
    // private LightSource lightSource;

    public Game1()
    {
      _graphics = new GraphicsDeviceManager(this);
      _graphics.PreferredBackBufferWidth = 800;
      _graphics.PreferredBackBufferHeight = 800;
      Content.RootDirectory = "Content";
      IsMouseVisible = true;

      this.player = new Player();
      this.player.Position = new Vector3(2, 2, 0);
      // lightSource = new LightSource();
    }

    protected override void Initialize()
    {
      Entity.Entities.ForEach((Entity entity) => entity.Initialize());

      base.Initialize();
    }

    protected override void LoadContent()
    {
      Renderer.LoadContent(GraphicsDevice, _graphics, Content.Load<Texture2D>(CraftEnd.CoreGame.Content.Content.Texture2DCoordinateAxis), Content.Load<SpriteFont>("Arial"));
      this.camera = new Camera(_graphics, GraphicsDevice);
      this.camera.Zoom = 40;
      this.camera.RenderPivot = RenderPivot.Center;
      var tiledTileSet = new TiledTileset(CraftEnd.CoreGame.Content.Content.FilePathTiled0x72DungenTileset, Content);
      var devLevelMap = new TiledMap(CraftEnd.CoreGame.Content.Content.FilePathTiledLevelDev, tiledTileSet);
      var devLevel = new Level(devLevelMap);
      Entity.Entities.Insert(0, devLevel);

      Entity.Entities.ForEach((Entity entity) =>
      {
        entity.LoadContent(Content);
        this.camera.AddEntity(entity);
      });

      var dungeonTileSet0x72Loader = new DungenonTilesetII0x72Loader();
      dungeonTileSet0x72Loader.LoadContent(Content);
      var characterShadow = Content.Load<Texture2D>(CraftEnd.CoreGame.Content.Content.Texture2DCharacterShadow);
      this.player.LoadContent(dungeonTileSet0x72Loader, characterShadow);
      // lightSource.Position = new Vector3(-2, -3, 2);
      // this.lightSource.LoadContent(dungeonTileSet0x72Loader);

      var guiLayer = new Camera(_graphics, GraphicsDevice);
      guiLayer.Zoom = 60;
      var uiLife = new Life();
      guiLayer.AddEntity(uiLife);
      uiLife.LoadContent(dungeonTileSet0x72Loader);
      uiLife.NumberOfHearts = 5;
      uiLife.Position = new Vector3(0.1f, 0, 0);
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      Entity.Entities.ForEach((Entity entity) => entity.Update(gameTime));
      this.camera.Position.X = this.player.Position.X;
      this.camera.Position.Y = this.player.Position.Y;
      // this.lightSource.Position.X = Mouse.GetState().Position.X;
      // this.lightSource.Position.Y = Mouse.GetState().Position.Y;
      // System.Console.WriteLine(Mouse.GetState().Position);
      // System.Console.WriteLine(this.camera.ScreenToWorldPosition(330, 380));

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      Renderer.Draw(gameTime);
      base.Draw(gameTime);
    }
  }
}
