using System.Reflection;
using System.Runtime.CompilerServices;
using FortRise;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Utils;
using TowerFall;

namespace KonspiracieCustomArrows;

[CustomArrows("GombocArrow", "CreateGraphicPickup")]
public class GombocArrow : Arrow
{
    // This is automatically been set by the mod loader
    public override ArrowTypes ArrowType { get; set; }
    private bool used, canDie;
    private Image normalImage;
    private Image buriedImage;


    public static ArrowInfo CreateGraphicPickup()
    {
        var graphic = new Sprite<int>(KonspiracieArrowsModModule.ArrowAtlas["GombocArrowPickup"], 12, 12, 0);
        graphic.CenterOrigin();
        var arrowInfo = ArrowInfo.Create(graphic, KonspiracieArrowsModModule.ArrowAtlas["GombocArrowHud"]);
        arrowInfo.Name = "Gomboc";
        return arrowInfo;
    }

    public GombocArrow() : base()
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
        normalImage = new Image(KonspiracieArrowsModModule.ArrowAtlas["GombocArrow"]);
        normalImage.Origin = new Vector2(13f, 3f);
        buriedImage = new Image(KonspiracieArrowsModModule.ArrowAtlas["GombocArrowBuried"]);
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

    }
}