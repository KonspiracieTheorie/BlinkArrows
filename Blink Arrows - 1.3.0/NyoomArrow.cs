using System.Reflection;
using System.Runtime.CompilerServices;
using FortRise;
using Microsoft.Xna.Framework;
using Monocle;
using MonoMod.Utils;
using TowerFall;

namespace KonspiracieCustomArrows;

[CustomArrows("NyoomArrow", "CreateGraphicPickup")]
public class NyoomArrow : Arrow
{
    // This is automatically been set by the mod loader
    public override ArrowTypes ArrowType { get; set; }
    private const float SPEED = 8f;
    protected override float StartSpeed => 10f;
    private bool used, canDie;
    private Image normalImage;
    private Image buriedImage;
    private Counter shootingCounter;
    private bool gravCounter;


    public static ArrowInfo CreateGraphicPickup()
    {
        var graphic = new Sprite<int>(KonspiracieArrowsModModule.ArrowAtlas["NyoomArrowPickup"], 12, 12, 0);
        graphic.Add(0, 0.3f, new int[2] { 0, 1 });
        graphic.Play(0, false);
        graphic.CenterOrigin();
        var arrowInfo = ArrowInfo.Create(graphic, KonspiracieArrowsModModule.ArrowAtlas["NyoomArrowHud"]);
        arrowInfo.Name = "Nyoom";
        return arrowInfo;
    }

    public NyoomArrow() : base()
    {
        shootingCounter = new Counter();
    }
    protected override void Init(LevelEntity owner, Vector2 position, float direction)
    {
        base.Init(owner, position, direction);
        used = (canDie = false);
        Sounds.sfx_boltArrowExplode.Play(base.X);
        StopFlashing();
        shootingCounter.Set(25);
    }
    protected override void CreateGraphics()
    {
        normalImage = new Image(KonspiracieArrowsModModule.ArrowAtlas["NyoomArrow"]);
        normalImage.Origin = new Vector2(13f, 3f);
        buriedImage = new Image(KonspiracieArrowsModModule.ArrowAtlas["NyoomArrowBuried"]);
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
        base.Update();
        if (canDie)
        {
            RemoveSelf();
        }
        base.Update();
        if (canDie)
        {
            RemoveSelf();
        }
        base.Update();
        if (canDie)
        {
            RemoveSelf();
        }
    }

    public override void ShootUpdate()
    {

    }

    public override void OnPlayerCollide(Player player)
    {
        base.OnPlayerCollide(player);
    }
}