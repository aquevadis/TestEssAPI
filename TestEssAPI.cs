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
using EssAPI;
using static EssAPI.IEssAPI;

namespace TestEssAPI;

public class TestEssAPI : BasePlugin
{
    public override string ModuleName => "TestEssAPI";
    public override string ModuleAuthor => "AquaVadis";
    public override string ModuleVersion => "0.0.1";

    public static IEssAPI? _api;

    public static PluginCapability<IEssAPI> _pluginCapability { get; } = new("ess:core");

    public override void Load(bool hotReload)
    {
        // _api = _pluginCapability.Get();
        // if (_api == null) return;

        //register listeners
        RegisterListener<Listeners.OnEntityCreated>(OnEntityCreatedBase);

       //public static PluginCapability<IZombiePlagueAPI> BalanceServiceCapability { get; } = new("myplugin:balance_service");
    }

    public override void OnAllPluginsLoaded(bool hotReload)
    {
        _api = _pluginCapability.Get();
        if (_api == null) return;

        //register listeners
        RegisterListener<Listeners.OnEntityCreated>(OnEntityCreatedBase);

       //public static PluginCapability<IZombiePlagueAPI> BalanceServiceCapability { get; } = new("myplugin:balance_service");
    }

    public override void Unload(bool hotReload)
    {

        //register listeners
        RemoveListener<Listeners.OnEntityCreated>(OnEntityCreatedBase);

        base.Unload(hotReload);
    }

    public static void OnEntityCreatedBase(CEntityInstance entity)
	{
        _api?.StartTouch(entity);
    }

}
