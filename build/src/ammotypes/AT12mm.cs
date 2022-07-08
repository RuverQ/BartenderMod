using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuckGame.BartenderMod.AmmoTypes
{
    public class AT12mm : AmmoType
    {
        public AT12mm()
        {
            accuracy = 0.75f;
            range = 250f;
            penetration = 0.4f;
            bulletSpeed = 10f;
            combustable = true;
            affectedByGravity = true;
            weight = 1.2f;
        }

        public override void PopShell(float x, float y, int dir)
        {
            Level.Add(new PistolShell(x, y)
            {
                hSpeed = (float)dir * (1.5f + Rando.Float(1f))
            });
        }


        public override void OnHit(bool destroyed, Bullet b)
        {
            base.OnHit(destroyed, b);
            if(b.owner is Duck)
            {
                
            }
        }
    }
}
