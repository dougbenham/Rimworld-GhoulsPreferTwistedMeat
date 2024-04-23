using HarmonyLib;
using Verse;
using RimWorld;

namespace GhoulsPreferTwistedMeat
{
    [StaticConstructorOnStartup]
    public static class Main
    {
        static Main()
        {
            Harmony instance = new Harmony("doug.ghoulsprefertwistedmeat");
            instance.PatchAll();
        }
    }

    [HarmonyPatch(typeof(FoodUtility), "FoodOptimality")]
    public static class FoodUtility_FoodOptimality_Patch
    {
	    [HarmonyPostfix]
	    public static void Postfix(ref float __result, Pawn eater, Thing foodSource, ThingDef foodDef, float dist, bool takingToInventory)
	    {
		    if (eater.IsGhoul)
		    {
			    if (foodDef == ThingDefOf.Meat_Twisted)
				    __result += 300;
			    else if (foodDef.IsCorpse)
				    __result += 200;
		    }
	    }
    }
}