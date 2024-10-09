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
            Console.WriteLine($"[OnEntityTouchByPlayer][Consumer Plugin] {touchedEntity.DesignerName} touched by {touchingPlayerPawnBase.DesignerName}");
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

        entitySubSystemAPI.StartTouch(entity);

    }

    [ConsoleCommand("css_testapi", "")]
    [CommandHelper(minArgs: 0, whoCanExecute: CommandUsage.CLIENT_AND_SERVER)]
    public void Unhide(CCSPlayerController? player, CommandInfo command)
    {

        if (player is null || player.IsValid is not true) return;

        var entitySubSystemAPI = Capability.Get();
        if (entitySubSystemAPI == null) return;

        //

    }


}
