using FortRise;
using Monocle;
using MonoMod.ModInterop;
using KonspiracieCustomArrows;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using TowerFall;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace KonspiracieCustomArrows;


[Fort("com.KonspiracieTheorie.BlinkArrows", "BlinkArrows")]
public class KonspiracieArrowsModModule : FortModule
{
    public static Atlas ArrowAtlas;
    public static Atlas VariantAtlas;
    public static KonspiracieArrowsModModule Instance;
    public List<Variant> ArrowVariantList = new List<Variant>();
    public static List<CustomArrowFormat> CustomArrowList = new List<CustomArrowFormat>();
    public static List<CustomPickupFormat> CustomPickupList = new List<CustomPickupFormat>();
    public KonspiracieArrowsModModule() 
    {
        Instance = this;
    }
    public override Type SettingsType => typeof(ExampleModSettings);
    public static ExampleModSettings Settings => (ExampleModSettings)Instance.InternalSettings;

    public override void LoadContent()
    {
        ArrowAtlas = Content.LoadAtlas("Atlas/ArrowAtlas.xml", "Atlas/ArrowAtlas.png");
        VariantAtlas = Content.LoadAtlas("Atlas/VariantAtlas.xml", "Atlas/VariantAtlas.png");
    }
    public override void Load()
    {
        Debugger.Launch();
        typeof(ModExports).ModInterop();
        typeof(ExplosiveImports).ModInterop();
       // AddArrows();
  

    }
    public void AddArrows()
    {
        CustomArrowList.Add(new CustomArrowFormat(GetID("BlinkArrow"), "StartWithBlinkArrows", "ExcludeBlinkArrows", "MIRAGE", KonspiracieArrowsModModule.CustomArrowList.Count + 1));
        CustomArrowList.Add(new CustomArrowFormat(GetID("GombocArrow"), "StartWithGombocArrows", "ExcludeGombocArrows", "FLIGHT", KonspiracieArrowsModModule.CustomArrowList.Count + 1));
        CustomArrowList.Add(new CustomArrowFormat(GetID("SublimeArrow"), "StartWithSublimeArrows", "ExcludeSublimeArrows", "THORNWOOD", KonspiracieArrowsModModule.CustomArrowList.Count + 1));
        CustomArrowList.Add(new CustomArrowFormat(GetID("QuicksilverArrow"), "StartWithQuicksilverArrows", "ExcludeQuicksilverArrows", "FROSTFANG KEEP", KonspiracieArrowsModModule.CustomArrowList.Count + 1));
        CustomArrowList.Add(new CustomArrowFormat(GetID("ArrowBombArrow"), "StartWithArrowBombArrows", "ExcludeArrowBombArrows", "CATACLYSM", KonspiracieArrowsModModule.CustomArrowList.Count + 1));
        CustomArrowList.Add(new CustomArrowFormat(GetID("NyoomArrow"), "StartWithNyoomArrows", "ExcludeNyoomArrows", "BACKFIRE", KonspiracieArrowsModModule.CustomArrowList.Count + 1));
        CustomArrowList.Add(new CustomArrowFormat(GetID("OccultArrow"), "StartWithOccultArrows", "ExcludeOccultArrows", "TWILIGHT SPIRE", KonspiracieArrowsModModule.CustomArrowList.Count + 1));
        ArrowTypes GetID(string arrowName)
        {
            return RiseCore.ArrowsRegistry[arrowName].Types;
        }
    }
    
    public static void CreatePickups()
    {
        OnTower Patcher;


        Patcher.VERSUS_Mirage.IncreaseTreasureRates(RiseCore.PickupRegistry["BlinkArrow"].ID);
        Patcher.VERSUS_Flight.IncreaseTreasureRates(RiseCore.PickupRegistry["GombocArrow"].ID);
        Patcher.VERSUS_Thornwood.IncreaseTreasureRates(RiseCore.PickupRegistry["SublimeArrow"].ID);
        Patcher.VERSUS_FrostfangKeep.IncreaseTreasureRates(RiseCore.PickupRegistry["QuicksilverArrow"].ID);
        Patcher.VERSUS_Cataclysm.IncreaseTreasureRates(RiseCore.PickupRegistry["ArrowBombArrow"].ID);
        Patcher.VERSUS_Backfire.IncreaseTreasureRates(RiseCore.PickupRegistry["NyoomArrow"].ID);
        Patcher.VERSUS_TwilightSpire.IncreaseTreasureRates(RiseCore.PickupRegistry["OccultArrow"].ID);

        RemoveFromAllVanillaTowers(Patcher, "SublimeArrowVapor");
        RemoveFromAllVanillaTowers(Patcher, "QuicksilverArrowDrip");
        RemoveFromAllVanillaTowers(Patcher, "ArrowBombArrowFragment");

        static void RemoveFromAllVanillaTowers(OnTower ontower, string removed)
        {
            ontower.VERSUS_SacredGround.RemoveTreasureRates(RiseCore.PickupRegistry[removed].ID);
            ontower.VERSUS_TwilightSpire.RemoveTreasureRates(RiseCore.PickupRegistry[removed].ID);
            ontower.VERSUS_Backfire.RemoveTreasureRates(RiseCore.PickupRegistry[removed].ID);
            ontower.VERSUS_Flight.RemoveTreasureRates(RiseCore.PickupRegistry[removed].ID);
            ontower.VERSUS_Mirage.RemoveTreasureRates(RiseCore.PickupRegistry[removed].ID);
            ontower.VERSUS_Thornwood.RemoveTreasureRates(RiseCore.PickupRegistry[removed].ID);
            ontower.VERSUS_FrostfangKeep.RemoveTreasureRates(RiseCore.PickupRegistry[removed].ID);
            ontower.VERSUS_KingsCourt.RemoveTreasureRates(RiseCore.PickupRegistry[removed].ID);
            ontower.VERSUS_SunkenCity.RemoveTreasureRates(RiseCore.PickupRegistry[removed].ID);
            ontower.VERSUS_Moonstone.RemoveTreasureRates(RiseCore.PickupRegistry[removed].ID);
            ontower.VERSUS_Towerforge.RemoveTreasureRates(RiseCore.PickupRegistry[removed].ID);
            ontower.VERSUS_Ascension.RemoveTreasureRates(RiseCore.PickupRegistry[removed].ID);
            ontower.VERSUS_TheAmaranth.RemoveTreasureRates(RiseCore.PickupRegistry[removed].ID);
            ontower.VERSUS_Dreadwood.RemoveTreasureRates(RiseCore.PickupRegistry[removed].ID);
            ontower.VERSUS_Darkfang.RemoveTreasureRates(RiseCore.PickupRegistry[removed].ID);
            ontower.VERSUS_Cataclysm.RemoveTreasureRates(RiseCore.PickupRegistry[removed].ID);
        }

    }
    public override void OnVariantsRegister(VariantManager manager, bool noPerPlayer = false)
    {
        base.OnVariantsRegister(manager, noPerPlayer);

        manager.AddArrowVariant(RiseCore.ArrowsRegistry["BlinkArrow"], VariantAtlas["variants/startWithBlinkArrows"], VariantAtlas["variants/excludeBlinkArrows"]);
        manager.AddArrowVariant(RiseCore.ArrowsRegistry["GombocArrow"], VariantAtlas["variants/startWithGombocArrows"], VariantAtlas["variants/excludeGombocArrows"]);
        manager.AddArrowVariant(RiseCore.ArrowsRegistry["SublimeArrow"], VariantAtlas["variants/startWithSublimeArrows"], VariantAtlas["variants/excludeSublimeArrows"]);
        manager.AddArrowVariant(RiseCore.ArrowsRegistry["QuicksilverArrow"], VariantAtlas["variants/startWithQuicksilverArrows"], VariantAtlas["variants/excludeQuicksilverArrows"]);
        manager.AddArrowVariant(RiseCore.ArrowsRegistry["ArrowBombArrow"], VariantAtlas["variants/startWithArrowBombArrows"], VariantAtlas["variants/excludeArrowBombArrows"]);
        manager.AddArrowVariant(RiseCore.ArrowsRegistry["NyoomArrow"], VariantAtlas["variants/startWithNyoomArrows"], VariantAtlas["variants/excludeNyoomArrows"]);
        manager.AddArrowVariant(RiseCore.ArrowsRegistry["OccultArrow"], VariantAtlas["variants/startWithOccultArrows"], VariantAtlas["variants/excludeOccultArrows"]);
        CreatePickups();
    }

   
       // var VarietyPack = variants.AddVariant("VarietyPack", info with { Description = "START WITH MULTIPLE KINDS OF ARROWS" }, VariantFlags.PerPlayer | VariantFlags.CanRandom, true); -- A reminder of the past

    public override void Unload()
    {
        
    }
}
[ModExportName("com.fortrise.BlinkArrows")]
public static class ModExports
{
    public static void AutoLinkArrowStartVariants(MatchVariants variants, Variant VariantToBeLinked)
    {
        variants.CreateCustomLinks(VariantToBeLinked, variants.StartWithBoltArrows);
        variants.CreateCustomLinks(VariantToBeLinked, variants.StartWithBombArrows);
        variants.CreateCustomLinks(VariantToBeLinked, variants.StartWithBrambleArrows);
        variants.CreateCustomLinks(VariantToBeLinked, variants.StartWithDrillArrows);
        variants.CreateCustomLinks(VariantToBeLinked, variants.StartWithFeatherArrows);
        variants.CreateCustomLinks(VariantToBeLinked, variants.StartWithLaserArrows);
        variants.CreateCustomLinks(VariantToBeLinked, variants.StartWithPrismArrows);
        variants.CreateCustomLinks(VariantToBeLinked, variants.StartWithRandomArrows);
        variants.CreateCustomLinks(VariantToBeLinked, variants.StartWithSuperBombArrows);
        variants.CreateCustomLinks(VariantToBeLinked, variants.StartWithTriggerArrows);
        variants.CreateCustomLinks(VariantToBeLinked, variants.StartWithToyArrows);
        foreach (var OtherArrow in KonspiracieArrowsModModule.Instance.ArrowVariantList)
        {
            variants.CreateCustomLinks(VariantToBeLinked, OtherArrow);
        }
        KonspiracieArrowsModModule.Instance.ArrowVariantList.Add(VariantToBeLinked);
    }
}

[ModImportName("ExplosionLibraryExport")]
public static class ExplosiveImports
{
    public static Func<Level, Vector2, int, bool, bool, bool, bool> SpawnSmall;
    public static Func<Level, Vector2, int, bool, bool> SpawnSmallSuper;
}