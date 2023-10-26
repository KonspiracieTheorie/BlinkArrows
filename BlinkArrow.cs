using System.Reflection;
using System.Runtime.CompilerServices;
using FortRise;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Utils;
using TowerFall;

namespace KonspiracieCustomArrows;

[CustomArrows("BlinkArrows", "CreateGraphicPickup")]
public class BlinkArrow : Arrow
{
    // This is automatically been set by the mod loader
    public override ArrowTypes ArrowType { get; set; }
    private bool used, canDie;
    private Image normalImage;
    private Image buriedImage;


    public static ArrowInfo CreateGraphicPickup()
    {
        var graphic = new Sprite<int>(ExampleArrowModModule.BlinkAtlas["BlinkArrowPickup"], 12, 12, 0);
        graphic.Add(0, 0.3f, new int[2] { 0, 1 });
        graphic.Play(0, false);
        graphic.CenterOrigin();
        var arrowInfo = ArrowInfo.Create(graphic, ExampleArrowModModule.BlinkAtlas["BlinkArrowHud"]);
        arrowInfo.Name = "Blink";
        return arrowInfo;
    }

    public BlinkArrow() : base()
    {
    }
    protected override void Init(LevelEntity owner, Vector2 position, float direction)
    {
        base.Init(owner, position, direction);
        used = (canDie = false);
        StopFlashing();
    }
    protected override void CreateGraphics()
    {
        normalImage = new Image(ExampleArrowModModule.BlinkAtlas["BlinkArrow"]);
        normalImage.Origin = new Vector2(13f, 3f);
        buriedImage = new Image(ExampleArrowModModule.BlinkAtlas["BlinkArrowBuried"]);
        buriedImage.Origin = new Vector2(13f, 3f);
        Graphics = new Image[2] { normalImage, buriedImage };
        Add(Graphics);
    }

    protected override void InitGraphics()
    {
        normalImage.Visible = true;
        buriedImage.Visible = false;
    }

    protected override void SwapToBuriedGraphics()
    {
        normalImage.Visible = false;
        buriedImage.Visible = true;
    }

    protected override void SwapToUnburiedGraphics()
    {
        normalImage.Visible = true;
        buriedImage.Visible = false;
    }

    public override bool CanCatch(LevelEntity catcher)
    {
        return !used && base.CanCatch(catcher);
    }
    public override void Update()
    {

        base.Update();
        if (canDie)
        {
            RemoveSelf();
        }
    }

    protected override void HitWall(TowerFall.Platform platform)
    {
        var position = Owner.Position;
        Owner.Position = Position + GetOffset();
        Position = position;
        Sounds.boss_shieldForm.Play(base.X);
        RemoveSelf();

    }













    public Vector2 GetOffset()
    {
        float X = 0, Y = 0;
        Entity entity = Level.CollideFirst(new Rectangle((int)Position.X - 8, (int)Position.Y - 8, 16, 16), GameTags.Solid);
        int TestY = 4, TestX = 4;
        int AddFourToHeight = 0;
        if (entity != null)
        {
            for (bool done = false; done != true;)
            {
                Entity testentity = Level.CollideFirst(new Rectangle((int)Position.X, (int)Position.Y + TestY - 8, 8, 8), GameTags.Solid);
                if (testentity == null)
                {
                    Console.WriteLine("Up");
                    X = TestX; break;
                }
                testentity = Level.CollideFirst(new Rectangle((int)Position.X + TestX - 8, (int)Position.Y, 8, 8), GameTags.Solid);
                if (testentity == null)
                {
                    Console.WriteLine("Left");
                    Y = TestY; break;


                }
                testentity = Level.CollideFirst(new Rectangle((int)Position.X - 8 + TestX, (int)Position.Y - 8 + TestY, 8, 8), GameTags.Solid);
                if (testentity == null)
                {
                    X = TestX; Y = TestY; break;
                }
                testentity = Level.CollideFirst(new Rectangle((int)Position.X - TestX - 8, (int)Position.Y, 8, 8), GameTags.Solid);
                if (testentity == null)
                {
                    Console.WriteLine("Right");
                    X = -TestX; break;
                }
                testentity = Level.CollideFirst(new Rectangle((int)Position.X, (int)Position.Y - TestY - 8, 8, 8), GameTags.Solid);
                if (testentity == null)
                {
                    Console.WriteLine("Down");
                    AddFourToHeight = -8;
                    Y = -TestY; break;
                }
                testentity = Level.CollideFirst(new Rectangle((int)Position.X - 8 - TestX, (int)Position.Y - 8 - TestY, 8, 8), GameTags.Solid);
                if (testentity == null)
                {

                    X = -TestX - 8; Y = -TestY - 8; break;
                }
                testentity = Level.CollideFirst(new Rectangle((int)Position.X - 8 + TestX, (int)Position.Y - 8 - TestY, 8, 8), GameTags.Solid);
                if (testentity == null)
                {
                    X = TestX; Y = -TestY - 8; break;
                }
                testentity = Level.CollideFirst(new Rectangle((int)Position.X - 8 - TestX, (int)Position.Y - 8 + TestY, 8, 8), GameTags.Solid);
                if (testentity == null)
                {
                    X = -TestX - 8; Y = TestY; break;
                }
                TestX += 2;
                TestY += 2;
                TestX += 2;
                TestY += 2;
            }
        }
        return new Vector2(X, Y + AddFourToHeight);
    }
}