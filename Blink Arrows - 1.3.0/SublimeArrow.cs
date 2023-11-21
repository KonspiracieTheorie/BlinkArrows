using System.Reflection;
using System.Runtime.CompilerServices;
using FortRise;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Utils;
using TowerFall;

namespace KonspiracieCustomArrows;

[CustomArrows("SublimeArrow", "CreateGraphicPickup")]
public class SublimeArrow : Arrow
{
    // This is automatically been set by the mod loader
    public override ArrowTypes ArrowType { get; set; }
    private bool used, canDie;
    private Image normalImage;
    private Image buriedImage;
    private float SublimateTimer = 2;

    public static ArrowInfo CreateGraphicPickup()
    {
        var graphic = new Sprite<int>(KonspiracieArrowsModModule.ArrowAtlas["SublimeArrowPickup"], 12, 12, 0);
        graphic.CenterOrigin();
        var arrowInfo = ArrowInfo.Create(graphic, KonspiracieArrowsModModule.ArrowAtlas["SublimeArrowHud"]);
        arrowInfo.Name = "Sublime";
        return arrowInfo;
    }

    public SublimeArrow() : base()
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
        normalImage = new Image(KonspiracieArrowsModModule.ArrowAtlas["SublimeArrow"]);
        normalImage.Origin = new Vector2(13f, 3f);
        buriedImage = new Image(KonspiracieArrowsModModule.ArrowAtlas["SublimeArrowBuried"]);
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
    public override void ShootUpdate()
    {
        this.UpdateSeeking();
    }
    public override void Update()
    {
        base.Update();
        
        if (SublimateTimer <= 0 && (int)this.State == 0)
        {
            Level.Add(Arrow.Create(RiseCore.ArrowsRegistry["SublimeArrowVapor"].Types, Owner, Position, -1.5708f));
            SublimateTimer = 3;
        }
        else
        {
            SublimateTimer--;
        }

        if (canDie)
        {
            RemoveSelf();
        }
    }
}