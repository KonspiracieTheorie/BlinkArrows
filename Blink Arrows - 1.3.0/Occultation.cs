// TowerFall.Ice
using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Monocle;
using TowerFall;

namespace KonspiracieCustomArrows;
public class Occultation : Actor
{
    private FlashingImage image;
    public int OwnerIndex { get; private set; }
    private Solid riding;

    private float size = 0.01f;
    private float sizeTracker = 0f;
    private float fadeTracker = 0f;

    public Occultation(Vector2 position, float rotation) : base(position)
    {
        Position = position;
        base.Depth = -50000;
        Tag(GameTags.PlayerCollider, GameTags.ExplosionCollider, GameTags.ShockCollider);
        ScreenWrap = true;
        Pushable = false;
        base.Collider = new WrapHitbox(8f, 8f, -4f, -4f);
        image = new FlashingImage(KonspiracieArrowsModModule.ArrowAtlas["Occultation"]);
        image.CenterOrigin();
        image.Rotation = rotation;
        Add(image);
    }
    public static IEnumerator CreateOccultation(Level level, Vector2 at, float rotation, int ownerIndex, Action onComplete)
    {
        Occultation MyOccultation = new Occultation(at, rotation);
        MyOccultation.OwnerIndex = ownerIndex;
        level.Add(MyOccultation);
        yield return 0.000001f;
        onComplete?.Invoke();
    }
    public override void DoWrapRender()
    {
        image.DrawOutline();
        base.DoWrapRender();
    }
    public override bool IsRiding(Solid solid)
    {
        return riding == solid;
    }

    public override void Removed()
    {
        riding = null;
    }


    public override void Added()
    {
        base.Added();

    }




    public override void Update()
    {
        base.Update();
        if (size < 100 && fadeTracker == 0)
        {
            size += 10;
        }
        image.Scale = new Vector2(size, size);
        sizeTracker++;
        if (sizeTracker > 1000 && OwnerIndex != -1 && base.Level.Session.MatchSettings.Variants.InfiniteBrambles[OwnerIndex])
        {
            fadeTracker = 1;
            size -= 0.3f;
            if (size < 1)
            {
                RemoveSelf();
            }
        }
    }




    public override bool IsRiding(JumpThru jumpThru)
    {
        return false;
    }

}
