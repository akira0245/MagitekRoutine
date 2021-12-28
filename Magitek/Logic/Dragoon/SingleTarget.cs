﻿using ff14bot;
using ff14bot.Managers;
using ff14bot.Objects;
using Magitek.Extensions;
using Magitek.Models.Dragoon;
using Magitek.Utilities;
using System.Linq;
using System.Threading.Tasks;
using Auras = Magitek.Utilities.Auras;
using DragoonRoutine = Magitek.Utilities.Routines.Dragoon;

namespace Magitek.Logic.Dragoon
{
    internal static class SingleTarget
    {

        public static async Task<bool> TrueThrust()
        {
            if (Core.Me.HasAura(Auras.SharperFangandClaw))
                return false;

            if (Core.Me.HasAura(Auras.EnhancedWheelingThrust))
                return false;

            if (Core.Me.HasAura(Auras.DraconianFire))
                return false;

            return await Spells.TrueThrust.Cast(Core.Me.CurrentTarget);
        }

        public static async Task<bool> RaidenThrust()
        {
            if (!Core.Me.HasAura(Auras.DraconianFire))
                return false;

            return await Spells.RaidenThrust.Cast(Core.Me.CurrentTarget);
        }

        /***************************************************************************
        *                           Combo 1
        * *************************************************************************/
        public static async Task<bool> Disembowel()
        {
            if (!DragoonRoutine.CanContinueComboAfter(Spells.TrueThrust) && !DragoonRoutine.CanContinueComboAfter(Spells.RaidenThrust))
                return false;

            return await Spells.Disembowel.Cast(Core.Me.CurrentTarget);
        }

        public static async Task<bool> ChaoticSpring()
        {
            if (!DragoonRoutine.CanContinueComboAfter(Spells.Disembowel))
                return false;

            return await DragoonRoutine.ChaoticSpring.Cast(Core.Me.CurrentTarget);
        }

        public static async Task<bool> WheelingThrust()
        {
            if (!Core.Me.HasAura(Auras.EnhancedWheelingThrust))
                return false;

            return await Spells.WheelingThrust.Cast(Core.Me.CurrentTarget);
        }

        /***************************************************************************
         *                           Combo 2
         * *************************************************************************/
        public static async Task<bool> VorpalThrust()
        {
            if (!DragoonRoutine.CanContinueComboAfter(Spells.TrueThrust) && !DragoonRoutine.CanContinueComboAfter(Spells.RaidenThrust))
                return false;

            if (!Core.Me.HasAura(Auras.PowerSurge))
                return false;

            if (!Core.Me.CurrentTarget.HasAura(Auras.ChaoticSpring))
                return false;

            Aura PowerSurgeAura = (Core.Me as Character).Auras.FirstOrDefault(x => x.Id == Auras.PowerSurge);
            if (Core.Me.HasAura(Auras.PowerSurge) && PowerSurgeAura.TimespanLeft.TotalMilliseconds <= 6000)
                return false;

            Aura ChaoticSpringAura = (Core.Me.CurrentTarget as Character).Auras.FirstOrDefault(x => x.Id == Auras.ChaoticSpring);
            if (Core.Me.CurrentTarget.HasAura(Auras.ChaoticSpring) && ChaoticSpringAura.TimespanLeft.TotalMilliseconds <= 6000)
                return false;

            return await Spells.VorpalThrust.Cast(Core.Me.CurrentTarget);
        }

        public static async Task<bool> HeavensThrust()
        {
            if (!DragoonRoutine.CanContinueComboAfter(Spells.VorpalThrust))
                return false;

            return await DragoonRoutine.HeavensThrust.Cast(Core.Me.CurrentTarget);
        }

        public static async Task<bool> FangAndClaw()
        {
            if (!Core.Me.HasAura(Auras.SharperFangandClaw))
                return false;

            return await Spells.FangAndClaw.Cast(Core.Me.CurrentTarget);
        }

    }
}
