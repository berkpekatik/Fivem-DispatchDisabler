using CitizenFX.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CitizenFX.Core.Native.API;

namespace DisableDispatchService
{
    public class Main : BaseScript
    {
        private static ConfigModel config = new ConfigModel();
        public Main()
        {
            var data = LoadResourceFile(GetCurrentResourceName(), "config.json");
            try
            {
                config = JsonConvert.DeserializeObject<ConfigModel>(data);
                if (config.Dispatch)
                {

                    Tick += Dispatch_Tick;
                }
                if (config.Traffic)
                {
                    Tick += Traffic_Tick;
                }
            }
            catch
            {
                Debug.WriteLine("#################################");
                Debug.WriteLine("# Config File cannot read!");
                Debug.WriteLine("# May This resource cannot work!");
                Debug.WriteLine("#################################");
            }

        }

        private async Task Traffic_Tick()
        {
            await Delay(0);
            //These natives have to be called every frame.
            SetVehicleDensityMultiplierThisFrame(config.Destinity);// set traffic density to 0
            SetPedDensityMultiplierThisFrame(config.Destinity);// set npc/ ai peds density to 0
            SetRandomVehicleDensityMultiplierThisFrame(config.Destinity);// set random vehicles(car scenarios / cars driving off from a parking spot etc.) to 0
            SetParkedVehicleDensityMultiplierThisFrame(config.Destinity);// set random parked vehicles(parked car scenarios) to 0
            SetScenarioPedDensityMultiplierThisFrame(config.Destinity, config.Destinity);// set random npc / ai peds or scenario peds to 0
            SetGarbageTrucks(false);// Stop garbage trucks from randomly spawning
            SetRandomBoats(false);// Stop random boats from spawning in the water.
            SetCreateRandomCops(false);// disable random cops walking/ driving around.
            SetCreateRandomCopsNotOnScenarios(false);// stop random cops(not in a scenario) from spawning.
            SetCreateRandomCopsOnScenarios(false);// stop random cops(in a scenario) from spawning.
            var pos = Game.PlayerPed.Position;
            ClearAreaOfVehicles(pos.X, pos.Y, pos.Z, 1000, false, false, false, false, false);
            RemoveVehiclesFromGeneratorsInArea(pos.X - 500.0F, pos.Y - 500.0F, pos.Z - 500.0F, pos.X + 500.0F, pos.Y + 500.0F, pos.Z + 500.0F, 0);
        }
        private async Task Dispatch_Tick()
        {
            await Delay(100);
            for (int i = 1; i <= 15; i++)
            {
                EnableDispatchService(i, false);
            }
            SetPlayerWantedLevel(PlayerId(), 0, false);
            SetPlayerWantedLevelNow(PlayerId(), false);
            SetPlayerWantedLevelNoDrop(PlayerId(), 0, false);
            SetGarbageTrucks(false);
            SetRandomBoats(false);
            SetCreateRandomCops(false);
            SetCreateRandomCopsNotOnScenarios(false);
            SetCreateRandomCopsOnScenarios(false);
        }
    }
}
