using HarmonyLib;
using UnityEngine;
using Verse;

namespace DaiCat
{
    [StaticConstructorOnStartup]
    public static class Startup
    {
        static Startup()
        {
            new Harmony("Adou.daimao.sleepgraphics").PatchAll();
        }
    }

    [HarmonyPatch(typeof(PawnRenderNode_AnimalPart), "GraphicFor")]
    public static class AnimalPartGraphicForPatch
    {
        public static void Postfix(Pawn pawn, ref Graphic __result)
        {
            if (pawn == null || pawn.kindDef == null || pawn.kindDef.defName != "DaiCat" ||
                pawn.jobs == null || pawn.jobs.curDriver == null || !pawn.jobs.curDriver.asleep)
            {
                return;
            }

            string stage = pawn.ageTracker.CurLifeStageIndex == 0 ? "Young" : "Adult";
            string path = "Things/Pawn/Animal/DaiCat/DaiCat_" + stage + "Sleep";
            __result = GraphicDatabase.Get<Graphic_Single>(path, __result.Shader, __result.drawSize,
                __result.color, __result.colorTwo);
        }
    }
}
