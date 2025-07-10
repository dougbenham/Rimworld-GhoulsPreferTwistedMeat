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
	    [HarmonyPrefix]
	    public static bool Prefix(ref float __result, Pawn eater, Thing foodSource, ThingDef foodDef, float dist, bool takingToInventory)
	    {
		    if (eater.IsGhoul)
		    {
			    if (foodDef == ThingDefOf.Meat_Twisted) // twisted meat
			    {
				    __result = 1000 - dist;
				    return false;
			    }

			    var category = foodDef.IsCorpse ? FoodUtility.GetMeatSourceCategoryFromCorpse(foodSource) : FoodUtility.GetMeatSourceCategory(foodDef);
			    if (foodDef.IsMeat && category == MeatSourceCategory.Humanlike) // human meat
			    {
				    __result = 950 - dist;
				    return false;
                }
			    else if (foodDef.IsMeat && category == MeatSourceCategory.Insect) // insect meat
			    {
				    __result = 900 - dist;
				    return false;
                }
			    else if (foodDef.IsCorpse && category == MeatSourceCategory.Humanlike) // humanlike corpse
			    {
				    __result = 850 - dist;
				    return false;
			    }
			    else if (foodDef.IsCorpse && category == MeatSourceCategory.Insect) // insect corpse
			    {
				    __result = 800 - dist;
				    return false;
                }
			    else if (foodDef.IsMeat) // other meat
			    {
				    __result = 750 - dist;
				    return false;
                }
                else if (foodDef.IsCorpse) // other corpse
                {
	                __result = 700 - dist;
	                return false;
                }
            }

		    return true;
	    }
    }
}
