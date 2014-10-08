using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using USITools;

namespace Kerbocalypse
{
    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class Kerbocalypse : MonoBehaviour
    {
        void Start()
        {
            if (FlightGlobals.ActiveVessel != null)
            {
                var v = FlightGlobals.ActiveVessel;
                for (int i = 0; i < 50; i++)
                {
                    var p = v.rootPart;
                    var r = new System.Random();

                    var x = r.Next(-500, 500);
                    var y = r.Next(-500, 500);
                    var z = 750;

                    var m = CIT_Util.PartFactory.SpawnPartInFlight("Meteor", p, new Vector3(x, y, z), p.vessel.srfRelRotation);
                    m.vessel.distanceUnpackThreshold = 5000;
                    m.vessel.distancePackThreshold = 5000;
                    m.vessel.distanceLandedUnpackThreshold = 5000;
                    m.vessel.distanceLandedPackThreshold = 5000;

                    m.vesselType = VesselType.SpaceObject;

                    var newX = m.transform.position.x;
                    var newY = m.transform.position.y;
                    var newZ = m.transform.position.z;
                    m.vessel.SetPosition(new Vector3(newX,newY,newZ));
                }
            }   
        }

        void Update()
        {
            var vessels = FlightGlobals.Vessels.Where(x => x.mainBody == FlightGlobals.currentMainBody);
            foreach (var v in vessels)
            {
                if (v.FindPartModulesImplementing<USI_Emmitter>().Any())
                {
                    v.GoOffRails();
                }
            }
        }

    }
}