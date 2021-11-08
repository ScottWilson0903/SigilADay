using BepInEx;
using BepInEx.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.IO;
using DiskCardGame;
using HarmonyLib;
using UnityEngine;
using APIPlugin;

namespace SigilADay
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    [BepInDependency("cyantist.inscryption.api", BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {
        private const string PluginGuid = "cyantist.inscryption.sigiladay";
        private const string PluginName = "SigilADay";
        private const string PluginVersion = "1.1.2.0";

        private void Awake()
        {
            Logger.LogInfo($"Loaded {PluginName}!");

            AddBloodGuzzler();
            AddLeech();
            AddRegen1();
            AddRegen2();
            AddRegen3();
            AddRegenFull();
            AddPoisonous();
            ChangeRingworm();
        }

        private void ChangeRingworm(){
            List<Ability> abilities = new List<Ability> {Poisonous.ability};
            new CustomCard("RingWorm") {abilities=abilities};
        }

        private NewAbility AddBloodGuzzler()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.powerLevel = 5;
            info.rulebookName = "BloodGuzzler";
            info.rulebookDescription = "When a creature bearing this sigil deals damage, it gains 1 Health for each damage dealt.";
            info.metaCategories = new List<AbilityMetaCategory> {AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular};

            List<DialogueEvent.Line> lines = new List<DialogueEvent.Line>();
            DialogueEvent.Line line = new DialogueEvent.Line();
            line.text = "This creature will gains 1 Health for each damage it deals to creatures.";
            lines.Add(line);
            info.abilityLearnedDialogue = new DialogueEvent.LineSet(lines);

            byte[] imgBytes = System.IO.File.ReadAllBytes("BepInEx/plugins/CardLoader/Artwork/ability_bloodguzzler.png");
            Texture2D tex = new Texture2D(2,2);
            tex.LoadImage(imgBytes);

            NewAbility ability = new NewAbility(info,typeof(BloodGuzzler),tex);
            BloodGuzzler.ability = ability.ability;
            return ability;
        }

        private NewAbility AddLeech()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.powerLevel = 4;
            info.rulebookName = "Leech";
            info.rulebookDescription = "When a creature bearing this sigil deals damage, it heals 1 Health for each damage dealt.";
            info.metaCategories = new List<AbilityMetaCategory> {AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular};

            List<DialogueEvent.Line> lines = new List<DialogueEvent.Line>();
            DialogueEvent.Line line = new DialogueEvent.Line();
            line.text = "This creature will heals 1 Health for each damage it deals to creatures.";
            lines.Add(line);
            info.abilityLearnedDialogue = new DialogueEvent.LineSet(lines);

            // Icon made by Lorc. Available on https://game-icons.net
            byte[] imgBytes = System.IO.File.ReadAllBytes("BepInEx/plugins/CardLoader/Artwork/ability_leech.png");
            Texture2D tex = new Texture2D(2,2);
            tex.LoadImage(imgBytes);

            NewAbility ability = new NewAbility(info,typeof(Leech),tex);
            Leech.ability = ability.ability;
            return ability;
        }

        private NewAbility AddRegen1()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.powerLevel = 1;
            info.rulebookName = "Regen 1";
            info.rulebookDescription = "At the end of the owner's turn, [creature] will regen 1 health.";
            info.metaCategories = new List<AbilityMetaCategory> {AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular};

            List<DialogueEvent.Line> lines = new List<DialogueEvent.Line>();
            DialogueEvent.Line line = new DialogueEvent.Line();
            line.text = "This creature will heal 1 Health at the end of it's owner's turn.";
            lines.Add(line);
            info.abilityLearnedDialogue = new DialogueEvent.LineSet(lines);

            byte[] imgBytes = System.IO.File.ReadAllBytes("BepInEx/plugins/CardLoader/Artwork/ability_regen_1.png");
            Texture2D tex = new Texture2D(2,2);
            tex.LoadImage(imgBytes);

            NewAbility ability = new NewAbility(info,typeof(Regen1),tex);
            Regen1.ability = ability.ability;
            return ability;
        }

        private NewAbility AddRegen2()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.powerLevel = 2;
            info.rulebookName = "Regen 2";
            info.rulebookDescription = "At the end of the owner's turn, [creature] will regen 2 health.";
            info.metaCategories = new List<AbilityMetaCategory> {AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular};

            List<DialogueEvent.Line> lines = new List<DialogueEvent.Line>();
            DialogueEvent.Line line = new DialogueEvent.Line();
            line.text = "This creature will heal 2 Health at the end of it's owner's turn.";
            lines.Add(line);
            info.abilityLearnedDialogue = new DialogueEvent.LineSet(lines);

            byte[] imgBytes = System.IO.File.ReadAllBytes("BepInEx/plugins/CardLoader/Artwork/ability_regen_2.png");
            Texture2D tex = new Texture2D(2,2);
            tex.LoadImage(imgBytes);

            NewAbility ability = new NewAbility(info,typeof(Regen2),tex);
            Regen2.ability = ability.ability;
            return ability;
        }

        private NewAbility AddRegen3()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.powerLevel = 3;
            info.rulebookName = "Regen 3";
            info.rulebookDescription = "At the end of the owner's turn, [creature] will regen 3 health.";
            info.metaCategories = new List<AbilityMetaCategory> {AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular};

            List<DialogueEvent.Line> lines = new List<DialogueEvent.Line>();
            DialogueEvent.Line line = new DialogueEvent.Line();
            line.text = "This creature will heal 3 Health at the end of it's owner's turn.";
            lines.Add(line);
            info.abilityLearnedDialogue = new DialogueEvent.LineSet(lines);

            byte[] imgBytes = System.IO.File.ReadAllBytes("BepInEx/plugins/CardLoader/Artwork/ability_regen_3.png");
            Texture2D tex = new Texture2D(2,2);
            tex.LoadImage(imgBytes);

            NewAbility ability = new NewAbility(info,typeof(Regen3),tex);
            Regen3.ability = ability.ability;
            return ability;
        }

        private NewAbility AddRegenFull()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.powerLevel = 4;
            info.rulebookName = "Regen";
            info.rulebookDescription = "At the end of the owner's turn, [creature] will regen to full health.";
            info.metaCategories = new List<AbilityMetaCategory> {AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular};

            List<DialogueEvent.Line> lines = new List<DialogueEvent.Line>();
            DialogueEvent.Line line = new DialogueEvent.Line();
            line.text = "This creature will heal to full Health at the end of it's owner's turn.";
            lines.Add(line);
            info.abilityLearnedDialogue = new DialogueEvent.LineSet(lines);

            byte[] imgBytes = System.IO.File.ReadAllBytes("BepInEx/plugins/CardLoader/Artwork/ability_regen_full.png");
            Texture2D tex = new Texture2D(2,2);
            tex.LoadImage(imgBytes);

            NewAbility ability = new NewAbility(info,typeof(RegenFull),tex);
            RegenFull.ability = ability.ability;
            return ability;
        }

        private NewAbility AddPoisonous()
        {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.powerLevel = 3;
            info.rulebookName = "Poisonous";
            info.rulebookDescription = "When [creature] perishes, the creature that killed it perishes as well.";
            info.metaCategories = new List<AbilityMetaCategory> {AbilityMetaCategory.Part1Rulebook, AbilityMetaCategory.Part1Modular};

            List<DialogueEvent.Line> lines = new List<DialogueEvent.Line>();
            DialogueEvent.Line line = new DialogueEvent.Line();
            line.text = "When this creature perishes, it will kill the creature that killed it.";
            lines.Add(line);
            info.abilityLearnedDialogue = new DialogueEvent.LineSet(lines);

            byte[] imgBytes = System.IO.File.ReadAllBytes("BepInEx/plugins/CardLoader/Artwork/ability_poisonous.png");
            Texture2D tex = new Texture2D(2,2);
            tex.LoadImage(imgBytes);

            NewAbility ability = new NewAbility(info,typeof(Poisonous),tex);
            Poisonous.ability = ability.ability;
            return ability;
        }

        public class BloodGuzzler : AbilityBehaviour
        {
            public override Ability Ability
            {
                get
                {
                    return ability;
                }
            }

            public static Ability ability;

            private void Start()
            {
                int health = base.Card.Info.Health;
                this.mod = new CardModificationInfo();
                this.mod.nonCopyable = true;
                this.mod.singletonId = "increaseHP";
                this.mod.healthAdjustment = 0;
                base.Card.AddTemporaryMod(this.mod);
            }

            public override bool RespondsToDealDamage(int amount, PlayableCard target)
            {
                 return amount > 0;
            }

            public override IEnumerator OnDealDamage(int amount, PlayableCard target)
            {
                yield return base.PreSuccessfulTriggerSequence();
                this.mod.healthAdjustment += amount;
                base.Card.OnStatsChanged();
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.25f);
                yield return base.LearnAbility(0.25f);
                yield break;
            }

            private CardModificationInfo mod;
        }

        public class Leech : AbilityBehaviour
        {
            public override Ability Ability
            {
                get
                {
                    return ability;
                }
            }

            public static Ability ability;

            public override bool RespondsToDealDamage(int amount, PlayableCard target)
            {
                 return amount > 0;
            }

            public override IEnumerator OnDealDamage(int amount, PlayableCard target)
            {
                yield return base.PreSuccessfulTriggerSequence();
                if (base.Card.Status.damageTaken > 0)
                {
                    base.Card.HealDamage(Mathf.Clamp(amount, 1, base.Card.Status.damageTaken));
                }
                base.Card.OnStatsChanged();
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.25f);
                yield return base.LearnAbility(0.25f);
                yield break;
            }
        }

        public class Regen1 : AbilityBehaviour
        {
            public override Ability Ability
            {
                get
                {
                    return ability;
                }
            }

            public static Ability ability;

            public override bool RespondsToUpkeep(bool playerUpkeep)
        		{
        			  return base.Card.OpponentCard != playerUpkeep;
        		}

            public override IEnumerator OnUpkeep(bool playerUpkeep)
            {
                yield return base.PreSuccessfulTriggerSequence();
                if (base.Card.Status.damageTaken > 0)
                {
                    base.Card.HealDamage(1);
                }
                yield return base.LearnAbility(0.25f);
                yield break;
            }
        }

        public class Regen2 : AbilityBehaviour
        {
            public override Ability Ability
            {
                get
                {
                    return ability;
                }
            }

            public static Ability ability;

            public override bool RespondsToUpkeep(bool playerUpkeep)
        		{
        			  return base.Card.OpponentCard != playerUpkeep;
        		}

            public override IEnumerator OnUpkeep(bool playerUpkeep)
            {
                yield return base.PreSuccessfulTriggerSequence();
                if (base.Card.Status.damageTaken > 0)
                {
                    base.Card.HealDamage(Mathf.Clamp(base.Card.Status.damageTaken, 1, 2));
                }
                yield return base.LearnAbility(0.25f);
                yield break;
            }
        }

        public class Regen3 : AbilityBehaviour
        {
            public override Ability Ability
            {
                get
                {
                    return ability;
                }
            }

            public static Ability ability;

            public override bool RespondsToUpkeep(bool playerUpkeep)
        		{
        			  return base.Card.OpponentCard != playerUpkeep;
        		}

            public override IEnumerator OnUpkeep(bool playerUpkeep)
            {
                yield return base.PreSuccessfulTriggerSequence();
                if (base.Card.Status.damageTaken > 0)
                {
                    base.Card.HealDamage(Mathf.Clamp(base.Card.Status.damageTaken, 1, 3));
                }
                yield return base.LearnAbility(0.25f);
                yield break;
            }
        }

        public class RegenFull : AbilityBehaviour
        {
            public override Ability Ability
            {
                get
                {
                    return ability;
                }
            }

            public static Ability ability;

            public override bool RespondsToUpkeep(bool playerUpkeep)
        		{
        			   return base.Card.OpponentCard != playerUpkeep;
        		}

            public override IEnumerator OnUpkeep(bool playerUpkeep)
            {
                yield return base.PreSuccessfulTriggerSequence();
                base.Card.HealDamage(base.Card.Status.damageTaken);
                yield return base.LearnAbility(0.25f);
                yield break;
            }
        }

        public class Poisonous : AbilityBehaviour
      	{
        		public override Ability Ability
        		{
          			get
          			{
          			     return ability;
          			}
        		}

            public static Ability ability;

        		public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        		{
        			   return !wasSacrifice && base.Card.OnBoard;
        		}

        		public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        		{
        				yield return base.PreSuccessfulTriggerSequence();
        				yield return new WaitForSeconds(0.25f);
                if (killer != null)
                {
                    yield return killer.Die(false, base.Card, true);
                    if (Singleton<BoardManager>.Instance is BoardManager3D)
                    {
                        yield return new WaitForSeconds(0.5f);
                        yield return base.LearnAbility(0.5f);
                    }
                }
                yield break;
        		}
      	}
    }
}
