using FortRise;
using HarmonyLib;
using Monocle;
using MonoMod.ModInterop;
using System;
using System.Diagnostics;
using TowerFall;



namespace KonspiracieCustomArrows;

[Fort("com.CoolModder.ExampleArrowMod", "ArrowTest")]

public class ExampleArrowModModule : FortModule
{
    public static ExampleArrowModModule Instance;
    public static Atlas VariantAtlas;
    public static Atlas BlinkAtlas;
    public static Atlas GombocAtlas;
    public static Atlas SublimeAtlas;
    public static Atlas QuicksilverAtlas;
    public static Atlas ArrowBombAtlas;
    public static Atlas NyoomAtlas;
    public ExampleArrowModModule() 
    {
        Instance = this;
    }
    

    public override Type SettingsType => typeof(ExampleModSettings);
    public static ExampleModSettings Settings => (ExampleModSettings)Instance.InternalSettings;


    public override void LoadContent()
    {
        VariantAtlas = Content.LoadAtlas("Atlas/VariantAtlas.xml", "Atlas/VariantAtlas.png");
        BlinkAtlas = Content.LoadAtlas("Atlas/BlinkArrow.xml", "Atlas/BlinkArrow.png");
        GombocAtlas = Content.LoadAtlas("Atlas/GombocArrow.xml", "Atlas/GombocArrow.png");
        SublimeAtlas = Content.LoadAtlas("Atlas/SublimeArrow.xml", "Atlas/SublimeArrow.png");
        QuicksilverAtlas = Content.LoadAtlas("Atlas/QuicksilverArrow.xml", "Atlas/QuicksilverArrow.png");
        ArrowBombAtlas = Content.LoadAtlas("Atlas/ArrowBombArrow.xml", "Atlas/ArrowBombArrow.png");
        NyoomAtlas = Content.LoadAtlas("Atlas/NyoomArrow.xml", "Atlas/NyoomArrow.png");
    }


    public override void Load()
    {
        typeof(OopsArrowsModImports).ModInterop();
    }


    public override void OnVariantsRegister(VariantManager manager, bool noPerPlayer = false)
    {
        var startBlinkInfo = new CustomVariantInfo("StartWithBlinkArrows", VariantAtlas["variants/startWithBlinkArrows"], CustomVariantFlags.PerPlayer | CustomVariantFlags.CanRandom);
        var StartWithBlinkArrows = manager.AddVariant(startBlinkInfo, noPerPlayer);
        var startGombocInfo = new CustomVariantInfo("StartWithGombocArrows", VariantAtlas["variants/startWithGombocArrows"], CustomVariantFlags.PerPlayer | CustomVariantFlags.CanRandom);
        var StartWithGombocArrows = manager.AddVariant(startGombocInfo, noPerPlayer);
        var startSublimeInfo = new CustomVariantInfo("StartWithSublimeArrows", VariantAtlas["variants/startWithSublimeArrows"], CustomVariantFlags.PerPlayer | CustomVariantFlags.CanRandom);
        var StartWithSublimeArrows = manager.AddVariant(startSublimeInfo, noPerPlayer);
        var startQuicksilverInfo = new CustomVariantInfo("StartWithQuicksilverArrows", VariantAtlas["variants/startWithQuicksilverArrows"], CustomVariantFlags.PerPlayer | CustomVariantFlags.CanRandom);
        var StartWithQuicksilverArrows = manager.AddVariant(startQuicksilverInfo, noPerPlayer);
        var startArrowBombInfo = new CustomVariantInfo("StartWithArrowBombArrows", VariantAtlas["variants/startWithArrowBombArrows"], CustomVariantFlags.PerPlayer | CustomVariantFlags.CanRandom);
        var StartWithArrowBombArrows = manager.AddVariant(startArrowBombInfo, noPerPlayer);
        var startNyoomInfo = new CustomVariantInfo("StartWithNyoomArrows", VariantAtlas["variants/startWithNyoomArrows"], CustomVariantFlags.PerPlayer | CustomVariantFlags.CanRandom);
        var StartWithNyoomArrows = manager.AddVariant(startNyoomInfo, noPerPlayer);


        var excludeBlinkInfo = new CustomVariantInfo("ExcludeBlinkArrows", VariantAtlas["variants/excludeBlinkArrows"], CustomVariantFlags.PerPlayer | CustomVariantFlags.CanRandom);
        var excludeBlinkArrows = manager.AddVariant(excludeBlinkInfo, noPerPlayer);
        var excludeGombocInfo = new CustomVariantInfo("ExcludeGombocArrows", VariantAtlas["variants/excludeGombocArrows"], CustomVariantFlags.PerPlayer | CustomVariantFlags.CanRandom);
        var excludeGombocArrows = manager.AddVariant(excludeGombocInfo, noPerPlayer);
        var excludeSublimeInfo = new CustomVariantInfo("ExcludeSublimeArrows", VariantAtlas["variants/excludeSublimeArrows"], CustomVariantFlags.PerPlayer | CustomVariantFlags.CanRandom);
        var excludeSublimeArrows = manager.AddVariant(excludeSublimeInfo, noPerPlayer);
        var excludeQuicksilverInfo = new CustomVariantInfo("ExcludeQuicksilverArrows", VariantAtlas["variants/excludeQuicksilverArrows"], CustomVariantFlags.PerPlayer | CustomVariantFlags.CanRandom);
        var excludeQuicksilverArrows = manager.AddVariant(excludeQuicksilverInfo, noPerPlayer);
        var excludeArrowBombInfo = new CustomVariantInfo("ExcludeArrowBombArrows", VariantAtlas["variants/excludeArrowBombArrows"], CustomVariantFlags.PerPlayer | CustomVariantFlags.CanRandom);
        var excludeArrowBombArrows = manager.AddVariant(excludeArrowBombInfo, noPerPlayer);
        var excludeNyoomInfo = new CustomVariantInfo("ExcludeNyoomArrows", VariantAtlas["variants/excludeNyoomArrows"], CustomVariantFlags.PerPlayer | CustomVariantFlags.CanRandom);
        var excludeNyoomArrows = manager.AddVariant(excludeNyoomInfo, noPerPlayer);


        OopsArrowsModImports.AddCustomArrow(RiseCore.ArrowsRegistry["BlinkArrows"].Types, "StartWithBlinkArrows", "ExcludeBlinkArrows", "MIRAGE");
        OopsArrowsModImports.AddCustomArrow(RiseCore.ArrowsRegistry["GombocArrows"].Types, "StartWithGombocArrows", "ExcludeGombocArrows", "FLIGHT");
        OopsArrowsModImports.AddCustomArrow(RiseCore.ArrowsRegistry["SublimeArrows"].Types, "StartWithSublimeArrows", "ExcludeSublimeArrows", "DARKFANG");
        OopsArrowsModImports.AddCustomArrow(RiseCore.ArrowsRegistry["QuicksilverArrows"].Types, "StartWithQuicksilverArrows", "ExcludeQuicksilverArrows", "FROSTFANG KEEP");
        OopsArrowsModImports.AddCustomArrow(RiseCore.ArrowsRegistry["ArrowBombArrows"].Types, "StartWithArrowBombArrows", "ExcludeArrowBombArrows", "TWILIGHT SPIRE");
        OopsArrowsModImports.AddCustomArrow(RiseCore.ArrowsRegistry["NyoomArrows"].Types, "StartWithNyoomArrows", "ExcludeNyoomArrows", "TWILIGHT SPIRE");

    }
    public override void Unload()
    {
        
    }
   
    [ModImportName("com.fortrise.OopsArrowsMod")] 
    public static class OopsArrowsModImports
    {
        // Fields are imported
        public static Action<MatchVariants, Variant> AutoLinkArrowStartVariants; //Links all start with arrow type variants to your variant.
        public static Action<ArrowTypes, string, string, string> AddCustomArrow; //Creates your arrow, will be added to treasure pool with the Spawn In Towers.
        public static Action<ArrowTypes, string, string, string, string> AddCustomArrowSpawn; //Creates your arrow, and will be added to treasure pool with your own variant.
        public static Action<Pickups, string> AddCustomPickup; //Creates your Pickup, spawns with Spawn In Towers.
        public static Action<Pickups, string, string> AddCustomPickupSpawn; //Creates your Pickup, and will be added to treasure pool with your own variant.
    }
}


