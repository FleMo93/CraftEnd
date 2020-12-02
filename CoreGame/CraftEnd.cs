using CraftEnd.CoreGame;
using CraftEnd.CoreGame.Content.Loader;
using CraftEnd.Engine;
using CraftEnd.Engine.Physics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CraftEnd
{
  public class CraftEnd : PenguinGame
  {
    private Player player;
    private Camera camera;
    private Cursor cursor;

    protected override void Start()
    {
      GraphicsDeviceManager.PreferredBackBufferWidth = 1280;
      GraphicsDeviceManager.PreferredBackBufferHeight = 720;
      Content.RootDirectory = "Content";
      Layer.SetCollision(0, true, 0);
      Layer.SetCollision(1, true, 0, 1);
      IsMouseVisible = false;
    }

    protected override void Initialize()
    {
    }

    protected override void LoadContent()
    {
      this.camera = new Camera(GraphicsDeviceManager, GraphicsDevice);
      this.camera.Zoom = 10;
      this.camera.RenderPivot = RenderPivot.Center;

      var tiledTileSet = new TiledTileset(global::CraftEnd.CoreGame.Content.Content.FilePathTiled0x72DungenTileset, Content);
      var devLevelMap = new TiledMap(global::CraftEnd.CoreGame.Content.Content.FilePathTiledLevelDev, tiledTileSet);
      var devLevel = new Level(devLevelMap);
      this.camera.AddEntity(devLevel);

      foreach (var child in devLevel)
        this.camera.AddEntity(child);

      var dungeonTileSet0x72Loader = new DungenonTilesetII0x72Loader();
      dungeonTileSet0x72Loader.LoadContent(Content);

      this.player = new Player(
        dungeonTileSet0x72Loader,
        Content.Load<Texture2D>(global::CraftEnd.CoreGame.Content.Content.Texture2DCharacterShadow));
      this.player.Position = new Vector3(2, 2, 0);
      this.camera.AddEntity(this.player);

      var guiCamera = new Camera(GraphicsDeviceManager, GraphicsDevice);
      guiCamera.Zoom = 8;

      var uiLife = new Life(dungeonTileSet0x72Loader);
      guiCamera.AddEntity(uiLife);
      uiLife.NumberOfHearts = 5;
      uiLife.Position = new Vector3(0.1f, 0, 0);

      var inventory = new Inventory(Content.Load<Texture2D>(global::CraftEnd.CoreGame.Content.Content.Texture2DInventoryTile), 10, 6);
      guiCamera.AddEntity(inventory);

      this.cursor = new Cursor(Content.Load<Texture2D>(global::CraftEnd.CoreGame.Content.Content.Texture2DCursor), guiCamera);
      guiCamera.AddEntity(this.cursor);
    }

    protected override void Update(GameTime gameTime)
    {
      if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        Exit();

      this.camera.Position.X = this.player.Position.X;
      this.camera.Position.Y = this.player.Position.Y;

    }
  }
}
