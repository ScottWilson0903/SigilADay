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
  public partial class Plugin : BaseUnityPlugin
  {
    private const string PluginGuid = "cyantist.inscryption.sigiladay";
    private const string PluginName = "SigilADay";
    private const string PluginVersion = "1.4.1.0";

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
      AddThickShell();
      AddBonePicker();
      AddNutritious();

      ChangeRingworm();
    }

    private void ChangeRingworm(){
      List<Ability> abilities = new List<Ability> {Poisonous.ability};
      new CustomCard("RingWorm") {abilities=abilities};
    }
  }
}
