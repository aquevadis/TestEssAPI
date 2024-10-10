using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Core.Capabilities;
using CounterStrikeSharp.API.Modules.Commands;
using CounterStrikeSharp.API.Modules.Memory;
using CounterStrikeSharp.API.Modules.Utils;
using System.Drawing;
using Vector = CounterStrikeSharp.API.Modules.Utils.Vector;
using EntitySubSystemAPI;
using static EntitySubSystemAPI.IEntitySubSystemAPI;

namespace TestEssAPI;

public class TestEssAPI : BasePlugin
{
    public override string ModuleName => "TestEssAPI";
    public override string ModuleAuthor => "AquaVadis";
    public override string ModuleVersion => "1.0.4s";

    public static PluginCapability<IEntitySubSystemAPI> Capability { get; } = new("ess:base");
    
    public int trackedEntities = 0;

    public override void Load(bool hotReload)
    {
        
        //register listeners
        RegisterListener<Listeners.OnEntityCreated>(OnEntityCreatedBase);

    }

    public override void OnAllPluginsLoaded(bool hotReload)
    {
        var entitySubSystemAPI = Capability.Get();
        if (entitySubSystemAPI == null) return;

        entitySubSystemAPI.OnPlayerTouchEntity += (CEntityInstance touchedEntity, CCSPlayerPawnBase touchingPlayerPawnBase) => {

            //debug log:
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[OnEntityTouchByPlayer][Consumer Plugin] {touchedEntity.DesignerName} touched by {touchingPlayerPawnBase.OriginalController.Value?.PlayerName}");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[OnEntityTouchByPlayer][Consumer Plugin] Tracked Entities: {trackedEntities}");
            Console.ResetColor();

        };
        
    }

    public override void Unload(bool hotReload)
    {

        //register listeners
        RemoveListener<Listeners.OnEntityCreated>(OnEntityCreatedBase);

        base.Unload(hotReload);
    }

    public void OnEntityCreatedBase(CEntityInstance entity)
	{
        var entitySubSystemAPI = Capability.Get();
        if (entitySubSystemAPI == null) return;

        if (entity.DesignerName.Contains("weapon")) {
            
            entitySubSystemAPI.StartTouch(entity);
            trackedEntities++;
        }

    }

    [ConsoleCommand("css_testapi", "")]
    [CommandHelper(minArgs: 0, whoCanExecute: CommandUsage.CLIENT_AND_SERVER)]
    public void TestAPI(CCSPlayerController? player, CommandInfo command)
    {

        if (player is null || player.IsValid is not true) return;

        var entitySubSystemAPI = Capability.Get();
        if (entitySubSystemAPI == null) return;

        //

    }

    [ConsoleCommand("css_testapi2", "")]
    [CommandHelper(minArgs: 0, whoCanExecute: CommandUsage.CLIENT_AND_SERVER)]
    public void TestAPI2(CCSPlayerController? player, CommandInfo command)
    {

        if (player is null || player.IsValid is not true) return;

        var entitySubSystemAPI = Capability.Get();
        if (entitySubSystemAPI == null) return;

        foreach (var listPlayer in Utilities.GetPlayers().Where(p => p is not null && p.PawnIsAlive is true))
        {
            listPlayer.GiveNamedItem("weapon_ak47");
            listPlayer.GiveNamedItem("weapon_deagle");
            listPlayer.GiveNamedItem("weapon_hegrenade");
            listPlayer.GiveNamedItem("weapon_smokegrenade");
            listPlayer.GiveNamedItem("weapon_molotov");
        }

    }


}
