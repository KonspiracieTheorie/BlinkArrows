using System.Reflection;
using System.Runtime.CompilerServices;
using FortRise;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Utils;
using TowerFall;

namespace KonspiracieCustomArrows;

[CustomArrows("ArrowBombArrow", "CreateGraphicPickup")]
public class ArrowBombArrow : Arrow
{
    // This is automatically been set by the mod loader
    public override ArrowTypes ArrowType { get; set; }
    private bool used, canDie;
    private Image normalImage;
    private Image buriedImage;
    private Alarm explodeAlarm;


    public static ArrowInfo CreateGraphicPickup()
    {
        var graphic = new Sprite<int>(KonspiracieArrowsModModule.ArrowAtlas["ArrowBombArrowPickup"], 12, 12, 0);
        graphic.Add(0, 0.3f, new int[2] { 0, 1 });
        graphic.Play(0, false);
        graphic.CenterOrigin();
        var arrowInfo = ArrowInfo.Create(graphic, KonspiracieArrowsModModule.ArrowAtlas["ArrowBombArrowHud"]);
        arrowInfo.Name = "Arrow Bomb";
        return arrowInfo;
    }

    public ArrowBombArrow() : base()
    {
    }
    protected override void Init(LevelEntity owner, Vector2 position, float direction)
    {
        base.Init(owner, position, direction);
        used = (canDie = false);
        explodeAlarm = Alarm.Create(Alarm.AlarmMode.Persist, Explode, 23);
        explodeAlarm.Start();
        StopFlashing();
    }
    protected override void CreateGraphics()
    {
        normalImage = new Image(KonspiracieArrowsModModule.ArrowAtlas["ArrowBombArrow"]);
        normalImage.Origin = new Vector2(13f, 3f);
        buriedImage = new Image(KonspiracieArrowsModModule.ArrowAtlas["ArrowBombArrowBuried"]);
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

        if (explodeAlarm.Active)
        {
            explodeAlarm.Update();
        }
        if (canDie)
        {
            RemoveSelf();
        }
    }

    public void Explode()
    {
        Level.Add(Arrow.Create(RiseCore.ArrowsRegistry["ArrowBombArrowFragment"].Types, Owner, Position, Direction + 1.5708f));
        Level.Add(Arrow.Create(RiseCore.ArrowsRegistry["ArrowBombArrowFragment"].Types, Owner, Position, Direction - 1.5708f));
        Level.Add(Arrow.Create(RiseCore.ArrowsRegistry["ArrowBombArrowFragment"].Types, Owner, Position, Direction + 3.1416f));
        Level.Add(Arrow.Create(RiseCore.ArrowsRegistry["ArrowBombArrowFragment"].Types, Owner, Position, Direction));

        Level.Add(Arrow.Create(RiseCore.ArrowsRegistry["ArrowBombArrowFragment"].Types, Owner, Position, Direction + 0.7854f));
        Level.Add(Arrow.Create(RiseCore.ArrowsRegistry["ArrowBombArrowFragment"].Types, Owner, Position, Direction - 0.7854f));
        Level.Add(Arrow.Create(RiseCore.ArrowsRegistry["ArrowBombArrowFragment"].Types, Owner, Position, Direction + 2.356f));
        Level.Add(Arrow.Create(RiseCore.ArrowsRegistry["ArrowBombArrowFragment"].Types, Owner, Position, Direction - 2.356f));
        Sounds.pu_bombArrowExplode.Play(base.X);
        RemoveSelf();
    }
}