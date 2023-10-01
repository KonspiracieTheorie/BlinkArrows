using FortRise;
using HarmonyLib;
using Monocle;
using MonoMod.ModInterop;
using System;
using System.Diagnostics;
using TowerFall;



namespace BlinkArrowMod;

[Fort("com.CoolModder.ExampleArrowMod", "ArrowTest")]

public class ExampleArrowModModule : FortModule
{
    public static ExampleArrowModModule Instance;
    public static Atlas VariantAtlas;
    public static Atlas BlinkAtlas;
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
    }


    public override void Load()
    {
        typeof(OopsArrowsModImports).ModInterop();
    }


    public override void OnVariantsRegister(VariantManager manager, bool noPerPlayer = false)
    {
        var startBlinkInfo = new CustomVariantInfo("StartWithBlinkArrows", VariantAtlas["variants/startWithBlinkArrows"], CustomVariantFlags.PerPlayer | CustomVariantFlags.CanRandom);
        var StartWithBlinkArrows = manager.AddVariant(startBlinkInfo, noPerPlayer); //Start Variant


        var excludeBlinkInfo = new CustomVariantInfo("ExcludeBlinkArrows", VariantAtlas["variants/excludeBlinkArrows"], CustomVariantFlags.PerPlayer | CustomVariantFlags.CanRandom);
        var excludeBlinkArrows = manager.AddVariant(excludeBlinkInfo, noPerPlayer);


        OopsArrowsModImports.AddCustomArrow(RiseCore.ArrowsRegistry["BlinkArrows"].Types, "StartWithBlinkArrows", "ExcludeBlinkArrows", "MIRAGE");
       
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


